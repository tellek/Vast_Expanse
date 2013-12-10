using UnityEngine;
using System.Collections;

public class DebugUtils : MonoBehaviour {

	public GameObject citizenAdd;

	void Start () {
	
	}
	
	void Update () {
		if(Input.GetKeyUp("1")) {
			Instantiate(citizenAdd, transform.position, transform.rotation);
		}
	}
}
