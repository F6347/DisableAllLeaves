using BepInEx;
using Newtilla;
using UnityEngine;

namespace DisableAllLeaves
{
    [BepInDependency("Lofiat.Newtilla", "1.1.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    [BepInIncompatibility("org.iidk.gorillatag.iimenu")]//screw you iidk
    [BepInIncompatibility("com.goldentrophy.gorillatag.nametags")]//no1 loves you
    [BepInIncompatibility("com.dedouwe26.gorillatag.cosmetx")]//follow some laws 👍
    public class Plugin : BaseUnityPlugin
    {
        public int fallSiblings;

        void Start()
        {
            Newtilla.Newtilla.OnJoinModded += OnModdedJoined;
            Newtilla.Newtilla.OnLeaveModded += OnModdedLeft;
        }

        void OnEnable()
        {
            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            SetLeavesActive(true);
            HarmonyPatches.RemoveHarmonyPatches();
        }

        void OnModdedJoined(string modeName)
        {
            fallSiblings = GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/fallleaves (combined by EdMeshCombinerSceneProcessor)").transform.GetSiblingIndex();
            SetLeavesActive(false);
        }

        void OnModdedLeft(string modeName)
        {
            SetLeavesActive(true);
        }

        void SetLeavesActive(bool isActive)
        {
            var forest = GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest").transform;
            for (int i = 0; i < 3; i++)
                forest.GetChild(fallSiblings + i).gameObject.SetActive(isActive);
        }
    }
}
