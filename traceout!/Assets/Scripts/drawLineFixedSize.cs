using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class drawLineFixedSize : MonoBehaviour {

	private LineRenderer line;
	private bool isMousePressed;
	private List<Vector3> pointsList;
	private Vector3 mousePos;

	public float lengthOfLineRenderer = 50;
	public bool startLine = false;

	
	// Structure for line points
	struct myLine
	{
		public Vector3 StartPoint;
		public Vector3 EndPoint;
	};
	//    -----------------------------------    
	void Awake()
	{
		// Create line renderer component and set its property
		line = gameObject.AddComponent<LineRenderer>();
		line.material = new Material(Shader.Find("Particles/Additive"));
		line.SetVertexCount(0);
		line.SetWidth(0.05f,0.05f);
		line.SetColors(Color.white, Color.white);
		line.useWorldSpace = true;    
		isMousePressed = false;
		pointsList = new List<Vector3>();
	}
	//    -----------------------------------    
	void Update()
	{
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePos.z=0;



		if (startLine) {
						if (!pointsList.Contains (mousePos)) {
								pointsList.Add (mousePos);
				
								if (pointsList.Count >= lengthOfLineRenderer) {
										pointsList.RemoveAt (0);
								}
								//Debug.Log(pointsList[0]);
								//line.SetVertexCount (pointsList.Count);
				
				
								int i = 0;
								while (i < pointsList.Count) {
										line.SetVertexCount (pointsList.Count);
										line.SetPosition (i, (Vector3)pointsList [i]);
					
					
										//if (Physics.Raycast(pointsList [i], pointsList [i+1], 10)){
										//	print("There is something in front of the object!");
										//}
					
					
					
					
										if (isLineCollide () || isLineCollidedWithOtherObject ()) {
												//isMousePressed = false;
												line.SetColors (Color.clear, Color.red);
										} else {
												line.SetColors (Color.clear, Color.white);
										}
					
										i++;
								}
						}
				} else {
			isLineCollidedWithStartObject();
				}
	}

	//    -----------------------------------    
	//  Following method checks is currentLine(line drawn by last two points) collided with line 
	//    -----------------------------------    
	private bool isLineCollide()
	{
		if (pointsList.Count < 2)
			return false;
		int TotalLines = pointsList.Count - 1;
		myLine[] lines = new myLine[TotalLines];
		if (TotalLines > 1) 
		{
			for (int i=0; i<TotalLines; i++) 
			{
				lines [i].StartPoint = (Vector3)pointsList [i];
				lines [i].EndPoint = (Vector3)pointsList [i + 1];
			}
		}
		for (int i=0; i<TotalLines-1; i++) 
		{
			myLine currentLine;
			currentLine.StartPoint = (Vector3)pointsList [pointsList.Count - 2];
			currentLine.EndPoint = (Vector3)pointsList [pointsList.Count - 1];
			if (isLinesIntersect (lines [i], currentLine)) 
				return true;
		}
		return false;
	}
	//    -----------------------------------    
	//    Following method checks whether given two points are same or not
	//    -----------------------------------    
	private bool checkPoints (Vector3 pointA, Vector3 pointB)
	{
		return (pointA.x == pointB.x && pointA.y == pointB.y);
	}
	//    -----------------------------------    
	//    Following method checks whether given two line intersect or not
	//    -----------------------------------    
	private bool isLinesIntersect (myLine L1, myLine L2)
	{
		if (checkPoints (L1.StartPoint, L2.StartPoint) ||
		    checkPoints (L1.StartPoint, L2.EndPoint) ||
		    checkPoints (L1.EndPoint, L2.StartPoint) ||
		    checkPoints (L1.EndPoint, L2.EndPoint))
			return false;
		
		return((Mathf.Max (L1.StartPoint.x, L1.EndPoint.x) >= Mathf.Min (L2.StartPoint.x, L2.EndPoint.x)) &&
		       (Mathf.Max (L2.StartPoint.x, L2.EndPoint.x) >= Mathf.Min (L1.StartPoint.x, L1.EndPoint.x)) &&
		       (Mathf.Max (L1.StartPoint.y, L1.EndPoint.y) >= Mathf.Min (L2.StartPoint.y, L2.EndPoint.y)) &&
		       (Mathf.Max (L2.StartPoint.y, L2.EndPoint.y) >= Mathf.Min (L1.StartPoint.y, L1.EndPoint.y)) 
		       );
	}
	//
	//
	//

	private bool isLineCollidedWithOtherObject()
	{
		// it's just testing the first position
		RaycastHit2D hit = Physics2D.Raycast((pointsList[pointsList.Count-1]), Vector2.zero);
		if (hit.collider != null) {

			if (hit.collider.name == "heart" )
			{
				Debug.Log("Finish Point");
				
			}
			return true;
				
		} else {
			return false;	
		}

	}

	private bool isLineCollidedWithStartObject()
	{
		// mouse position collides with start button
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);


		if (hit.collider != null) {
			if (hit.collider.name == "startHeart" )
			{
				startLine =true;
				Debug.Log("Start Point");

			}

			return true;
		} else {
			return false;	
		}

	}
}
