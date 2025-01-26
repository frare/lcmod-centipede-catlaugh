using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace CentipedeCatLaugh;

[BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
public class CentipedeCatLaughBase : BaseUnityPlugin
{
    // plugin info
    internal const string PLUGIN_GUID = "frare.CentipedeCatLaugh";
    internal const string PLUGIN_NAME = "Centipede laughing cat grab";
    internal const string PLUGIN_VERSION = "1.2.0";

    // singleton
    internal static CentipedeCatLaughBase Instance;

    // for debugging
    internal ManualLogSource logger;

    // harmony instance
    private readonly Harmony harmony = new(PLUGIN_GUID);

    // cfg file options
    public static float LocalPlayerClipVolume { get; private set; }
    public static float OtherPlayerClipVolume { get; private set; }

    // mod audioclip reference
    public static AudioClip SnareClip { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;

        logger = BepInEx.Logging.Logger.CreateLogSource(PLUGIN_GUID);
        logger.LogInfo($"Mod started! :)");

        LoadAssets();
        LoadFromConfig();

        harmony.PatchAll();

        logger.LogInfo($"Mod finished loading!");
    }

    private void LoadAssets()
    {
        // load asset bundle
        var sAssemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var assetBundle = AssetBundle.LoadFromFile(Path.Combine(sAssemblyLocation, "modaudio"));
        if (assetBundle == null)
        {
            logger.LogError("Failed to load custom audio");
            return;
        }

        var path = assetBundle.GetAllAssetNames()[0];
        SnareClip = assetBundle.LoadAsset<AudioClip>(path);

        if (SnareClip == null)
            logger.LogError("Failed to load custom audio");
        else
            logger.LogDebug("Custom audio loaded!");
    }

    private void LoadFromConfig()
    {
        LocalPlayerClipVolume = Config.Bind("General", "SelfGrabbedVolume", 1f, "Volume that the clip will play when YOU are grabbed\n0 is muted, 1 is default, 1.5 is 150% the clip's original volume, etc.").Value;
        OtherPlayerClipVolume = Config.Bind("General", "OtherGrabbedVolume", 1.5f, "Volume that the clip will play when OTHERS are grabbed\n0 is muted, 1 is default, 1.5 is 150% the clip's original volume, etc.").Value;
    }

    public static void LogMessage(string message, LogLevel logLevel = LogLevel.Debug)
    {
        Instance.logger.Log(logLevel, message);
    }
}
