using UnityEngine;

namespace UndeadAssault
{
    public class SpecialEffect : MonoBehaviour
    {
        public float lifetime = 3f;

        void Start()
        {
            Destroy(gameObject, lifetime);
        }
    }
}
