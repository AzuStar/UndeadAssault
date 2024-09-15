using UnityEngine;

namespace UndeadAssault
{
    public class MeteoriteSecondaryAbility : SecondaryAbility
    {
        public SpecialEffect rangeIndicator;
        public float xOffset = 5f;
        public float heightOffset = 10f;
        public MeteoriteProjectile meteoriteProjectile;
        public override float cooldownFormula => cooldown / _stats.secondaryCdr;
        public float cooldown = 12f;

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
                if (
                    Physics.Raycast(
                        Camera.main.ScreenPointToRay(Input.mousePosition),
                        out RaycastHit hit,
                        1000f,
                        LayerMask.GetMask(GameConstants.LAYER_ENVIRONMENT)
                    )
                )
                {
                    LaunchMeteorite(hit.point);
                }
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
                    Instantiate(
                        rangeIndicator,
                        impactPoint + new Vector3(0, 0.1f, 0),
                        Quaternion.identity
                    );
                    _cdTimeout += cooldownFormula;
                    _casting = false;
                }
            );
        }
    }
}
