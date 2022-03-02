using System;

namespace Assets.Scripts.SoundManager
{
    [Serializable]
    public class SoundTypeWithPlaybackSettings
    {
        public SoundType SoundType;
        public PlaybackSettings PlaybackSettings;

        public SoundTypeWithPlaybackSettings()
        {
            SoundType = SoundType.NoSound;
            PlaybackSettings = new PlaybackSettings();
        }

        public SoundTypeWithPlaybackSettings Clone()
        {
            return new SoundTypeWithPlaybackSettings()
            {
                SoundType = this.SoundType,
                PlaybackSettings = this.PlaybackSettings.Clone()
            };
        }
    }
}
