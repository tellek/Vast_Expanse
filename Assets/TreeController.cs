using UnityEngine;
using System.Collections;

public class TreeController : MonoBehaviour {
	
	public bool claimed, destroyed;
	public Transform logs;
	
	private bool gone;
	
	void Start () {
	
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
