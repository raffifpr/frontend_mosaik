﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Mosaik.id.Model
{
    public class RegisterResponse
    {
        // status = ("success", "failed", "email already exist")
        public string status { get; set; }
    }
}
