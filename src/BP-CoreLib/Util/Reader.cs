using BPCoreLib.Interfaces;
using Newtonsoft.Json;
using System.IO;

namespace BPCoreLib.Util
{
    /// <inheritdoc cref="IReader{TModel}"/>
    public class Reader<TModel> : IReader<TModel>
    {
        /// <inheritdoc cref="IReader{TModel}.Path"/>
        public string Path { get; set; }
        
        /// <inheritdoc cref="IReader{TModel}.Content"/>
        public TModel Content { get; private set; }

        /// <inheritdoc cref="Reader{TModel}"/>
        public Reader()
        {
        }

        /// <inheritdoc cref="Reader{TModel}"/>
        /// <param name="path">The relative or absolute path of the file.</param>
        public Reader(string path) : this()
        {
            Path = path;
        }

        /// <inheritdoc cref="IReader{TModel}.Read"/>
        public TModel Read()
        {
            Content = JsonConvert.DeserializeObject<TModel>(File.ReadAllText(Path));
            return Content;
        }

        /// <inheritdoc cref="IReader{TModel}.Write"/>
        public void Write(Formatting formatting = Formatting.Indented)
        {
            File.WriteAllText(Path, JsonConvert.SerializeObject(Content, formatting));
        }
    }
}
