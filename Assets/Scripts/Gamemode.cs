using UnityEditor.SearchService;
using UnityEngine;

namespace UndeadAssault
{
    public class Gamemode : MonoBehaviour
    {
        public static Gamemode instance;
        public Entity hero;

        public Entity[] enemyTypes;
        public string[] floorScenes;

        public int floor = 1;
        public int baseFloorWeight = 10;
        public int floorWeight = 20;

        void Awake()
        {
            instance = this;
        }

        public void NextFloor()
        {
            floor++;
            floorWeight = baseFloorWeight + (floor * 6);
        }
    }
}
