using System.IO;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using LobbyCompatibility.Attributes;
using LobbyCompatibility.Enums;
using UnityEngine;

namespace JollyLethal;

internal static class AssetLoader
{
    const string ASSET_BUNDLE_NAME = "lethal-mod-asset";  

    public static AssetBundle? AssetsAwake(string assemblyPath)
    {
        if (string.IsNullOrEmpty(assemblyPath))
        {
            JollyLethal.PluginLogErrorWithPrefix("Invalid assebmly path!!!");
            return null;
        }

        AssetBundle? customAssets = AssetBundle.LoadFromFile(Path.Combine(assemblyPath, ASSET_BUNDLE_NAME));
        if (customAssets is null)
        {
            JollyLethal.PluginLogErrorWithPrefix("Failed to load custom assets !!!");
            return null;
        }
        JollyLethal.PluginLogInfoWithPrefix("Successfully loaded custom assets.");

        return customAssets;
    }

    public static GameObject LoadSantaHat(AssetBundle customAssets)
    {
        GameObject asset = customAssets.LoadAsset<GameObject>("assets/hatparent.prefab");
        JollyLethal.PluginLogInfoWithPrefix("Successfully loaded in santa hat");
        return asset;
    }
}
