using UnityEngine;
using System.Collections;

public class SelectionSystem : MonoBehaviour {

	public string Current_Selection = "Nothing";

	void FixedUpdate () {
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			Physics.Raycast(ray, out hit);
			Debug.Log("Current Selection: " + hit.transform.name); ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(hit.transform.name != "Plane")
			{
				Current_Selection = hit.transform.name;
			}
		}

		
	}
	
	public GUISkin customSkin;
	
	void OnGUI(){
		GUI.skin = customSkin;
		GUI.skin.label.alignment = TextAnchor.MiddleLeft;
		GUI.Box (new Rect(10,10,428,23),"Right-click to move, Hold the Right Mouse Button to continuously move.");
		GUI.Box (new Rect(10,35,250,23),"Current Selection: " + Current_Selection);
		GUI.Box (new Rect(10,70,175,23),"Press '1' to spawn a citizen. ");
	}
}
