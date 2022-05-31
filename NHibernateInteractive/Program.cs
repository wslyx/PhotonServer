using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernateInteractive.Model;
using NHibernateInteractive.Manager;

namespace NHibernateInteractive
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User() { Id=2 , Username = "哈哈哈", Password = "Ddllsdla", Registerdate = DateTime.Today };
            IUserManager userManager = new UserManager();
            //userManager.Add(user);
            //userManager.Update(user);
            //userManager.Remove(user);

            //User user1 = userManager.GetById(2);
            //Console.WriteLine(user1.Username);
            //Console.WriteLine(user1.Password);

            ICollection<User> users = userManager.GetAllUsers();
            foreach(User u in users)
            {
                Console.WriteLine(u.Username + "  " + u.Password);
            }

            Console.WriteLine(userManager.VerifyUser("dsffsf", "sfgs"));
            Console.WriteLine(userManager.VerifyUser("safsdf", "fssfseg"));

            Console.ReadKey();
        }
    }
}
