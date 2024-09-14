using UnityEngine;

namespace UndeadAssault
{
    public class PlayerSpellController : MonoBehaviour
    {
        public PrimaryAbility primaryAbility;
        public SecondaryAbility secondaryAbility;

        void Update()
        {
            if (Input.GetMouseButton(0))
                primaryAbility.CastAbility(null);
            if (Input.GetMouseButton(1))
                secondaryAbility.CastAbility(null);
        }
    }
}
