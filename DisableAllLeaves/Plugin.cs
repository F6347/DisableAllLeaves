using BepInEx;
using System;
using UnityEngine;
using Photon.Pun;

namespace DisableAllLeaves
{

    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        bool inRoom;
        int fallSiblings = 0;

        private GameObject Forest;

        void Update()
        {
            if (!PhotonNetwork.InRoom) OnModdedJoined();
            else if (!NetworkSystem.Instance.GameModeString.Contains("MODDED")) OnModdedLeft();
        }

        public void Start()
        {
            Forest = GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest");
        }

        void OnEnable()
        {
            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            Enl()
            HarmonyPatches.RemoveHarmonyPatches();
        }
        private void Enl()
        {
            Forest.transform.GetChild(fallSiblings).gameObject.SetActive(true);
            Forest.transform.GetChild(fallSiblings+1).gameObject.SetActive(true);
            Forest.transform.GetChild(fallSiblings+2).gameObject.SetActive(true);
        }
        private void Disl()
        {
            Forest.transform.GetChild(fallSiblings).gameObject.SetActive(true);
            Forest.transform.GetChild(fallSiblings+1).gameObject.SetActive(true);
            Forest.transform.GetChild(fallSiblings+2).gameObject.SetActive(true);
        }

        void OnModdedJoined()
        {
            if (inRoom) return;
            inRoom = true;
            fallSiblings = GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/fallleaves (combined by EdMeshCombinerSceneProcessor)").transform.GetSiblingIndex();
            Disl()
        }
        void OnModdedLeft()
        {
            if (!inRoom) return;
            inRoom = false;
            Enl()
        }
    }
}
