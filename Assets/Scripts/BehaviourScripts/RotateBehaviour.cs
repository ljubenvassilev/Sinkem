using UnityEngine;

namespace BehaviourScripts
{
    public class RotateBehaviour : CannonBehaviour 
    {
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            animator.gameObject.transform.Rotate(0, Time.deltaTime * 15, 0);
        }
    }
}
