using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ScriptBloxAPI
{
    internal class ScriptBloxException : Exception
    {
        internal ScriptBloxException(string message) : base(message) => Console.WriteLine("ScriptBloxException was thrown with message: " + message);
    }
}
