using NHibernateInteractive.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Criterion;

namespace NHibernateInteractive.Manager
{
    class UserManager : IUserManager
    {
        public void Add(User user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using(ITransaction transaction= session.BeginTransaction())
                {
                    session.Save(user);
                    transaction.Commit();
                }
            }//using大括号内语句执行完会释放小括号内的资源
        }

        public ICollection<User> GetAllUsers()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(User));
                IList<User> users = criteria.List<User>();
                return users;
            }//using大括号内语句执行完会释放小括号内的资源
        }

        public Model.User GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    User user = session.Get<User>(id);
                    transaction.Commit();
                    return user;
                }
            }//using大括号内语句执行完会释放小括号内的资源
        }

        public User GetByUsername(string username)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(User));
                criteria.Add(Restrictions.Eq("Username", username));
                User user = criteria.UniqueResult<User>();
                return user;
            }//using大括号内语句执行完会释放小括号内的资源
        }

        public void Remove(Model.User user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(user);
                    transaction.Commit();
                }
            }//using大括号内语句执行完会释放小括号内的资源
        }

        public void Update(User user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(user);
                    transaction.Commit();
                }
            }//using大括号内语句执行完会释放小括号内的资源
        }

        public bool VerifyUser(string username, string password)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                User user = session.CreateCriteria(typeof(User))
                    .Add(Restrictions.Eq("Username", username))
                    .Add(Restrictions.Eq("Password", password))
                    .UniqueResult<User>();
                if (user == null) return false;
                return true;
            }//using大括号内语句执行完会释放小括号内的资源
        }
    }
}
