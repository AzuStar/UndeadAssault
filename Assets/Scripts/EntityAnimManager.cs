using UnityEngine;

namespace UndeadAssault
{
    public class EntityAnimManager : MonoBehaviour
    {
        Animator animator;
        static float LerpSpeed = 10.0f;
        float currentX = 0.0f;
        float currentY = 0.0f;
        float targetX = 0.0f;
        float targetY = 0.0f;
        private NTimer _hitTimer;

        public void FirePrimaryAttack()
        {
            animator.SetTrigger(EntityAnimParameters.FirePrimaryAttack);
        }

        public void FireSecondaryAttack()
        {
            animator.SetTrigger(EntityAnimParameters.FireSecondaryAttack);
        }

        public void FinishSecondaryAttack()
        {
            animator.SetTrigger(EntityAnimParameters.FinishSecondaryAttack);
        }

        public void SetLocomotionVector(float x, float y)
        {
            targetX = x;
            targetY = y;
        }

        public void PlayDeath()
        {
            animator.SetTrigger(EntityAnimParameters.PlayDeath);
            animator.SetLayerWeight(EntityAnimLayers.Action, 0);
        }

        public void PlayHit()
        {
            if (_hitTimer != null)
            {
                _hitTimer.Cancel();
            }
            // unity made me do it
            if (animator.GetInteger(EntityAnimParameters.PlayHit) > 0)
            {
                animator.SetInteger(EntityAnimParameters.PlayHit, 0);
                _hitTimer = this.AttachNTimer(
                    0.01f,
                    () =>
                    {
                        animator.SetInteger(EntityAnimParameters.PlayHit, 1);
                    }
                );
            }
            else
            {
                animator.SetInteger(EntityAnimParameters.PlayHit, 1);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            currentX = Mathf.Lerp(currentX, targetX, Time.deltaTime * LerpSpeed);
            currentY = Mathf.Lerp(currentY, targetY, Time.deltaTime * LerpSpeed);
            animator.SetFloat(EntityAnimParameters.X, currentX);
            animator.SetFloat(EntityAnimParameters.Y, currentY);
        }
    }
}