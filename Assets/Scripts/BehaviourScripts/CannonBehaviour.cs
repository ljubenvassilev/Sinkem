using UnityEngine;

namespace BehaviourScripts
{
	public class CannonBehaviour : StateMachineBehaviour
	{
		public Transform Target;
		public float RotationSpeed = 5f;
		protected ShootCannonScript Scs;

		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
			Scs = animator.gameObject.GetComponent<ShootCannonScript>();
		}
	}
}
