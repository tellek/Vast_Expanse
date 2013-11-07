using UnityEngine;
using System.Collections;

public class SelecionSystem : MonoBehaviour {

	public string Current_Selection = "Nothing";
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			Physics.Raycast(ray, out hit);
			Debug.Log("Current Selection: " + hit.transform.name); ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Current_Selection = hit.transform.name;
		}

		
	}
	
	public GUISkin customSkin;
	
	void OnGUI(){
		GUI.skin = customSkin;
		GUI.skin.label.alignment = TextAnchor.MiddleLeft;
		GUI.Box (new Rect(10,10,428,23),"Right-click to move, Hold the Right Mouse Button to continuously move.");
		GUI.Box (new Rect(10,35,250,23),"Current Selection: " + Current_Selection);
	}
}
