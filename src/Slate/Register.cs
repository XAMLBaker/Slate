using System;
using System.Collections.Generic;

namespace Slate
{
    public class Register
    {
        public Dictionary<string, Type> RegisterMap = new Dictionary<string, Type> ();
        public Type InitialLayout { get; set; }
    }
}
