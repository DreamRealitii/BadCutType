using HarmonyLib;
using IPA.Utilities;
using UnityEngine;
using Zenject;
using BadCutType.Configuration;

//Thanks CustomMissText for doing all the hard coding (_spawnerBase.get) for me.
namespace BadCutType.HarmonyPatches
{
    [HarmonyPatch(typeof(BadNoteCutEffectSpawner), nameof(BadNoteCutEffectSpawner.HandleNoteWasCut), MethodType.Normal)]
    internal class BadCutTextSpawner_HandleNoteWasMissed
    {
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
                    _spawner.SetField("_fontSize", PluginConfig.Instance.textSize);
                    _spawner.SetField("_color", PluginConfig.Instance.textColor);

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
            //My code that generates bad cut text.
            if (PluginConfig.Instance.isEnabled && !noteCutInfo.allIsOK)
                _spawner.SpawnText(noteCutInfo.cutPoint, noteController.worldRotation, noteController.inverseWorldRotation,
                    Plugin.FailText(noteCutInfo.failReason));
            return !PluginConfig.Instance.isEnabled;
        }
    }
}