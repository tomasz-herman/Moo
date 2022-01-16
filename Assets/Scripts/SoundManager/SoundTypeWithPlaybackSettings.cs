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
    }
}
