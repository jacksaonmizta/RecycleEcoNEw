﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RecycleEco.Model
{
    class Collector : User
    {
        public string Address { get; set; }

        public List<string> MaterialCollection { get; set; }
    }
}
