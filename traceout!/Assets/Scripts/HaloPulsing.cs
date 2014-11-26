using UnityEngine;
using System.Collections;

public class HaloPulsing : MonoBehaviour {
	public float maxDist = 5f;
	public float speed = 1.0f;
	private float timer   = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		light.range = Mathf.PingPong(timer * speed, maxDist*3);
		timer += (Time.deltaTime*0.25f);
	}
}
