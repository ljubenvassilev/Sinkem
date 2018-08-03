using UnityEngine;

namespace Island.Scripts
{
	public class WaterScroller : MonoBehaviour 
	{	
		public float ScrollSpeed = 0.1f;
		public Camera Cam;
		private Material _mat;
		private new Renderer renderer;
		private float _offset;

		private void Start()
		{
			renderer = GetComponent<Renderer>();
			_mat = renderer.sharedMaterial;
			Cam.depthTextureMode |= DepthTextureMode.Depth;
		}
	
		void Update () {
			_offset = Time.time * ScrollSpeed;
			Vector2 k = new Vector2(_offset / 10.0f, _offset);
			_mat.SetTextureOffset ("_MainTex", k);
		}
	}
}
