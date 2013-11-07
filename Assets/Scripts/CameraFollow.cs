using UnityEngine;

public class CameraFollow: MonoBehaviour {

	public Transform target;
	public float distance, height;
	
	void Update()
	{
		transform.position = new Vector3(target.position.x, target.position.y +height, target.position.z -distance);
	}
}
