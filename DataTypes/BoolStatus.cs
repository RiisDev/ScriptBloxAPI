namespace ScriptBloxAPI.DataTypes
{
    public class BoolStatus
    {
        public bool Value { get; }

        public string Status { get; }

        public BoolStatus(bool value, string status)
        {
            Value = value;
            Status = status;
        }
    }
}
