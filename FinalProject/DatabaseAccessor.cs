﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject
{
    public class DatabaseAccessor
    {
        private static readonly SchoolEntities entities;

        static DatabaseAccessor()
        {
            entities = new SchoolEntities();
            entities.Database.Connection.Open();
        }

        public static SchoolEntities Instance
        {
            get
            {
                return entities;
            }
        }
    }
}