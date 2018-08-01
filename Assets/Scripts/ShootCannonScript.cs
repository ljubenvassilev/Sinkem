using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCannonScript : MonoBehaviour
{
	public Transform fireTransform;
	public GameObject ball;
	
	// Use this for initialization
	void Start () {
				
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void OpenFire()
	{
		InvokeRepeating(nameof(Fire), 0f, 1f);
	}

	public void HoldFire()
	{
		CancelInvoke(nameof(Fire));
	}

	private void Fire()
	{
		Instantiate(ball, fireTransform.position, fireTransform.rotation).GetComponent<Rigidbody>().AddForce(fireTransform.forward * 1500);
	}
}
