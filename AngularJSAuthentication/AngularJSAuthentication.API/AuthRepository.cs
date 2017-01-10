using AngularJSAuthentication.API.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AngularJSAuthentication.API
{
    public class AuthRepository : IDisposable
    {
        private AuthContext _ctx;

        private UserManager<IdentityUser> _userManager;

        private RoleManager<IdentityRole> _rm;

        public AuthRepository()
        {
            _ctx = new AuthContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
            _rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_ctx));

            //TODO this needs to be removed
            String[] roles = new String[] { "LibraryUser", "Librarian" };
            foreach (string role in roles)
            {
                if (!_rm.RoleExists(role))
                {
                    var roleResult = _rm.Create(new IdentityRole(role));
                    if (!roleResult.Succeeded)
                        throw new ApplicationException("Creating role " + role + "failed with error(s): " + roleResult.Errors);
                }
            }
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.UserName
                //Email = userModel.Email;
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (result.Succeeded)
            {
                var currentUser = _userManager.FindByName(user.UserName);
                var role = _rm.FindByName("LibraryUser");
                var roleresult = _userManager.AddToRole(currentUser.Id, role.Name);
            }
            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public string FindRole(IdentityUser user)
        {
            return _userManager.GetRoles(user.Id)[0];
        }

        public string FindId(string userName)
        {
            try
            {
                var ans = _userManager.FindByName(userName).Id;
                return ans;
            }
            catch
            {
                return null;
            }
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }
}