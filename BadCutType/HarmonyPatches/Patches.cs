using HarmonyLib;
using IPA.Utilities;
using UnityEngine;
using Zenject;

//Thanks CustomMissText for doing all the thinking for me.
namespace BadCutType.HarmonyPatches
{
    [HarmonyPatch(typeof(BadNoteCutEffectSpawner), nameof(BadNoteCutEffectSpawner.HandleNoteWasCut), MethodType.Normal)]
    internal class BadCutTextSpawner_HandleNoteWasMissed
    {
        public static bool inMethod = false;
        static FlyingTextSpawner _spawner;
        static FlyingTextSpawner _spawnerBase
        {
            get
            {
                if (_spawner != null) return _spawner;
                else
                {
                    _spawner = new GameObject("BadCutTextSpawner").AddComponent<FlyingTextSpawner>();

                    var installers = Object.FindObjectsOfType<MonoInstallerBase>();
                    foreach (var installer in installers)
                    {
                        var container = installer.GetProperty<DiContainer, MonoInstallerBase>("Container");

                        if (container != null && container.HasBinding<FlyingSpriteEffect.Pool>())
                        {
                            container.Inject(_spawner);
                            break;
                        }
                    }
                    _spawner.SetField("_fontSize", 2f);
                    _spawner.SetField("_color", Color.red);

                    return _spawner;
                }
            }
        }

        [HarmonyPrefix]
        static bool Prefix(BadNoteCutEffectSpawner __instance, NoteController noteController, in NoteCutInfo noteCutInfo)
        {
            if (_spawnerBase == null) {
                Plugin.Log.Error("Failed to inject FlyingTextSpawner!");
                return true;
            }
            if (Configuration.PluginConfig.Instance.isEnabled && !noteCutInfo.allIsOK)
                _spawner.SpawnText(noteCutInfo.cutPoint, noteController.worldRotation, noteController.inverseWorldRotation, Plugin.FailText(noteCutInfo.failReason));
            return false;
        }
    }
}