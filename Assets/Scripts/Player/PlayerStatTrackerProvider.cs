using UnityEngine;

namespace UndeadAssault
{
    public class PlayerStatTrackerProvider : MonoBehaviour
    {
        private Stats _stats;

        void Start()
        {
            _stats = GetComponent<Entity>().stats;
        }

        void Update()
        {
            RefreshStats();
        }

        public void RefreshStats()
        {
            HudStatTrackerSingletonGroup.instance.healthTracker.SetStatText(
                (float)_stats.health,
                (float)_stats.maxHealth
            );
        }
    }
}
