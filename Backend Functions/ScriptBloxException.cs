using System;

namespace ScriptBloxAPI.Backend_Functions
{
    internal class ScriptBloxException : Exception
    {
        internal ScriptBloxException(string message) : base(message) => Console.WriteLine("ScriptBloxException was thrown with message: " + message);
    }
}
