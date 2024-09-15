using System.Diagnostics;

namespace UndeadAssault
{
    public class Mage : Entity
    {
        public override void LevelUp(int times)
        {
            base.LevelUp(times);
            stats.baseAttack += 1 * times;
            stats.baseMaxHealth += 3 * times;
        }
    }
}
