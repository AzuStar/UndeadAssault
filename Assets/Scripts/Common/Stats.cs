using System;

namespace UndeadAssault
{
    [Serializable]
    public class Stats
    {
        public float experience;
        public float experienceToNextLevel =>
            experienceToNextLevelBase + experienceToNextLevelMultiplier * level;
        public float experienceToNextLevelBase;
        public float experienceToNextLevelMultiplier;
        public float doubleExpChance;
        public float experienceGranted;
        public int level;

        public double attack => baseAttack * (1 + bonusAttack);
        public double baseAttack;
        public double bonusAttack;

        public double maxHealth => baseMaxHealth * (1 + bonusMaxHealth);
        public double baseMaxHealth;
        public double bonusMaxHealth;
        public double secondaryDamageAmplifier;

        public double damageReduction;

        public double health;

        public float attackRange;
        public float primaryCdr;
        public float secondaryCdr;
        public float dashCdr;
        public float movementSpeed;
        public float angularSpeed;
    }
}
