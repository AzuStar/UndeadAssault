namespace UndeadAssault
{
    static class EntityAnimParameters
    {
        // ints

        public const string PlayHit = "PlayHit";

        // floats

        public const string X = "X";
        public const string Y = "Y";

        // triggers

        public const string FirePrimaryAttack = "FirePrimaryAttack";
        public const string FireSecondaryAttack = "FireSecondaryAttack";
        public const string FinishSecondaryAttack = "FinishSecondaryAttack";
        public const string PlayDeath = "PlayDeath";
    }

    static class EntityAnimLayers
    {
        public const int Base = 0;
        public const int Action = 1;
    }
}