namespace BPCoreLib.Interfaces
{
    public interface IReader<Model>
    {
        string Path { get; set; }

        string FileContent { get; set; }

        Model Parsed { get; set; }

        Model ReadAndParse();

        string ReadFile();

        Model Parse();
    }
}
