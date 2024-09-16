using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace UndeadAssault
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class Entity : MonoBehaviour
    {
        public AudioClip[] takeDamageSounds;
        public AudioClip[] deathSounds;

        public bool invulnerable = false;
        public int weight = 1;
        public Stats stats = new Stats();
        public EntityAnimManager _animManager;
        public bool isDead = false;
        private AudioSource _audioSource;

        void Start()
        {
            _animManager = GetComponent<EntityAnimManager>();
            _audioSource = GetComponent<AudioSource>();
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
            if (target.takeDamageSounds.Length > 0)
            {
                target._audioSource.PlayOneShot(
                    target.takeDamageSounds[
                        UnityEngine.Random.Range(0, target.takeDamageSounds.Length)
                    ]
                );
            }
            if (target.invulnerable)
            {
                return;
            }
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
            GetComponents<Collider>().ToList().ForEach(collider => Destroy(collider));

            if (_animManager != null)
            {
                _animManager.PlayDeath();
                if (deathSounds.Length > 0)
                {
                    _audioSource.PlayOneShot(
                        deathSounds[UnityEngine.Random.Range(0, deathSounds.Length)]
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
