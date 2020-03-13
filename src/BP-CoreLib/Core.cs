using BPCoreLib.Interfaces;
using BPCoreLib.Util;
using BrokeProtocol.API;
using System.Diagnostics;
using System.Reflection;

namespace BPCoreLib
{
    public class Core : Plugin
    {
        public ILogger Logger { get; } = new Logger();

        public static Core Instance { get; private set; }

        public static string Version { get; } = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;

        public Core()
        {
            Instance = this;
            Info = new PluginInfo("BPCoreLib", "CL")
            {
                Description = "Contains a list of helpful features for Broke Protocol plugins. Required core libary by a few plugins.",
                Website = "https://github.com/BPEssentials/BP-CoreLib"
            };
            Logger.LogInfo($"BP CoreLib {(IsDevelopmentBuild() ? "[DEVELOPMENT-BUILD] " : "")}v{Version} loaded in successfully!");
        }

        public static bool IsDevelopmentBuild()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }
}
