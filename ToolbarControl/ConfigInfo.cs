using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using KSP.UI.Screens;
using KSP.IO;

namespace ToolbarControl_NS
{
    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    public class ConfigInfo : MonoBehaviour
    {
        public static ConfigInfo Instance;
        const string SETTINGSNAME = "ToolbarController";

        static public bool debugMode = false;

        // KSPUtil.ApplicationRootPath internally calls get_dataPath, and :
        // "get_dataPath is not allowed to be called from a MonoBehaviour
        // constructor (or instance field initializer), call it in Awake
        // or Start instead."
        // As it's not very resource-intensive and we're not calling it all
        // that often anyway, let's make it a Property.
        static string PLUGINDATA
        {
            get { return KSPUtil.ApplicationRootPath + "GameData/001_ToolbarControl/PluginData/Debug.cfg"; }
        }

        void Start()
        {
            Instance = this;
            LoadData();
            DontDestroyOnLoad(this);
        }

        public void SaveData()
        {
            ConfigNode settingsFile = new ConfigNode();
            ConfigNode settings = new ConfigNode();

            settingsFile.SetNode(SETTINGSNAME, settings, true);
            settings.AddValue("debugMode", HighLogic.CurrentGame.Parameters.CustomParams<TC>().debugMode);
            settingsFile.Save(PLUGINDATA);
        }

        public void LoadData()
        {
            ConfigNode settingsFile = ConfigNode.Load(PLUGINDATA);
            ConfigNode node = null;
            if (settingsFile != null)
            {
                node = settingsFile.GetNode(SETTINGSNAME);
                if (node != null)
                {
                    if (node.HasValue("debugMode"))
                    {
                        debugMode = bool.Parse(node.GetValue("debugMode"));
                    }
                }
            }
            
        }
    }
}
