using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{
	public new Transform camera;
	
	private void Update()
	{
		transform.LookAt(camera);
	}
}
