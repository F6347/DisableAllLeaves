using BepInEx;
using GorillaExtensions;
using System;
using UnityEngine;
using Utilla;

namespace DisableAllLeaves
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>

    /* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        bool inRoom;

        void Start()
        {
            /* A lot of Gorilla Tag systems will not be set up when start is called /*
			/* Put code in OnGameInitialized to avoid null references */

            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnEnable()
        {
            /* Set up your mod here */
            /* Code here runs at the start and whenever your mod is enabled*/

            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest").transform.GetChild(19).gameObject.SetActive(true);
            GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest").transform.GetChild(20).gameObject.SetActive(true);
            GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest").transform.GetChild(21).gameObject.SetActive(true);

            HarmonyPatches.RemoveHarmonyPatches();
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            int fallSiblings = GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/fallleaves (combined by EdMeshCombinerSceneProcessor)").transform.GetSiblingIndex();
        }

        void Update()
        {
            
        }

        /* This attribute tells Utilla to call this method when a modded room is joined */
        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            GameObject leaves = GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/");
            int fallSiblings = GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/fallleaves (combined by EdMeshCombinerSceneProcessor)").transform.GetSiblingIndex();
            
            leaves.transform.GetChild(fallSiblings).gameObject.SetActive(false);
            leaves.transform.GetChild(fallSiblings+1).gameObject.SetActive(false);
            leaves.transform.GetChild(fallSiblings+2).gameObject.SetActive(false);
            
            inRoom = true;
        }

        /* This attribute tells Utilla to call this method when a modded room is left */
        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest").transform.GetChild(19).gameObject.SetActive(true);
            GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest").transform.GetChild(20).gameObject.SetActive(true);
            GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest").transform.GetChild(21).gameObject.SetActive(true);

            inRoom = false;
        }
    }
}
