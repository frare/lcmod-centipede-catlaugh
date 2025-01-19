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
    internal const string PLUGIN_VERSION = "1.0.0";

    // singleton
    internal static CentipedeCatLaughBase Instance;

    // for debugging
    internal ManualLogSource logger;

    // harmony instance
    private readonly Harmony harmony = new(PLUGIN_GUID);

    internal AudioClip snareClip;
    public static AudioClip SnareClip { get { return Instance.snareClip; } private set { } }

    private void Awake()
    {
        if (Instance == null) Instance = this;

        logger = BepInEx.Logging.Logger.CreateLogSource(PLUGIN_GUID);
        logger.LogInfo($"Mod started! :)");

        LoadAssets();

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
        snareClip = assetBundle.LoadAsset<AudioClip>(path);

        if (snareClip == null)
            logger.LogError("Failed to load custom audio");
        else
            logger.LogDebug("Custom audio loaded!");
    }

    public static void LogMessage(string message, LogLevel logLevel = LogLevel.Debug)
    {
        Instance.logger.Log(logLevel, message);
    }
}
