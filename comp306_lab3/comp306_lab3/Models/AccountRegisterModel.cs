﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace comp306_lab3.Models
{
    public class AccountRegisterModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}