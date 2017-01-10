using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusLayerDataAccess.ModelsDTO;
using System.Data.Entity.Infrastructure;


namespace BusLayerDataAccess.Services
{
    public class UsersService
    {
        private EntityDBEntities1 db = new EntityDBEntities1();

        public List<UserDTO> getUsers() // 1.
        {
            var users = db.AspNetUsers.Select(b => new UserDTO()
            {
                Id = b.Id, 
                Email = b.Email,
                EmailConfirmed = b.EmailConfirmed,
                PasswordHash = b.Id,
                SecurityStamp = b.PasswordHash,
                PhoneNumber = b.PhoneNumber,
                PhoneNumberConfirmed = b.PhoneNumberConfirmed,
                TwoFactorEnabled = b.TwoFactorEnabled,
                LockoutEndDateUtc = b.LockoutEndDateUtc,
                LockoutEnabled = b.LockoutEnabled,
                AccessFailedCount = b.AccessFailedCount,
                UserName = b.UserName,

                //ola ta alla pedia


            }).ToList();

            return users ; // girnaei lista me tous users
        }

        public UserDTO getUser(string id) // 2.
   {
    // psaxnei gia ena user sigkekrimena me vasi to id tou
    var user = db.AspNetUsers.Where(o => o.Id.Equals(id)).Select(b => new UserDTO()
    {
        Id = b.Id,
        Email = b.Email,
        EmailConfirmed = b.EmailConfirmed,
        PasswordHash = b.Id,
        SecurityStamp = b.PasswordHash,
        PhoneNumber = b.PhoneNumber,
        PhoneNumberConfirmed = b.PhoneNumberConfirmed,
        TwoFactorEnabled = b.TwoFactorEnabled,
        LockoutEndDateUtc = b.LockoutEndDateUtc,
        LockoutEnabled = b.LockoutEnabled,
        AccessFailedCount = b.AccessFailedCount,
        UserName = b.UserName,


    })/*.ToList()*/;

    return user.FirstOrDefault();
}

public UserDTO getUser(string username , string id) // 2.
{
    // psaxnei gia ena user sigkekrimena me vasi to id tou
    var user = db.AspNetUsers.Where(o => o.UserName.Equals(username) && o.Id.Equals(id)).Select(b => new UserDTO()
    {
        Id = b.Id,
        Email = b.Email,
        EmailConfirmed = b.EmailConfirmed,
        PasswordHash = b.Id,
        SecurityStamp = b.PasswordHash,
        PhoneNumber = b.PhoneNumber,
        PhoneNumberConfirmed = b.PhoneNumberConfirmed,
        TwoFactorEnabled = b.TwoFactorEnabled,
        LockoutEndDateUtc = b.LockoutEndDateUtc,
        LockoutEnabled = b.LockoutEnabled,
        AccessFailedCount = b.AccessFailedCount,
        UserName = b.UserName,


    })/*.ToList()*/;

    return user.FirstOrDefault();
}


public int putUser(string id, UserDTO user) //3. update a user 
{       // kane update //!!!!
    try
    {
        if (!UserExists(id)) { return -1; }
        var b = (from x in db.AspNetUsers
                 where x.Id.Equals(id)
                 select x).First();
                b.Id = user.Id;
                b.Email = user.Email;
                b.EmailConfirmed = user.EmailConfirmed;
                b.PasswordHash = user.PasswordHash;
                b.SecurityStamp = user.SecurityStamp;
                b.PhoneNumber = user.PhoneNumber;
                b.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
                b.TwoFactorEnabled = user.TwoFactorEnabled;
                b.LockoutEndDateUtc = user.LockoutEndDateUtc;
                b.LockoutEnabled = user.LockoutEnabled;
                b.AccessFailedCount = user.AccessFailedCount;
                b.UserName = user.UserName;

        db.SaveChanges();
    }
    catch (DbUpdateConcurrencyException)
    {
        return 0;
    }

    return 0;

}

public int postUser(UserDTO user)
{ //4. insert a book
  // kane insert
    try
    {

        //New Code
        var dto = new AspNetUser()
        {
            Id = user.Id,
            Email = user.Email,
            EmailConfirmed = user.EmailConfirmed,
            PasswordHash = user.Id,
            SecurityStamp = user.PasswordHash,
            PhoneNumber = user.PhoneNumber,
            PhoneNumberConfirmed = user.PhoneNumberConfirmed,
            TwoFactorEnabled = user.TwoFactorEnabled,
            LockoutEndDateUtc = user.LockoutEndDateUtc,
            LockoutEnabled = user.LockoutEnabled,
            AccessFailedCount = user.AccessFailedCount,
            UserName = user.UserName,


        };
        db.AspNetUsers.Add(dto);
        return db.SaveChanges();
    }
    catch (DbUpdateConcurrencyException)
    {
        return 0;
    }
}

public int deleteUser(string id)
{

    // delete a book 
    try
    {
        if (!UserExists(id)) { return -1; }

        var userTobeDeleted = (from d in db.AspNetUsers
                               where d.Id.Equals(id)
                               select d).Single();

        db.AspNetUsers.Remove(userTobeDeleted);
        db.SaveChanges();

    }
    catch (DbUpdateConcurrencyException)
    {
        return 0;
    }

    return 0;

}
protected void Dispose(bool disposing)
{
    if (disposing)
    {
        db.Dispose();
    }

}
private bool UserExists(string id)
{
    return db.AspNetUsers.Count(e => e.Id.Equals(id)) > 0;
}

    }
}
