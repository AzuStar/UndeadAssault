using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace UndeadAssault
{
    public abstract class Entity : MonoBehaviour
    {
        public int weight = 1;
        public Stats stats = new Stats();
        public EntityAnimManager _animManager;
        public GameObject deathSoundPrefab;
        public bool isDead = false;

        void Start()
        {
            _animManager = GetComponent<EntityAnimManager>();
        }

        protected virtual void Awake()
        {
            stats.health = stats.maxHealth;
        }

        public void AddExperience(float amount)
        {
            stats.experience += amount;
            int levelups = 0;
            while (true)
            {
                float expNext = stats.experienceToNextLevel;
                if (expNext != 0 && stats.experience >= expNext)
                {
                    stats.experience -= expNext;
                    stats.level++;
                    levelups++;
                }
                else
                {
                    break;
                }
            }
            if (levelups > 0)
            {
                LevelUp(levelups);
            }
        }

        public virtual void LevelUp(int times) { }

        public void DealDamage(Entity target, double damage)
        {
            target._animManager.PlayHit();
            target.stats.health -= damage;
            if (target.stats.health <= 0)
            {
                AddExperience(target.stats.experienceGranted);
                target.Die();
            }
        }

        public void Die()
        {
            isDead = true;
            GetComponents<CastableAbility>().ToList().ForEach(ability => Destroy(ability));
            var collider = GetComponent<CapsuleCollider>();
            if (collider)
            {
                collider.enabled = false;
            }
            if (_animManager != null)
            {
                _animManager.PlayDeath();
                if (name == "Mage")
                {
                    //
                }
                else
                {
                    if (deathSoundPrefab != null)
                    {
                        var obj = Instantiate(
                            deathSoundPrefab,
                            transform.position,
                            transform.rotation
                        );
                        this.AttachNTimer(
                            2.0f,
                            () =>
                            {
                                Destroy(obj);
                            }
                        );
                    }
                }
                var navMeshAgent = GetComponent<NavMeshAgent>();
                if (navMeshAgent)
                {
                    //
                }
                else
                {
                    var obj = Instantiate(deathSoundPrefab, transform.position, transform.rotation);
                    this.AttachNTimer(
                        2.0f,
                        () =>
                        {
                            Destroy(obj);
                        }
                    );
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
