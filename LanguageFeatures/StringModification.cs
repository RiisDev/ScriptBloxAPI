
namespace ScriptBloxApi.LanguageFeatures
{
    internal static class StringModification
    {
        internal static string GetBoolInt(this bool value) =>  value ? "1" : "0";
    }
}
