using UnityEngine;
using System.Collections;

public class TreeController : MonoBehaviour {
	
	public bool claimed, destroyed;
	public Transform logs;
	
	private bool gone;
	
	void Start () {
		transform.rotation = Quaternion.AngleAxis(Random.Range(0,360), Vector3.up);
	}
	
	void Update () {
		
		if(destroyed == true && gone == false)
		{
			Instantiate(logs, transform.position, transform.rotation);
			Transform.Destroy(gameObject);
			gone = true;
		}
	}
	

}
