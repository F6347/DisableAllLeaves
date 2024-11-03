using BepInEx;
using Newtilla;
using System;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

namespace DisableAllLeaves
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>



    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {

        public int fallSiblings;

        void Start()
        {
            /* A lot of Gorilla Tag systems will not be set up when start is called /*
			/* Put code in OnGameInitialized to avoid null references */


            Newtilla.Newtilla.OnJoinModded += OnModdedJoined;
            Newtilla.Newtilla.OnLeaveModded += OnModdedLeft;
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

        void OnGameInitialized(object sender, EventArgs e)
        {
            
            
        }


        void OnModdedJoined(string modeName)
        {
            fallSiblings = GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/fallleaves (combined by EdMeshCombinerSceneProcessor)").transform.GetSiblingIndex();
            Console.WriteLine(fallSiblings);
            GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest").transform.GetChild(fallSiblings).gameObject.SetActive(false);
            GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest").transform.GetChild(fallSiblings+1).gameObject.SetActive(false);
            GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest").transform.GetChild(fallSiblings+2).gameObject.SetActive(false);
        }

        /* This attribute tells Newtilla to call this method when a modded room is left */

        void OnModdedLeft(string modeName)
        {

            GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest").transform.GetChild(fallSiblings).gameObject.SetActive(true);
            GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest").transform.GetChild(fallSiblings+1).gameObject.SetActive(true);
            GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest").transform.GetChild(fallSiblings+2).gameObject.SetActive(true);

            
        }
    }
}
