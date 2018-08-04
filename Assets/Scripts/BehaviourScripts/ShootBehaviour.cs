using UnityEngine;

namespace BehaviourScripts
{
    public class ShootBehaviour : CannonBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, animatorStateInfo, layerIndex);
            Scs.OpenFire();
        }
        
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            var direction = Target.position - animator.gameObject.transform.position;
            var lookAtRotation = Quaternion.LookRotation(direction);
            var lookAtRotationOnlyY = Quaternion.Euler(animator.gameObject.transform.rotation.eulerAngles.x, 
                lookAtRotation.eulerAngles.y, animator.gameObject.transform.rotation.eulerAngles.z);
            animator.gameObject.transform.rotation = Quaternion.Slerp(animator.gameObject.transform.rotation,
                lookAtRotationOnlyY, Time.deltaTime * RotationSpeed);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            Scs.HoldFire();
        }
    }
}
