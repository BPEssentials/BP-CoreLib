using System;
using System.Collections.Generic;

namespace BPCoreLib.Interfaces
{
    /// <summary>
    /// Provides internationalization support based on a singular file.
    /// </summary>
    public interface II18n
    {
        /// <summary>
        /// The reader that is used to read the language file.
        /// The most outer key is the language code as ISO 3166-1 alpha-2, the inner key is the key of the translation.
        /// </summary>
        IReader<Dictionary<string, Dictionary<string, string>>> Reader { get; }

        /// <summary>
        /// Localizes a specific node by the language provided. If any format arguments are provided, they will be used to format the string.
        /// </summary>
        /// <param name="lang">The language code as ISO 3166-1 alpha-2.</param>
        /// <param name="node">The translation node key.</param>
        /// <param name="format">Used to format the string. Can be used in the node value using {0} {1} tags.</param>
        /// <returns>The translated string, or null if either the language or node was not found.</returns>
        string Localize(string lang, string node, params object[] format);

        /// <inheritdoc cref="Localize(string,string,object[])"/>
        string Localize(IFormatProvider formatProvider, string lang, string node, params object[] format);

        /// <summary>
        /// Tries to fetch the values of a specific language.
        /// </summary>
        /// <param name="lang">The ISO 3166-1 alpha-2 language code.</param>
        /// <param name="values">The output values of the specified language. null if return value is false.</param>
        /// <returns>Returns true if found, otherwise false.</returns>
        bool TryGetValuesByLanguage(string lang, out Dictionary<string, string> values);

        /// <summary>
        /// Tries to fetch a singular node by a key. Output is the value of the node.
        /// </summary>
        /// <param name="values">The values, which should be provided by <see cref="TryGetValuesByLanguage"/>.</param>
        /// <param name="node">The key of the translation that should be fetched.</param>
        /// <param name="local">The raw (translated) value. Should be passed through a <see cref="string.Format(string, object[])"/> before displaying.</param>
        /// <returns>Returns true if found, otherwise false.</returns>
        bool TryGetNodeByString(Dictionary<string, string> values, string node, out string local);
    }
}
