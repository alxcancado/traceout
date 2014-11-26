using UnityEngine;
using System.Collections;

public class c : MonoBehaviour {
	
	public int tailLength = 1;
	public int tailNodes = 300;
	public GameObject[] nodes;
	public GameObject tail;
	public Color c1 = Color.yellow;
	public Color c2 = Color.red;
	
	
	
	private Vector3 pos;    //Position
	
	// Use this for initialization
	void Start () {
		
		nodes = new GameObject[tailNodes];
		
		for ( int i = 0; i < tailNodes; i++){
			nodes[i] = Instantiate(tail, transform.position, transform.rotation) as GameObject;
			//nodes[i].transform.position.x = this.gameObject.transform.position.x;
			//nodes[i].transform.position.y = this.gameObject.transform.position.y;
		}
		
		LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
		//Find mouse position
		pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
		//Set position
		transform.position = new Vector3(pos.x,pos.y,0);
		
		nodes[0] = this.gameObject;
		
		for ( int i = 1; i < tailNodes-1; i++){
			//posicao atual do obj do node
			Vector3 currentPos = nodes[i].gameObject.transform.position;
			//nova posicao
			Vector3 newPos = nodes[i-1].gameObject.transform.position;
			
			float nodeAngle = Mathf.Atan2(Camera.main.WorldToScreenPoint(currentPos).y - Camera.main.WorldToScreenPoint(newPos).y
			                              , Camera.main.WorldToScreenPoint(currentPos).x - Camera.main.WorldToScreenPoint(newPos).x
			                              ) * Mathf.Rad2Deg;
			Debug.Log (nodeAngle);
			
			// save in the next node
			nodes[i+1].gameObject.transform.position = new Vector3(currentPos.x, currentPos.y, currentPos.z);
			// update his own 
			nodes[i].gameObject.transform.position = new Vector3(newPos.x, newPos.y, newPos.z);
			
		}
		
		
	}
	
	
	// Loop through every tailNode and save new positions in table
	// Note: LUA tables start with an index of 1 and not 0!
	private void newPositions(){
		nodes[0] = this.gameObject;
		for (int i = 1; i < tailNodes; i++) {
			//Debug.Log(i);
			
			Vector3 nodOri = Camera.main.WorldToScreenPoint(nodes[0].transform.position);
			Vector3 nod = Camera.main.WorldToScreenPoint(nodes[i].transform.position);
			float nodeAngle = Mathf.Atan2( (nod.y - nodOri.y), (nod.x - nodOri.x) ); //* Mathf.Rad2Deg ;
			
			//Debug.Log(nodeAngle);
			//float nodeAngle = Mathf.Atan2(cam.WorldToScreenPoint(nodes[i].transform.position.y) - cam.WorldToScreenPoint(nodes[i-1].transform.position.y), cam.WorldToScreenPoint(nodes[i].transform.position.x) - cam.WorldToScreenPoint(nodes[i-1].transform.position.x)) * Mathf.Rad2Deg;    
			
			
			nodes[i].transform.position = new Vector3(nodes[i-1].transform.position.x + tailLength * Mathf.Cos(nodeAngle * Mathf.PI),
			                                          nodes[i-1].transform.position.y + tailLength * Mathf.Sin(nodeAngle* Mathf.PI),
			                                          this.gameObject.transform.position.z)
				;
			
			
			
		}     
	}
}