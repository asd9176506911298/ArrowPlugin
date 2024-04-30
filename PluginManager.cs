using BepInEx.Configuration;
using BepInEx.IL2CPP.UnityEngine;
using BepInEx.Logging;
using System;
using UnityEngine;

namespace ArrowPlugin;

public class PluginManager : MonoBehaviour
{
    public static PluginManager Instance { get; private set; }
    public static ManualLogSource Log => Plugin.Instance.Log;

    public PluginManager(IntPtr ptr) : base(ptr)
    {
        Instance = this;
    }

    internal static GameObject Create(string name)
    {

        var gameObject = new GameObject(name);
        DontDestroyOnLoad(gameObject);

        var component = new PluginManager(gameObject.AddComponent(UnhollowerRuntimeLib.Il2CppType.Of<PluginManager>()).Pointer);

        return gameObject;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        //Log.LogMessage("Update");
        //Log.LogMessage($"Money: {GameDataManager.money}");
        if (GameManager.Instance)
        {
            if (GameManager.Instance.playerInteraction)
            {
                //public static ConfigEntry<float> animationSpeed;
                //public static ConfigEntry<int> projectileCount;
                //public static ConfigEntry<float> projectileDamage;
                //public static ConfigEntry<float> projectileDistance;
                //public static ConfigEntry<float> projectileSpeed;
                //AddAnimatorSpeed
                //Log.LogMessage($"GetProjectileSpeed:{PlayerInteraction.bowLevel}");
                //GameManager.Instance.playerInteraction.AddHealth(1);
                if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Keypad1))
                {
                    //GameManager.Instance.playerInteraction.health = 1000;
                    //GameManager.Instance.playerInteraction.animationSpeed = 100;
                    //GameManager.Instance.playerInteraction.AddAnimatorSpeed(0);
                    //GameManager.Instance.playerInteraction.projectileCount = 10;
                    //GameManager.Instance.playerInteraction.projectileDistance = 10;
                    //GameManager.Instance.playerInteraction.projectileSpeed = 10;
                    //GameManager.Instance.playerInteraction.AddAnimatorSpeed(0);
                    //Log.LogMessage($"GetProjectileFrequency: {GameManager.Instance.playerInteraction.GetProjectileFrequency()}");
                    //Log.LogMessage($"projectileCount:{GameManager.Instance.playerInteraction.projectileCount}");
                    //Log.LogMessage($"GetProjectileDamage:{GameManager.Instance.playerInteraction.GetProjectileDamage()}");
                    //Log.LogMessage($"GetProjectileDistance:{GameManager.Instance.playerInteraction.GetProjectileDistance()}");
                    //Log.LogMessage($"GetProjectileSpeed:{GameManager.Instance.playerInteraction.GetProjectileSpeed()}");
                    //Log.LogMessage(GameObject.Find("Canvas").transform.GetChild(11).gameObject.name);
                    //GameObject.Find("Canvas").transform.GetChild(11).gameObject.SetActive(true);

                    //Log.LogMessage(GameObject.Find("Canvas").transform.Find("debugPanel").gameObject.name);
                    //GameObject.Find("Canvas").transform.Find("debugPanel").gameObject.SetActive(true);

                    //PlayerInteraction.bowLevel += 1;

                    //GameManager.Instance.swordsManager.GenerateMultipleSwords(100);
                    //Log.LogMessage(FlySwordParentControl.cooldown);
                    //Log.LogMessage(FlySwordParentControl.moveSpeed);
                    //Log.LogMessage(FlySwordParentControl.damage);


                }

                if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Keypad2))
                {
                    //FlySwordParentControl.cooldown = 0;
                    //FlySwordParentControl.moveSpeed = 150;
                    //FlySwordParentControl.damage = 1;
                }
            }
                

        }
    }
}
