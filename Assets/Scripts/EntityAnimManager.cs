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
            // bool forward = Input.GetKey("w");
            // bool left = Input.GetKey("a");
            // bool back = Input.GetKey("s");
            // bool right = Input.GetKey("d");
            // x = 0;
            // z = 0;

            // if (forward)
            // {
            //     z += 1.0f;
            // }
            // if (left)
            // {
            //     x -= 1.0f;
            // }
            // if (right)
            // {
            //     x += 1.0f;
            // }
            // if (back)
            // {
            //     z -= 1.0f;
            // }
            // if (Input.GetKey(KeyCode.Mouse0))
            // {
            //     FirePrimaryAttack();
            // }
            // if (Input.GetKey(KeyCode.Mouse1))
            // {
            //     FireSecondaryAttack();
            // }
            // if (Input.GetKey(KeyCode.Mouse2))
            // {
            //     FinishSecondaryAttack();
            // }

            // if (Input.GetKey(KeyCode.P))
            // {
            //     PlayDeath();
            // }

            // SetLocomotionVector(x, z);
        }
    }
}