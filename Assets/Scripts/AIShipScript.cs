﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIShipScript : MonoBehaviour
{
	public float chaseDistance, shootDistance, rotationSpeed;
	private GameObject player;
	private NavMeshAgent navMeshAgent;
	private int rotatingDirection;
	private bool shooting;
	public float maxHealth;
	public float currentHealth;
	public Transform[] leftFireTransforms;
	public Transform[] rightFireTransforms;
	public GameObject ball;
	public float power;
	public GameManager gameManager;
	private bool dead;

	void Start ()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag("Player");
		rotatingDirection = 0;
		currentHealth = maxHealth;
		dead = false;
	}

	private void Update()
	{
		var distance = Vector3.Distance(player.transform.position, transform.position);
		if (distance > chaseDistance) return;
		if (distance < shootDistance)
		{
			if (rotatingDirection != 0) return;
			rotatingDirection = Random.Range(1, 3);
		}
		else
		{
			navMeshAgent.SetDestination(player.transform.position);
			rotatingDirection = 0;
			shooting = false;
			CancelInvoke(nameof(Shoot));
		}
	}

	private void FixedUpdate()
	{
		if (rotatingDirection == 0) return;
		var desiredDirection = player.transform.position - transform.position;
		var rotation = Quaternion.LookRotation(desiredDirection);
		rotation *= Quaternion.Euler(0, rotatingDirection == 1 ? 90 : -90, 0);
		if (rotation.y < 20)
		{
			if (!shooting)
			{
				shooting = true;
				InvokeRepeating(nameof(Shoot), 2f, 2f);
			}
		}
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
	}

	private void Shoot()
	{
		IEnumerable<Transform> firePositions = rotatingDirection == 1 ? leftFireTransforms : rightFireTransforms;
		foreach (var t in firePositions)
		{
			Instantiate(ball, t.position, t.localRotation).GetComponent<Rigidbody>().AddForce(t.forward * power);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("ball"))
		{
			currentHealth -= 10;
		}
		if (currentHealth > 0) return;
		if (!dead)
		{
			gameManager.EnemyDead();
			Destroy(this.gameObject);
			dead = true;
		}
	}
}