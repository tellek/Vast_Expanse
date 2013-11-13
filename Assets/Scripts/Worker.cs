using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Worker : MonoBehaviour {
	
	public bool busy, woodCutter, miner, builder;
	public float woodCutterExp = 100, minerExp = 50, builderExp, health, hunger, energy;
	public List<Vector3> actionList;
	public float speed = 5f;
	public float turnSpeed = 10f;
	
	private Vector3 targetPosition, chopPoint;
	private GameObject targetObject;
	private Transform targetTransform;
	private bool working;
	private float workTime;

	void Start() {
		targetPosition = transform.position;
		woodCutter = true;
	}
	
	//
	private TreeController theTree;
	//
	
	void Update () {
		if(transform.position != targetPosition)
		{
			MoveTo(targetObject, targetPosition);
		}
		
		if(transform.position == chopPoint && working == true)
		{
			Debug.Log("Worker: Starting task.");
			StartCoroutine(BeginWork(workTime));
			working = false;
		}
		
		if (woodCutter == true && busy == false)
		{
			busy = true;
			targetObject = FindTree();
			//
			theTree = targetObject.GetComponent<TreeController>();
			theTree.claimed = true;
			//
			//targetTransform = targetObject.transform;
			targetPosition = targetObject.transform.position;
			workTime = 3f; //Adjusted later based off multiple values.
			working = true;
		}
	}
	
	//MOVEMENT AND ROTATION
	void MoveTo(GameObject treeObject, Vector3 target)
	{
		Quaternion _lookRotation;
		Vector3 _direction;
		
		foreach(Transform child in treeObject.transform)
		{
			chopPoint = child.transform.position;
		}
		
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, chopPoint, step);
		
		_direction = (target - transform.position).normalized;
		_lookRotation = Quaternion.LookRotation(_direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * turnSpeed);
	}
	
	GameObject FindTree()
	{
		GameObject[] treeLocations = GameObject.FindGameObjectsWithTag("Tree");
		float distance = float.MaxValue;
		GameObject closestTree = GameObject.FindGameObjectWithTag("Tree");
		
		foreach (GameObject aTree in treeLocations)
		{
			Vector3 difference = aTree.transform.position - transform.position;
			float thisDistance = difference.sqrMagnitude;
			theTree = aTree.GetComponent<TreeController>();

			if(distance > thisDistance && theTree.claimed == false)
			{
				closestTree = aTree;
				distance = thisDistance;
			}
		}
		Debug.Log("Worker: Closest Tree: " + closestTree.name + closestTree.transform.position);
		return closestTree;
	}
	
	IEnumerator BeginWork(float seconds)
	{
		Debug.Log("Worker: Choopping wood.");
		yield return new WaitForSeconds (seconds);
		theTree.destroyed = true;
		busy = false;
		
	}
	
	
}
