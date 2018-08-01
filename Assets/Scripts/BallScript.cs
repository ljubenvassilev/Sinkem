using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

	void Start ()
	{
		StartCoroutine(nameof(Dissapear));
	}

	private IEnumerator Dissapear()
	{
		yield return new WaitForSeconds(5.0f);
		Destroy(this.gameObject);
	}

	private void OnCollisionEnter(Collision other)
	{
		StopCoroutine(nameof(Dissapear));
		Destroy(this.gameObject);
	}
}
