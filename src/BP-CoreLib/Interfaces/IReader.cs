using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
