using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using System;
using UnhollowerRuntimeLib;

namespace ArrowPlugin;

[BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
public class Plugin : BasePlugin
{
    public const string PLUGIN_GUID = "ArrowPlugin";
    public const string PLUGIN_NAME = "ArrowPlugin";
    public const string PLUGIN_VERSION = "1.0.0";

    public static Plugin Instance;

    public Plugin()
    {
        Instance = this;
    }

    public override void Load()
    {
        // Plugin startup logic
        Log.LogInfo($"Plugin {PLUGIN_GUID} is loaded!");
        try
        {
            ClassInjector.RegisterTypeInIl2Cpp<PluginManager>();
        }
        catch
        {
            Log.LogMessage("Failed to Registers Il2cpp Type");
        }

        try
        {
            var harmony = new Harmony(PLUGIN_GUID);
            var originalHandle = AccessTools.Method(typeof(UnityEngine.UI.CanvasScaler), "Handle");
            var postHandle = AccessTools.Method(typeof(BootstrapPatch), "Handle");
            harmony.Patch(originalHandle, postfix: new HarmonyMethod(postHandle));
        }
        catch
        {
            Log.LogMessage("Failed to Apply Hook");
        }
    }

    class BootstrapPatch
    {
        [HarmonyPostfix]
        static void Handle()
        {
            if (PluginManager.Instance != null) return;

            try
            {
                PluginManager.Create("PluginManager");
            }
            catch(Exception e)
            {
                Instance.Log.LogMessage($"ERROR Bootstrapping Trainer: {e.Message}");
            }
        }
    }
}
