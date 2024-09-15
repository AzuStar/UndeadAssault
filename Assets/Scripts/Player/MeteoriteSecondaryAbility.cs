using UnityEngine;

namespace UndeadAssault
{
    public class MeteoriteSecondaryAbility : SecondaryAbility
    {
        public float xOffset = 5f;
        public float heightOffset = 10f;
        public MeteoriteProjectile meteoriteProjectile;
        public override float cooldownFormula => 12f / _stats.secondaryCdr;
        private float _cdTimeout;
        private Stats _stats;

        void Start()
        {
            _stats = GetComponent<Entity>().stats;
        }

        void Update()
        {
            RefreshText();
            if (_casting)
                return;
            if (_cdTimeout > 0)
                _cdTimeout -= Time.deltaTime;
        }

        public void RefreshText()
        {
            HudSkillTrackerSingletonGroup.instance.secondarySkillTracker.SetCooldown(
                _cdTimeout,
                cooldownFormula
            );
        }

        public override void CastAbility(Entity target)
        {
            if (_cdTimeout <= 0 && !_casting)
            {
                // mouse to ray, only look for environment layer
                Vector3? impactPoint = null;

                if (
                    Physics.Raycast(
                        Camera.main.ScreenPointToRay(Input.mousePosition),
                        out RaycastHit hit,
                        1000f
                    )
                )
                {
                    if (hit.collider.tag == GameConstants.TAG_ENVIRONMENT)
                        impactPoint = hit.point;
                }
                if (impactPoint != null)
                    LaunchMeteorite(impactPoint.Value);
            }
        }

        public void LaunchMeteorite(Vector3 impactPoint)
        {
            _casting = true;
            this.AttachNTimer(
                castTime / _stats.primaryCdr,
                () =>
                {
                    float sign = Random.value > 0.5f ? 1 : -1;
                    Vector3 spawnPoint = impactPoint + new Vector3(xOffset * sign, heightOffset, 0);
                    MeteoriteProjectile proj = Instantiate(
                        meteoriteProjectile,
                        spawnPoint,
                        transform.rotation
                    );
                    proj.impactPoint = impactPoint;
                    proj.owner = GetComponent<Entity>();
                    _cdTimeout += cooldownFormula;
                    _casting = false;
                }
            );
        }
    }
}
