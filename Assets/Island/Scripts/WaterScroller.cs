using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScroller : MonoBehaviour {
	
	public float scrollSpeed = 0.1f;
	public Camera cam;
	private Renderer renderer;

	private void Start()
	{
		renderer = GetComponent<Renderer>();
		if(renderer.material.shader.isSupported)
			cam.depthTextureMode |= DepthTextureMode.Depth;
	}

	void Update () 
	{
		float offset = Time.time * scrollSpeed;
		Vector2 k = new Vector2(offset / 10.0f, offset);
		renderer.sharedMaterial.SetTextureOffset ("_MainTex", k);
	}
}
