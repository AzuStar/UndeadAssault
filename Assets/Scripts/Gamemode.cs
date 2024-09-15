using UnityEngine;

namespace UndeadAssault
{
    public class Gamemode : MonoBehaviour
    {
        public static Gamemode instance;
        public Entity hero;

        public int floor = 1;

        void Awake()
        {
            instance = this;
        }
    }
}
