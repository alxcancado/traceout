using UnityEngine;
using System.Collections;

public class LightiningAuto : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 lightPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		lightPos.z = -1;
		transform.position = lightPos;
		//- See more at: http://www.theappguruz.com/tutorial/unity-lighting-effect-on-2d-sprite-in-unity/#sthash.63FEhhDV.dpuf
	}
}
