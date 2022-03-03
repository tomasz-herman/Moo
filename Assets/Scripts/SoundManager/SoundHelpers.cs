namespace Assets.Scripts.SoundManager
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

        private static readonly SoundType[] EnemyHurtSoundTypes = new SoundType[]
        {
            SoundType.enemy_hurt_001, SoundType.enemy_hurt_002, SoundType.enemy_hurt_003, SoundType.enemy_hurt_004, SoundType.enemy_hurt_005, SoundType.enemy_hurt_006, SoundType.enemy_hurt_007, SoundType.enemy_hurt_008, SoundType.enemy_hurt_009, SoundType.enemy_hurt_010, SoundType.enemy_hurt_011, SoundType.enemy_hurt_012, SoundType.enemy_hurt_013, SoundType.enemy_hurt_014, SoundType.enemy_hurt_015, SoundType.enemy_hurt_016, SoundType.enemy_hurt_017, SoundType.enemy_hurt_018, SoundType.enemy_hurt_019, SoundType.enemy_hurt_020, SoundType.enemy_hurt_021, SoundType.enemy_hurt_022, SoundType.enemy_hurt_023, SoundType.enemy_hurt_024, SoundType.enemy_hurt_025, SoundType.enemy_hurt_026, SoundType.enemy_hurt_027, SoundType.enemy_hurt_028, SoundType.enemy_hurt_029, SoundType.enemy_hurt_030, SoundType.enemy_hurt_031, SoundType.enemy_hurt_032
        };

        private static readonly SoundType[] EnemyDeathSoundTypes = new SoundType[]
        {
            SoundType.enemy_die_001, SoundType.enemy_die_002, SoundType.enemy_die_003, SoundType.enemy_die_004, SoundType.enemy_die_005, SoundType.enemy_die_006, SoundType.enemy_die_007, SoundType.enemy_die_008, SoundType.enemy_die_009, SoundType.enemy_die_010, SoundType.enemy_die_011, SoundType.enemy_die_012, SoundType.enemy_die_013, SoundType.enemy_die_014, SoundType.enemy_die_015, SoundType.enemy_die_016, SoundType.enemy_die_017
        };
    }
}
