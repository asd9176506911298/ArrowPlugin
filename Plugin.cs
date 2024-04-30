using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using HarmonyLib;
using System;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace ArrowPlugin;

[BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
public class Plugin : BasePlugin
{
    public const string PLUGIN_GUID = "yuki.BepinEx.ArrowPlugin";
    public const string PLUGIN_NAME = "Yuki ArrowPlugin";
    public const string PLUGIN_VERSION = "1.0.0";

    public static int defaultValue = 1337;

    public static ConfigEntry<bool> debugPanel;
    public static ConfigEntry<int> Coin;
    public static ConfigEntry<float> Health;
    public static ConfigEntry<float> animationSpeed;
    public static ConfigEntry<int> projectileCount;
    public static ConfigEntry<float> projectileDamage;
    public static ConfigEntry<float> projectileDistance;
    public static ConfigEntry<float> projectileSpeed;

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

        debugPanel = Config.Bind<bool>("", "Developer Window", false, "");
        Coin = Config.Bind<int>("", "Coin", defaultValue, "");
        Health = Config.Bind<float>("", "Health", 400, "");
        animationSpeed = Config.Bind<float>("", "shootFrequency", 10, "");
        projectileCount = Config.Bind<int>("", "projectileCount", 1, "");
        projectileDamage = Config.Bind<float>("", "projectileDamage", 14, "");
        projectileDistance = Config.Bind<float>("", "projectileDistance", 10, "");
        projectileSpeed = Config.Bind<float>("", "projectileSpeed", 6, "");

        debugPanel.Value = false;
        Coin.Value = defaultValue;
        Health.Value = defaultValue;
        animationSpeed.Value = 10;
        projectileCount.Value = 1;
        projectileDamage.Value = 14;
        projectileDistance.Value = 10;
        projectileSpeed.Value = 6;

        debugPanel.SettingChanged += DebugPanel_SettingChanged;
        Coin.SettingChanged += Coin_SettingChanged;
        Health.SettingChanged += Health_SettingChanged;
        animationSpeed.SettingChanged += AnimationSpeed_SettingChanged;
        projectileCount.SettingChanged += ProjectileCount_SettingChanged;
        projectileDamage.SettingChanged += ProjectileDamage_SettingChanged;
        projectileDistance.SettingChanged += ProjectileDistance_SettingChanged;
        projectileSpeed.SettingChanged += ProjectileSpeed_SettingChanged;
        
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

    private void DebugPanel_SettingChanged(object sender, EventArgs e)
    {
        if (GameObject.Find("Canvas").transform.Find("debugPanel").gameObject)
        {
            GameObject.Find("Canvas").transform.Find("debugPanel").gameObject.SetActive(debugPanel.Value);
        }
        
    }

    private void Coin_SettingChanged(object sender, EventArgs e)
    {
        //Log.LogMessage($"Money:{GameDataManager.money} {CoinValue.Value}");
        GameDataManager.money = Coin.Value;
    }

    private void Health_SettingChanged(object sender, EventArgs e)
    {
        GameManager.Instance.playerInteraction.health = Health.Value;
    }

    private void AnimationSpeed_SettingChanged(object sender, EventArgs e)
    {
        GameManager.Instance.playerInteraction.animationSpeed = animationSpeed.Value;
        GameManager.Instance.playerInteraction.AddAnimatorSpeed(0);
    }

    private void ProjectileCount_SettingChanged(object sender, EventArgs e)
    {
        GameManager.Instance.playerInteraction.projectileCount = projectileCount.Value;
        GameManager.Instance.playerInteraction.AddProjectileCount(0);
    }

    private void ProjectileDamage_SettingChanged(object sender, EventArgs e)
    {
        GameManager.Instance.playerInteraction.projectileDamage = projectileDamage.Value;
    }


    private void ProjectileDistance_SettingChanged(object sender, EventArgs e)
    {
        GameManager.Instance.playerInteraction.projectileDistance = projectileDistance.Value;
    }

    private void ProjectileSpeed_SettingChanged(object sender, EventArgs e)
    {
        GameManager.Instance.playerInteraction.projectileSpeed = projectileSpeed.Value;
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
