using System;
using System.Collections.Generic;

namespace Slate
{
    public static class ModuleContainer
    {
        public static Dictionary<string, Type> RegisterType = new Dictionary<string, Type> ();
    }
}