using UnityEngine;
using System.Collections;

public class Citizen_GetMedical : MonoBehaviour {
	
	public Citizen_MoveTo moveTo;
	
	public void Go() {
		Debug.Log("Citizen: I'm Injured!");
		
		GameObject[] medLocations = GameObject.FindGameObjectsWithTag("Medical");
		float distance = float.MaxValue;
		GameObject closestMed = GameObject.FindGameObjectWithTag("Medical");
		
		foreach (GameObject aMed in medLocations)
		{
			Vector3 difference = aMed.transform.position - transform.position;
			float thisDistance = difference.sqrMagnitude;

			if(distance > thisDistance)
			{
				closestMed = aMed;
				distance = thisDistance;
			}
		}
		Debug.Log("ME " + transform.position);
		
		moveTo = GetComponent<Citizen_MoveTo>();
		moveTo.Go(closestMed,8,10);
	}
	
}
