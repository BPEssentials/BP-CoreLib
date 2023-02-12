using Newtonsoft.Json;

namespace BPCoreLib.Interfaces
{
    /// <summary>
    /// A class that provides methods for reading and writing JSON files.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public interface IReader<out TModel>
    {
        /// <summary>
        /// The path the file resides at. This is either a relative or absolute path.
        /// </summary>
        string Path { get; set; }

        /// <summary>
        /// The parsed file content.
        /// </summary>
        TModel Content { get; }

        /// <summary>
        /// Reads the content of the file and parses it to the specified type. Internally saves the parsed content to the <see cref="Content"/> property.
        /// </summary>
        /// <returns>The <see cref="Content"/> property, which is now populated.</returns>
        TModel Read();

        /// <summary>
        /// Writes the content back to the file.
        /// </summary>
        /// <param name="formatting">The formatting that should be used. By default is indented for verbosity sake.</param>
        void Write(Formatting formatting = Formatting.Indented);
    }
}
