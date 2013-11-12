using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Worker : MonoBehaviour {
	
	public bool busy, woodCutter, miner, builder;
	public float woodCutterExp, minerExp, builderExp, health, hunger, energy;
	public List<Vector3> actionList;
	public float speed = 5f;
	public float turnSpeed = 10f;
	
	private Vector3 targetPosition;
	private GameObject targetObject;
	private Transform targetTransform;
	private bool working;
	private float workTime;

	void Start() {
		targetPosition = transform.position;
		woodCutter = true;
	}
	
	//
	private TreeController GenTest;
	//
	
	void Update () {
		if(transform.position != targetPosition)
		{
			MoveTo(targetPosition);
		}
		
		if(transform.position == targetPosition && working == true)
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
			GenTest = targetObject.GetComponent<TreeController>();
			GenTest.claimed = true;
			//
			targetTransform = targetObject.transform;
			targetPosition = targetObject.transform.position;
			workTime = 3f; //Adjusted later based off multiple values.
			working = true;
		}
	}
	
	//MOVEMENT AND ROTATION
	void MoveTo(Vector3 target)
	{
		Quaternion _lookRotation;
		Vector3 _direction;
		
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target, step);
		
		_direction = (targetTransform.position - transform.position).normalized;
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
			//
			GenTest = aTree.GetComponent<TreeController>();
			//
			if(distance > thisDistance && GenTest.claimed == false)
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
		GenTest.destroyed = true;
		busy = false;
		
	}
	
	
}
