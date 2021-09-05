using System.Reflection;
using BeatSaberMarkupLanguage.Settings;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using BadCutType.Configuration;
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
        [Init]
        public void InitWithConfig(Config conf) {
            PluginConfig.Instance = conf.Generated<PluginConfig>();
            BSMLSettings.instance.AddSettingsMenu("Bad Cut Type", "BadCutType.Views.settings.bsml", Configuration.PluginConfig.Instance);
            Log.Debug("Config loaded");
        }
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

        //Convert a FailReason into a string to display in-game.
        public static string FailText(NoteCutInfo.FailReason reason) {
            switch (reason) {
                case NoteCutInfo.FailReason.WrongDirection: return PluginConfig.Instance.directionFail;
                case NoteCutInfo.FailReason.WrongColor: return PluginConfig.Instance.colorFail;
                case NoteCutInfo.FailReason.TooSoon: return PluginConfig.Instance.timingFail;
                case NoteCutInfo.FailReason.CutHarder: return PluginConfig.Instance.swingFail;
                default: return "None";
            }
        }
    }
}
