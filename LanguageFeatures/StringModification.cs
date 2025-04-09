
namespace ScriptBloxApi.LanguageFeatures
{
    internal static class StringModification
    {
        internal static string GetBoolInt(this bool? value) => value.HasValue ? (value.Value ? "1" : "0") : "";
    }
}
