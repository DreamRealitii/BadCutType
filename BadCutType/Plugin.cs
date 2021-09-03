using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using UnityEngine.SceneManagement;
using UnityEngine;
using HarmonyLib;
using IPALogger = IPA.Logging.Logger;

namespace BadCutType
{

    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }
        private Harmony harmony;

        [Init]
        /// <summary>
        /// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
        /// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
        /// Only use [Init] with one Constructor.
        /// </summary>
        public void Init(IPALogger logger)
        {
            Instance = this;
            Log = logger;
            Log.Info("BadCutType initialized.");
        }

        #region BSIPA Config
        //Uncomment to use BSIPA's config
        /*
        [Init]
        public void InitWithConfig(Config conf)
        {
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Log.Debug("Config loaded");
        }
        */
        #endregion

        [OnStart]
        public void OnApplicationStart()
        {
            Log.Debug("OnApplicationStart");
            harmony = new Harmony("DreamReality.BadCutType");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            harmony.UnpatchAll(harmony.Id);
            Log.Debug("OnApplicationQuit");
        }

        public static string FailText(NoteCutInfo.FailReason reason) {
            switch (reason) {
                case NoteCutInfo.FailReason.WrongDirection: return "Direction";
                case NoteCutInfo.FailReason.WrongColor: return "Color";
                case NoteCutInfo.FailReason.TooSoon: return "Timing";
                case NoteCutInfo.FailReason.CutHarder: return "Swing";
                default: return "None";
            }
        }
    }
}
