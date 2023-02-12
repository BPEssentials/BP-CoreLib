using BPCoreLib.Interfaces;
using BPCoreLib.Util;
using BrokeProtocol.API;
using System.Diagnostics;
using System.Reflection;

namespace BPCoreLib
{
    // TODO: New features: 
    //       - Composite design pattern
    //       - Command system (console?)
    //       - Better logger
    //       - cs/Roslyn file loader
    

    /// <summary>
    /// The main class of the plugin. Should not be manually invoked, even though it is public.
    /// </summary>
    public class Core : Plugin
    {
        /// <summary>
        /// Global logger.
        /// </summary>
        public ILogger Logger { get; } = new Logger();

        /// <inheritdoc cref="Core"/>
        public static Core Instance { get; private set; }

        /// <summary>
        /// The plugin's version. This is automatically set based on the <see cref="FileVersionInfo.FileVersion"/> of the assembly.
        /// </summary>
        public static string Version { get; } = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;

        /// <inheritdoc cref="Core"/>
        public Core()
        {
            Instance = this;
            Info = new PluginInfo("BPCoreLib", "CL")
            {
                Description = "Contains a list of helpful features for Broke Protocol plugins. Required core library by a few plugins.",
                Website = "https://github.com/BPEssentials/BP-CoreLib"
            };
            Logger.LogInfo($"BP CoreLib v{Version} loaded in successfully!");
        }
    }
}
