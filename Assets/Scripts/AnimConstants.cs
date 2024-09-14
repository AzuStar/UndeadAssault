namespace UndeadAssault
{
    static class EntityAnimParameters
    {
        // floats

        public const string X = "X";
        public const string Y = "Y";

        // triggers

        public const string FirePrimaryAttack = "FirePrimaryAttack";
        public const string FireSecondaryAttack = "FireSecondaryAttack";
        public const string FinishSecondaryAttack = "FinishSecondaryAttack";
        public const string PlayDeath = "PlayDeath";
        public const string PlayHit = "PlayHit";
    }

    static class EntityAnimLayers
    {
        public const int Base = 0;
        public const int Action = 1;
    }
}