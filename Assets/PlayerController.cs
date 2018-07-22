using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody rigidBody;
	public float speed;
	public float rotationSpeed;
        
	void Start()
	{
		rigidBody = this.GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		rigidBody.MovePosition(rigidBody.position + transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed);
		rigidBody.MoveRotation(rigidBody.rotation * Quaternion.Euler(0f, Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed, 0f));
	}
}
