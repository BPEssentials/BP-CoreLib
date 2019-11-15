using System.Collections.Generic;
using BPCoreLib.Interfaces;
using Newtonsoft.Json;

namespace BPCoreLib.Util
{
    public class I18n : II18n
    {
        public string File { get; set; } = "srv_localization.json";

        public Dictionary<string, Dictionary<string, string>> Languages { get; private set; } = new Dictionary<string, Dictionary<string, string>>();

        public string Localize(string lang, string node, params string[] format)
        {
            if (!TryGetValuesByLanguage(lang, out var values))
            {
                return null;
            }
            if (!TryGetNodeByString(values, node, out var local))
            {
                return null;
            }
            if (format.Length > 0)
            {
                return string.Format(local, format);
            }
            return local;
        }

        public void ParseLocalization()
        {
            Languages = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(File);
        }

        public bool TryGetNodeByString(Dictionary<string, string> values, string node, out string local)
        {
            return values.TryGetValue(node, out local);
        }

        public bool TryGetValuesByLanguage(string lang, out Dictionary<string, string> values)
        {
            return Languages.TryGetValue(lang, out values);
        }
    }
}
