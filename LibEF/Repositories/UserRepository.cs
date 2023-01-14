using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibEF.Repositories
{
    public class UserRepository
    {
        public User GetUserById(int id)
        {
            using (var db = new AppContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Id == id);
                return user;
            }
        }
        public IEnumerable<User> GetAllUsers()
        {
            using (var db = new AppContext())
            {
               List<User> users = db.Users.ToList();
                return users;
            }
        }

        public void Add(User user)
        {
            using (var db = new AppContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public void Delete(User user)
        {
            using (var db = new AppContext())
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }
        public void UpdateNameById(int id, string newName)
        {
            User user = GetUserById(id);
            if (user != null)
            {
                user.Name = newName;

                using (var db = new AppContext())
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else throw new Exception() ;
        }
    }
}
