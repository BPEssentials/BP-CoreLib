using BPCoreLib.Interfaces;
using Newtonsoft.Json;
using System.IO;

namespace BPCoreLib.Util
{
    public class Reader<Model> : IReader<Model>
    {
        public string Path { get; set; }

        public string FileContent { get; set; }

        public Model Parsed { get; set; }

        public Reader()
        {
        }

        public Reader(string path) : this()
        {
            Path = path;
        }

        public Model ReadAndParse()
        {
            ReadFile();
            return Parse();
        }

        public string ReadFile()
        {
            FileContent = File.ReadAllText(Path);
            return FileContent;
        }

        public Model Parse()
        {
            Parsed = JsonConvert.DeserializeObject<Model>(FileContent);
            return Parsed;
        }
    }
}
