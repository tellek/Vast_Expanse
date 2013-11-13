using UnityEngine;
using System.Collections;

public class Citizen_MoveTo : MonoBehaviour {

	public void Go(GameObject toObject, float speed, float turnSpeed)
	{
		Quaternion _lookRotation;
		Vector3 _direction;
		Vector3 target = toObject.transform.position;
		
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target, step);
		
		_direction = (target - transform.position).normalized;
		_lookRotation = Quaternion.LookRotation(_direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * turnSpeed);
	}
}
