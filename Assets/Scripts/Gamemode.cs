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

        public int floor = 0;
        public int baseFloorWeight = 8;
        public int multiFloorWeight = 2;
        public int floorWeight;

        void Awake()
        {
            instance = this;
            NextFloor();
        }

        public void NextFloor()
        {
            floor++;
            floorWeight = baseFloorWeight + (floor * multiFloorWeight);
        }
    }
}
