using BPCoreLib.Interfaces;
using System;
using System.Collections.Generic;

namespace BPCoreLib.Util
{
    /// <inheritdoc cref="II18n"/>
    public class I18n : II18n
    {
        /// <inheritdoc cref="II18n.Reader"/>
        public IReader<Dictionary<string, Dictionary<string, string>>> Reader { get; } = new Reader<Dictionary<string, Dictionary<string, string>>>();

        /// <inheritdoc cref="I18n"/>
        public I18n()
        {
        }

        /// <inheritdoc cref="I18n"/>
        /// <param name="path">The path that is used as i18n resource file.</param>
        public I18n(string path)
        {
            Reader.Path = path;
        }
        
        /// <inheritdoc cref="II18n.Localize(string,string,object[])"/>
        public string Localize(string lang, string node, params object[] format)
        {
            if (!TryGetValuesByLanguage(lang, out Dictionary<string, string> values))
            {
                return null;
            }
            if (!TryGetNodeByString(values, node, out string local))
            {
                return null;
            }

            return format.Length > 0 ? string.Format(local, format) : local;
        }

        /// <inheritdoc cref="Localize(string,string,object[])"/>
        public string Localize(IFormatProvider formatProvider, string lang, string node, params object[] format)
        {
            if (!TryGetValuesByLanguage(lang, out Dictionary<string, string> values))
            {
                return null;
            }
            if (!TryGetNodeByString(values, node, out string local))
            {
                return null;
            }

            return format.Length > 0 ? string.Format(formatProvider, local, format) : local;
        }
        
        /// <inheritdoc cref="II18n.TryGetValuesByLanguage"/>
        public bool TryGetValuesByLanguage(string lang, out Dictionary<string, string> values)
        {
            return Reader.Content.TryGetValue(lang, out values);
        }
        
        /// <inheritdoc cref="II18n.TryGetNodeByString"/>
        public bool TryGetNodeByString(Dictionary<string, string> values, string node, out string local)
        {
            return values.TryGetValue(node, out local);
        }
    }
}
