using Content.Shared.Actions.ActionTypes;
using Robust.Shared.Audio;

namespace Content.Shared.Abilities.Psionics
{
    [RegisterComponent]
    public sealed class PsychokinesisPowerComponent : Component
    {
        public WorldTargetAction? PsychokinesisPowerAction = null;

        [DataField("waveSound")]
        public SoundSpecifier WaveSound = new SoundPathSpecifier("/Audio/Nyanotrasen/Mobs/SilverGolem/wave.ogg");

        /// <summary>
        /// Volume control for the spell.
        /// </summary>
        [DataField("waveVolume")]
        public float WaveVolume = 5f;
    }

}
