using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	private Rigidbody rigidBody;
	public float speed;
	public float rotationSpeed;
	public GameObject ball;
	public Transform[] shootLeft;
	public Transform[] shootRight;
	public float power;
	public Slider healthBar;
	public float maxHealth;
	public float currentHealth;
	private bool ableToShoot;
	public GameManager gameManager;
	private bool dead;
        
	void Start()
	{
		rigidBody = GetComponent<Rigidbody>();
		currentHealth = maxHealth;
		healthBar.value = maxHealth;
		StartCoroutine(nameof(Regenerate));
		ableToShoot = true;
		dead = false;
	}

	private void FixedUpdate()
	{
		rigidBody.MovePosition(rigidBody.position + transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed);
		rigidBody.MoveRotation(rigidBody.rotation * Quaternion.Euler(0f, Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed, 0f));
		if (ableToShoot)
		{
			if (Input.GetKeyDown(KeyCode.J))
			{
				StartCoroutine(Fire(shootLeft));
			}
			else if (Input.GetKeyDown(KeyCode.K))
			{
				StartCoroutine(Fire(shootRight));
			}
		}
	}

	private IEnumerator Fire(IEnumerable<Transform> firePositions)
	{
		ableToShoot = false;
		foreach (var t in firePositions)
		{
			Instantiate(ball, t.position, t.localRotation).GetComponent<Rigidbody>().AddForce(t.forward * power);
		}
		yield return new WaitForSeconds(2);
		ableToShoot = true;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("ball")) return;
		currentHealth -= 5;
		healthBar.value = currentHealth / maxHealth;
	}

	private void Update()
	{
		if (currentHealth <= 0 && !dead)
		{
			dead = true;
			Die();
		}
		// ReSharper disable once CompareOfFloatsByEqualityOperator
		else if (Time.time / 60 == 0)
		{
			currentHealth++;
		}
	}

	private void Die()
	{
		gameManager.ShowPanel(2);
	}

	private IEnumerator Regenerate(){
		while (true){
			if (currentHealth < maxHealth){
				currentHealth++;
				healthBar.value = currentHealth / maxHealth;
				yield return new WaitForSeconds(1);
			} else yield return null;
		}
		// ReSharper disable once IteratorNeverReturns
	}
}
