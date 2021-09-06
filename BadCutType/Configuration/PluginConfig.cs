using System.Runtime.CompilerServices;
using IPA.Config.Stores;
using UnityEngine;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace BadCutType.Configuration
{
    internal class PluginConfig
    {
        public static PluginConfig Instance { get; set; }
        public virtual bool isEnabled { get; set; } = true;
        public virtual Color textColor { get; set; } = Color.red;
        public virtual float textSize { get; set; } = 3f;
        public virtual string directionFail { get; set; } = "Direction";
        public virtual string colorFail { get; set; } = "Color";
        public virtual string timingFail { get; set; } = "Timing";
        public virtual string swingFail { get; set; } = "Swing";
        public virtual string bombFail { get; set; } = "Bomb";

        /// <summary>
        /// This is called whenever BSIPA reads the config from disk (including when file changes are detected).
        /// </summary>
        public virtual void OnReload()
        {
            // Do stuff after config is read from disk.
        }

        /// <summary>
        /// Call this to force BSIPA to update the config file. This is also called by BSIPA if it detects the file was modified.
        /// </summary>
        public virtual void Changed()
        {
            
        }

        /// <summary>
        /// Call this to have BSIPA copy the values from <paramref name="other"/> into this config.
        /// </summary>
        public virtual void CopyFrom(PluginConfig other)
        {
            // This instance's members populated from other
        }
    }
}
