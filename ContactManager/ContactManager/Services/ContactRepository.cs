﻿using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactManager.Services
{
    public class ContactRepository
    {
        public Contact[] GetAllContacts()
        {
            return new Contact[]
            {
			    new Contact
			    {
				    Id = 1,
				    Name = "Glenn Block"
			    },
			    new Contact
			    {
				    Id = 2,
				    Name = "Dan Roth"
			    }
            };
        }
    }
}