namespace UndeadAssault
{
    public abstract class CastableAbility : Ability
    {
        public abstract double cooldownFormula { get; }
        public abstract void CastAbility();
    }
}
