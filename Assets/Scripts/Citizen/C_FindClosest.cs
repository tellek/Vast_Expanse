using UnityEngine;
using System.Collections;

public class C_FindClosest : MonoBehaviour {

	private GameObject closestLocation;

	public GameObject Go(string theTag) 
	{
		Debug.Log ("Citizen: Finding closest " + theTag);

		//Create a list of all objects with the overloaded Tag.
		GameObject[] locations = GameObject.FindGameObjectsWithTag(theTag);

		float distance = float.MaxValue;
		closestLocation = GameObject.FindGameObjectWithTag(theTag);

		//Find the closest object in the list and assign it to 'closestLocation'.
		foreach (GameObject aLoc in locations)
		{
			Vector3 difference = aLoc.transform.position - transform.position;
			float thisDistance = difference.sqrMagnitude;
			
			if(distance > thisDistance)
			{
				closestLocation = aLoc;
				distance = thisDistance;
			}
		}
		
		return closestLocation;
	}

}