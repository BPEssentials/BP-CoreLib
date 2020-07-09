using System;
using System.Collections.Generic;
using BPCoreLib.Interfaces;

namespace BPCoreLib.Util
{
    public class I18n : II18n
    {
        public IReader<Dictionary<string, Dictionary<string, string>>> Reader { get; } = new Reader<Dictionary<string, Dictionary<string, string>>>();

        public string Localize(string lang, string node, params object[] format)
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

        public string Localize(IFormatProvider formatProvoider ,string lang, string node, params object[] format)
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
                return string.Format(formatProvoider, local, format);
            }
            return local;
        }

        public void ParseLocalization(string path = "srv_localization.json")
        {
            Reader.Path = path;
            Reader.ReadAndParse();
        }

        public bool TryGetNodeByString(Dictionary<string, string> values, string node, out string local)
        {
            return values.TryGetValue(node, out local);
        }

        public bool TryGetValuesByLanguage(string lang, out Dictionary<string, string> values)
        {
            return Reader.Parsed.TryGetValue(lang, out values);
        }
    }
}
