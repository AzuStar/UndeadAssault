namespace UndeadAssault
{
    public abstract class CastableAbility : Ability
    {
        public float castTime;
        public abstract float cooldownFormula { get; }

        public virtual void CastAbility(Entity target) { }

        protected bool _casting = false;
    }
}
