using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	private Rigidbody _rigidBody;
	public float Speed;
	public float RotationSpeed;
	public GameObject Ball;
	public Transform[] ShootLeft;
	public Transform[] ShootRight;
	public GameObject LeftShootEffects;
	public GameObject RightShootEffects;
	public float Power;
	public Slider HealthBar;
	public float MaxHealth;
	public float CurrentHealth;
	private bool _ableToShoot;
	public GameManager GameManager;
	private bool _dead;
	private ParticleSystem[] _leftEffects;
	private ParticleSystem[] _rightEffects;
        
	void Start()
	{
		_rigidBody = GetComponent<Rigidbody>();
		CurrentHealth = MaxHealth;
		HealthBar.value = MaxHealth;
		StartCoroutine(nameof(Regenerate));
		_ableToShoot = true;
		_dead = false;
		_leftEffects = LeftShootEffects.GetComponentsInChildren<ParticleSystem>();
		_rightEffects = RightShootEffects.GetComponentsInChildren<ParticleSystem>();
	}

	private void FixedUpdate()
	{
		_rigidBody.MovePosition(_rigidBody.position + transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * Speed);
		_rigidBody.MoveRotation(_rigidBody.rotation * Quaternion.Euler(0f, Input.GetAxis("Horizontal") * Time.deltaTime * RotationSpeed, 0f));
		if (_ableToShoot)
		{
			if (Input.GetKeyDown(KeyCode.J))
			{
				StartCoroutine(Fire(ShootLeft, _leftEffects));
			}
			else if (Input.GetKeyDown(KeyCode.K))
			{
				StartCoroutine(Fire(ShootRight, _rightEffects));
			}
		}
	}

	private IEnumerator Fire(IEnumerable<Transform> firePositions, IEnumerable<ParticleSystem> effectParticleSystems)
	{
		_ableToShoot = false;
		foreach (var t in firePositions)
		{
			Instantiate(Ball, t.position, t.localRotation).GetComponent<Rigidbody>().AddForce(t.forward * Power);
		}

		foreach (var system in effectParticleSystems)
		{
			system.Play();
		}
		yield return new WaitForSeconds(2);
		_ableToShoot = true;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("ball")) return;
		CurrentHealth -= 5;
		HealthBar.value = CurrentHealth / MaxHealth;
	}

	private void Update()
	{
		if (CurrentHealth <= 0 && !_dead)
		{
			_dead = true;
			Die();
		}
		// ReSharper disable once CompareOfFloatsByEqualityOperator
		else if (Time.time / 60 == 0)
		{
			CurrentHealth++;
			HealthBar.value = CurrentHealth / MaxHealth;
		}
	}

	private void Die()
	{
		GameManager.ShowPanel(2);
	}

	private IEnumerator Regenerate(){
		while (true){
			if (CurrentHealth < MaxHealth){
				CurrentHealth++;
				HealthBar.value = CurrentHealth / MaxHealth;
				yield return new WaitForSeconds(1);
			} else yield return null;
		}
		// ReSharper disable once IteratorNeverReturns
	}
}
