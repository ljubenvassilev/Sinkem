using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody rigidBody;
	public float speed = 10f;
	public float rotationSpeed = 70f;
        
	void Start()
	{
		this.rigidBody = this.GetComponent<Rigidbody>();
	}

	void LateUpdate()
	{
		float translation = Input.GetAxis("Vertical") * Time.deltaTime * this.speed;
		float rotation = Input.GetAxis("Horizontal") * Time.deltaTime * this.rotationSpeed;

		Quaternion turnAngle = Quaternion.Euler(0f, rotation, 0f);

		this.rigidBody.MovePosition(this.rigidBody.position + this.transform.forward * translation);
		this.rigidBody.MoveRotation(this.rigidBody.rotation * turnAngle);
	}
}
