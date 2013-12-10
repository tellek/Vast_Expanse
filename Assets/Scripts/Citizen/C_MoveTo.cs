using UnityEngine;
using System.Collections;

public class C_MoveTo : MonoBehaviour {

	public void Go(Vector3 movePoint, Vector3 target, float speed, float turnSpeed)
	{
		Debug.Log("Citizen: Moving to location " + movePoint);

		Quaternion _lookRotation;
		Vector3 _direction;

		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, movePoint, step);
		
		_direction = (target - transform.position).normalized;
		_lookRotation = Quaternion.LookRotation(_direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * turnSpeed);
	}


}
