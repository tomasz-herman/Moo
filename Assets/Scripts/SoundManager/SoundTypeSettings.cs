namespace Assets.Scripts.SoundManager
{
    public static class SoundTypeSettings
    {
        public static float GetVolumeForSoundType(SoundType soundType)
        {
            //TODO: this is temporary solution, when we will have all audio clips we can try to normalize them in audacity for example
            var volume = 1f;
            switch (soundType)
            {
                case SoundType.NoSound:
                    break;
                case SoundType.LaserShot:
                    volume = 0.5f;
                    break;
                case SoundType.EnemyKilled:
                    volume = 1f;
                    break;
                case SoundType.GrenadeExplosion:
                    volume = 0.4f;
                    break;
                case SoundType.BackgroundTheme:
                    break;
                case SoundType.MusicRoom:
                    break;
                case SoundType.Music1:
                    break;
                case SoundType.Music2:
                    break;
                case SoundType.Music3:
                    break;
                case SoundType.Music4:
                    break;
                case SoundType.Music5:
                    break;
            }

            return volume;
        }
    }
}
