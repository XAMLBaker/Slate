using System;
using System.Collections.Generic;

namespace FlexMVVM
{
    public class Register
    {
        public Dictionary<string, Type> RegisterMap = new Dictionary<string, Type> ();
        public Type NestedLayout { get; set; }
        public Type InitialLayout { get; set; }
    }
}
