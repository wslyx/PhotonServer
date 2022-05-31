﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using MyGameServer.Model;

namespace MyGameServer
{
    class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;
        private static ISessionFactory SessionFactory
        {
            get
            {
                if(_sessionFactory==null)
                {
                    var configurtion = new Configuration();
                    configurtion.Configure();
                    configurtion.AddAssembly("MyGameServer");
                    _sessionFactory = configurtion.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }
        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
