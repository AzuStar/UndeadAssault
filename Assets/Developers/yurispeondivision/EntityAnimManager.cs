using UnityEngine;

namespace UndeadAssault
{
    public class EntityAnimManager : MonoBehaviour
    {
        Animator animator;
        float x = 0.0f;
        float z = 0.0f;

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
            animator.SetFloat(EntityAnimParameters.X, x);
            animator.SetFloat(EntityAnimParameters.Y, y);
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
            bool forward = Input.GetKey("w");
            bool left = Input.GetKey("a");
            bool back = Input.GetKey("s");
            bool right = Input.GetKey("d");
            x = 0;
            z = 0;

            if (forward)
            {
                z += 1.0f;
            }
            if (left)
            {
                x -= 1.0f;
            }
            if (right)
            {
                x += 1.0f;
            }
            if (back)
            {
                z -= 1.0f;
            }
            if (Input.GetKey(KeyCode.Mouse0))
            {
                FirePrimaryAttack();
            }
            if (Input.GetKey(KeyCode.Mouse1))
            {
                FireSecondaryAttack();
            }
            if (Input.GetKey(KeyCode.Mouse2))
            {
                FinishSecondaryAttack();
            }

            if (Input.GetKey(KeyCode.P))
            {
                PlayDeath();
            }

            SetLocomotionVector(x, z);
        }
    }
}