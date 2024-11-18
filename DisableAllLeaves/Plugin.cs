using BepInEx;
using System;
using UnityEngine;
using Photon.Pun;

namespace DisableAllLeaves
{

    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        int fallSiblings = 0;

        void Update()
        {
            if (!PhotonNetwork.InRoom) OnModdedJoined();
            else if (!NetworkSystem.Instance.GameModeString.Contains("MODDED")) OnModdedLeft();
        }

        void OnEnable()
        {
            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest").transform.GetChild(fallSiblings).gameObject.SetActive(true);
            GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest").transform.GetChild(fallSiblings+1).gameObject.SetActive(true);
            GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest").transform.GetChild(fallSiblings+2).gameObject.SetActive(true);

            HarmonyPatches.RemoveHarmonyPatches();
        }



        void OnModdedJoined()
        {
            fallSiblings = GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/fallleaves (combined by EdMeshCombinerSceneProcessor)").transform.GetSiblingIndex();
            GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest").transform.GetChild(fallSiblings).gameObject.SetActive(false);
            GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest").transform.GetChild(fallSiblings+1).gameObject.SetActive(false);
            GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest").transform.GetChild(fallSiblings+2).gameObject.SetActive(false);
        }
        void OnModdedLeft()
        {

            GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest").transform.GetChild(fallSiblings).gameObject.SetActive(true);
            GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest").transform.GetChild(fallSiblings+1).gameObject.SetActive(true);
            GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest").transform.GetChild(fallSiblings+2).gameObject.SetActive(true);

            
        }
    }
}
