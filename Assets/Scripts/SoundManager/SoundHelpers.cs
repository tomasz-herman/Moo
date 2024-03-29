﻿namespace Assets.Scripts.SoundManager
{
    public static class SoundHelpers
    {
        public static float GetVolumeForSoundType(SoundType soundType)
        {
            //TODO: this is temporary solution, when we will have all audio clips we can try to normalize them in audacity for example
            var volume = 1f;
            switch (soundType)
            {
                case SoundType.NoSound:
                    break;
                case SoundType.EnemyKilled:
                    volume = 1f;
                    break;
                case SoundType.GrenadeExplosion:
                    volume = 0.9f;
                    break;
                case SoundType.PistolShot:
                    volume = 0.25f;
                    break;
                case SoundType.GrenadeLauncherShot:
                    volume = 0.9f;
                    break;
                case SoundType.ShotgunShot:
                    volume = 0.9f;
                    break;
                case SoundType.SwordSwing:
                    volume = 0.8f;
                    break;
            }

            return volume;
        }

        public static SoundType GetRandomEnemyDeathSound()
        {
            var index = Utils.NumberBetween(0, EnemyDeathSoundTypes.Length - 1);
            return EnemyDeathSoundTypes[index];
        }

        public static SoundType[] GetEnemyHurtSoundTypes()
        {
            return EnemyHurtSoundTypes;
        }

        public static SoundType[] GetEnemyDeathSoundTypes()
        {
            return EnemyDeathSoundTypes;
        }

        public static PlaybackSettings GetEnemyHurtPlaybackSettings(SoundType soundType)
        {
            return new PlaybackSettings()
            {
                SpatialBlend = 1f,
                Volume = GetVolumeForSoundType(soundType),
                Pitch = 0.85f
            };
        }

        public static PlaybackSettings GetEnemyDeathPlaybackSettings(SoundType soundType)
        {
            return new PlaybackSettings()
            {
                SpatialBlend = 1f,
                Volume = GetVolumeForSoundType(soundType),
                Pitch = 0.85f
            };
        }

        private static readonly SoundType[] EnemyHurtSoundTypes = new SoundType[]
        {
            SoundType.enemy_hurt_001, SoundType.enemy_hurt_002, SoundType.enemy_hurt_003, SoundType.enemy_hurt_004, SoundType.enemy_hurt_005, SoundType.enemy_hurt_006, SoundType.enemy_hurt_007, SoundType.enemy_hurt_008, SoundType.enemy_hurt_009, SoundType.enemy_hurt_010, SoundType.enemy_hurt_011, SoundType.enemy_hurt_012, SoundType.enemy_hurt_013, SoundType.enemy_hurt_014, SoundType.enemy_hurt_015, SoundType.enemy_hurt_016, SoundType.enemy_hurt_017, SoundType.enemy_hurt_018, SoundType.enemy_hurt_019, SoundType.enemy_hurt_020, SoundType.enemy_hurt_021, SoundType.enemy_hurt_022
        };

        private static readonly SoundType[] EnemyDeathSoundTypes = new SoundType[]
        {
            SoundType.enemy_die_001, SoundType.enemy_die_002, SoundType.enemy_die_003, SoundType.enemy_die_004, SoundType.enemy_die_005, SoundType.enemy_die_006, SoundType.enemy_die_007, SoundType.enemy_die_008, SoundType.enemy_die_009
        };
    }
}
