using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using LobbyCompatibility.Attributes;
using LobbyCompatibility.Enums;
using UnityEngine;

namespace JollyLethal;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency("BMX.LobbyCompatibility", BepInDependency.DependencyFlags.HardDependency)]
[LobbyCompatibility(CompatibilityLevel.ClientOnly, VersionStrictness.None)]
public class JollyLethal : BaseUnityPlugin
{
    public static JollyLethal Instance { get; private set; } = null!;
    internal new static ManualLogSource Logger { get; private set; } = null!;
    internal static Harmony? Harmony { get; set; }

    internal static AssetBundle? myCustomAssets;
    internal static GameObject mySantaHat;
    internal const string myJollyHatSpawnedObjName = "mishelin.JollyLethal.JollyHatParent";

    private void Awake()
    {
        Logger = base.Logger;
        Instance = this;

        LoadAssets();
        Patch();

        Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
    }

    internal void LoadAssets()
    {
        string assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        myCustomAssets = Helper.AssetLoader.AssetsAwake(assemblyLocation);
        if (myCustomAssets is null)
        {
            return;
        }
        mySantaHat = Helper.AssetLoader.LoadSantaHat(myCustomAssets);
    }

    internal static void Patch()
    {
        Harmony ??= new Harmony(MyPluginInfo.PLUGIN_GUID);

        Logger.LogDebug("Patching...");

        Harmony.PatchAll();

        Logger.LogDebug("Finished patching!");
    }

    internal static void Unpatch()
    {
        Logger.LogDebug("Unpatching...");

        Harmony?.UnpatchSelf();

        Logger.LogDebug("Finished unpatching!");
    } 

    internal static void PluginLogInfoWithPrefix(string content)
    {
        Logger.LogInfo($"[EVIL] {content}");
    }

    internal static void PluginLogErrorWithPrefix(string content)
    {
        Logger.LogError($"[EVIL] {content}");
    }
}
