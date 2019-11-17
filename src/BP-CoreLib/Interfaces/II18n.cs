using System.Collections.Generic;

namespace BPCoreLib.Interfaces
{
    public interface II18n
    {
        IReader<Dictionary<string, Dictionary<string, string>>> Reader { get; }

        string Localize(string lang, string node, params string[] format);

        bool TryGetValuesByLanguage(string lang, out Dictionary<string, string> values);

        bool TryGetNodeByString(Dictionary<string, string> values, string node, out string local);

        void ParseLocalization(string path);
    }
}
