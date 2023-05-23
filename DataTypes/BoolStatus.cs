using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptBloxAPI.DataTypes
{
    public class BoolStatus
    {
        private bool _value = false;
        private string _status = string.Empty;

        public bool Value => _value;
        public string Status => _status;

        public BoolStatus(bool value, string status)
        {
            _value = value;
            _status = status;
        }
    }
}
