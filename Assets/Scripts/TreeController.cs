using UnityEngine;
using System.Collections;

public class TreeController : MonoBehaviour {
	
	public bool claimed;
	public Transform logs;
	public int health = 3;

	void Start () {
		transform.rotation = Quaternion.AngleAxis(Random.Range(0,360), Vector3.up);
	}
	
	void Update () {
		
		if(health <= 0)
		{
			Instantiate(logs, transform.position, transform.rotation);
			Transform.Destroy(gameObject);
		}
	}
	

}
