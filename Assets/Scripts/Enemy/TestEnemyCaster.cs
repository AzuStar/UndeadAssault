using UnityEngine;

namespace UndeadAssault
{
    public class TestEnemyCaster : MonoBehaviour
    {
        public CastableAbility abilityToCast;

        void Update()
        {
            if (abilityToCast != null)
                abilityToCast.CastAbility(null);
        }
    }
}
