using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// ReSharper disable once InconsistentNaming
public class AIShipScript : MonoBehaviour
{
	public float ChaseDistance, ShootDistance, RotationSpeed;
	private GameObject _player;
	private NavMeshAgent _navMeshAgent;
	private int _rotatingDirection;
	private bool _shooting;
	public float MaxHealth;
	public float CurrentHealth;
	public Transform[] LeftFireTransforms;
	public Transform[] RightFireTransforms;
	public GameObject Ball;
	public float Power;
	public GameManager GameManager;
	private bool _dead;
	public Slider HealthBar;
	private ParticleSystem[] _leftEffects;
	private ParticleSystem[] _rightEffects;
	public GameObject LeftShootEffects;
	public GameObject RightShootEffects;

	private void Start ()
	{
		_navMeshAgent = GetComponent<NavMeshAgent>();
		_player = GameObject.FindGameObjectWithTag("Player");
		_rotatingDirection = 0;
		CurrentHealth = MaxHealth;
		HealthBar.value = MaxHealth;
		_dead = false;
		_leftEffects = LeftShootEffects.GetComponentsInChildren<ParticleSystem>();
		_rightEffects = RightShootEffects.GetComponentsInChildren<ParticleSystem>();
	}

	private void Update()
	{
		var distance = Vector3.Distance(_player.transform.position, transform.position);
		if (distance > ChaseDistance) return;
		if (distance < ShootDistance)
		{
			if (_rotatingDirection != 0) return;
			_rotatingDirection = Random.Range(1, 3);
		}
		else
		{
			_navMeshAgent.SetDestination(_player.transform.position);
			_rotatingDirection = 0;
			_shooting = false;
			CancelInvoke(nameof(Shoot));
		}
	}

	private void FixedUpdate()
	{
		if (_rotatingDirection == 0) return;
		var desiredDirection = _player.transform.position - transform.position;
		var rotation = Quaternion.LookRotation(desiredDirection);
		rotation *= Quaternion.Euler(0, _rotatingDirection == 1 ? 90 : -90, 0);
		if (rotation.y < 20)
		{
			if (!_shooting)
			{
				_shooting = true;
				InvokeRepeating(nameof(Shoot), 2f, 2f);
			}
		}
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * RotationSpeed);
	}

	private void Shoot()
	{
		IEnumerable<Transform> firePositions = _rotatingDirection == 1 ? LeftFireTransforms : RightFireTransforms;
		IEnumerable<ParticleSystem> effects = _rotatingDirection == 1 ? _leftEffects : _rightEffects;
		foreach (var t in firePositions)
		{
			Instantiate(Ball, t.position, t.localRotation).GetComponent<Rigidbody>().AddForce(t.forward * Power);
		}

		foreach (var effect in effects)
		{
			effect.Play();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("ball"))
		{
			CurrentHealth -= 10;
			HealthBar.value = CurrentHealth / MaxHealth;
		}
		if (CurrentHealth > 0 || _dead) return;
		GameManager.EnemyDead();
		Destroy(gameObject);
		_dead = true;
	}
}