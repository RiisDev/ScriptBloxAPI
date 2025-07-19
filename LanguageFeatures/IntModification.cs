namespace ScriptBloxApi.LanguageFeatures
{
    internal static class IntModification
    {
        internal static int? InternalClamp(this int? value, int min, int max) => value == null ? min : value < min ? min : value > max ? max : value;
    }
}
