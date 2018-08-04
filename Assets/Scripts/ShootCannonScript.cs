using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

public class ShootCannonScript : MonoBehaviour
{
	public Transform FireTransform;
	public GameObject Ball;
	public Transform Target;
	public float RotationSpeed = 5f;
	public float RotationAccuracy = 0f;
	public float ShootingRange;
	public Animator Animator;
	public ParticleSystem ParticleSystem;

	private void Start()
	{
		Animator = GetComponent<Animator>();
	}

	[Task]
	private void Fire()
	{
		Instantiate(Ball, FireTransform.position, FireTransform.rotation).GetComponent<Rigidbody>().AddForce(FireTransform.forward * 2500);
		ParticleSystem.Play(true);
		Task.current.Succeed();
	}
	
	[Task]
	public void LookAtTarget()
	{
		var direction = Target.position - transform.position;

		transform.rotation = Quaternion.Slerp(transform.rotation,
			Quaternion.LookRotation(direction),
			Time.deltaTime * RotationSpeed);
        
		var angle = Vector3.Angle(transform.forward, direction);

		if(angle < RotationAccuracy)
		{
			Task.current.Succeed();
		}
		else
		{
			Task.current.Fail();
		}
	}
	
	[Task]
	public bool IsPlayerNear()
	{
		return (Target.position - transform.position).magnitude < ShootingRange;
	}

	private void Update()
	{
		if (Animator != null)
		{
			Animator.SetFloat("Distance", (Target.position - transform.position).magnitude);
		}
	}
	
	public void OpenFire()
	{
		this.InvokeRepeating(nameof(FsmFire), 0.5f, 0.5f);
	}

	public void HoldFire()
	{
		this.CancelInvoke(nameof(FsmFire));
	}

	private void FsmFire()
	{
		Instantiate(Ball, FireTransform.position, FireTransform.rotation).GetComponent<Rigidbody>().AddForce(FireTransform.forward * 1500);
		ParticleSystem.Play(true);
	}
}
