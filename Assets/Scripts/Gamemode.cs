using UnityEngine;

namespace UndeadAssault
{
    public class Gamemode : MonoBehaviour
    {
        public static Gamemode instance;
        public Entity hero;

        void Awake()
        {
            instance = this;
        }
    }
}
