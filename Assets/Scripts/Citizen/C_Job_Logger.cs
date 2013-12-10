using UnityEngine;
using System.Collections;

public class C_Job_Logger : MonoBehaviour {

	private C_Behavior stats;
	private TreeController treeCon;
	private GameObject targetTree;

	private bool checkedForLogs = false;
	private C_FindClosest findClosest;
	private C_MoveTo moveTo;
	private GameObject jobObject;
	private Vector3 jobLocation;

	void Awake () {
		stats = GetComponent<C_Behavior>();
		findClosest = GetComponent<C_FindClosest>();
		moveTo = GetComponent<C_MoveTo>();
	}

	public void Go(bool isBusy) {

		GameObject[] logLocations = null;
		if(checkedForLogs == false) { //Look for logs and chop wood only if none are found.
			logLocations = GameObject.FindGameObjectsWithTag("Wood");
		}

		if (logLocations.Length != 0) {
			Debug.Log ("Found some");

		}
		else {
			if(stats.jobInProgress != "Tree") {
				jobObject = findClosest.Go("Tree");
				foreach(Transform child in jobObject.transform)
				{ jobLocation = child.transform.position; }
			}
			
			stats.jobInProgress = "Tree";
			if(transform.position != jobLocation) {
				moveTo.Go(jobLocation, jobObject.transform.position, stats.speed, stats.turnSpeed);
				stats.moving = true;
			}
			else { 
				stats.moving = false;
				if (jobObject != null) {
					if (isBusy == false) {
						treeCon = jobObject.GetComponent<TreeController>();
						StartCoroutine(Timber());
					}
				}
				else { stats.jobInProgress = ""; }
			}
		}

	}

	public void Collect() {
		
	}

	IEnumerator Timber()
	{
		stats.isBusy = true;
		yield return new WaitForSeconds (2f);
		Debug.Log ("Chop Chop");
		if(jobObject != null) { treeCon.health--; }
		stats.isBusy = false;
	}

}
