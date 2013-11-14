using UnityEngine;
using System.Collections;

public class Citizen_SelfRestore : MonoBehaviour {
	private string restoreType;
	private float moveSpeed, rotateSpeed;
	private GameObject closestLocation;
	
	public bool Go(string theTag, float speed, float turnSpeed) 
	{
		if(closestLocation != null && transform.position == closestLocation.transform.position)
		{
			closestLocation = null;
			return true;
		}
			
		restoreType = theTag;
		moveSpeed = speed;
		rotateSpeed = turnSpeed;
		GameObject[] locations = GameObject.FindGameObjectsWithTag(restoreType);
		float distance = float.MaxValue;
		closestLocation = GameObject.FindGameObjectWithTag(restoreType);
		
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
		
		moveTo();
		return false;
	}
	
	private void moveTo()
	{
		Quaternion _lookRotation;
		Vector3 _direction;
		Vector3 target = closestLocation.transform.position;
		
		float step = moveSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target, step);
		
		_direction = (target - transform.position).normalized;
		_lookRotation = Quaternion.LookRotation(_direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotateSpeed);
	}

}
