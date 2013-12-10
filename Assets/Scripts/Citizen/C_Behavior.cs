using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C_Behavior : MonoBehaviour {

	#region The Job List
	public enum jobList {
		None,
		Logger,
		Miner,
		Builder,
		Warrior
	};
	public jobList currentJob;
	#endregion

	#region The Public Variables
	public double 
		health = 100, maxHealth = 100,
		hunger, maxHunger = 100,
		energy = 100, maxEnergy = 100;
	public float 
		speed = 4f, turnSpeed = 3f,
		addHungerEvery = 10, minusEnergyEvery = 30;
	public bool hungerOverTime = false, EnergyOverTime = false,
	isBusy = false, moving = false;
	public Animator anim;
	public string jobInProgress;
	#endregion

	#region The Private Variables
	private bool busy = false;
	private GameObject jobObject;
	private Vector3 jobLocation;
	private C_FindClosest findClosest;
	private C_MoveTo moveTo;
	private C_RestoreStats restoreStats;
	private C_Job_Logger jobLogger;
	#endregion

	static bool alarmEnabled = false;
	
	void Awake () {
		findClosest = GetComponent<C_FindClosest>();
		moveTo = GetComponent<C_MoveTo>();
		restoreStats = GetComponent<C_RestoreStats>();
		jobLogger = GetComponent<C_Job_Logger>();
	}

	void Start () {
		anim = GetComponent<Animator>();
		jobObject = null;
	}
	
	void Update () {
		RestrictRanges(); // Prevent Health, Hunger and Energy from getting exceding a max or min.
		double healthPercent = (health / maxHealth) * 100;
		double hungerPercent = (hunger / maxHunger) * 100;
		double energyPercent = (energy / maxEnergy) * 100;

		StartCoroutine(hungerOT());
		StartCoroutine(energyOT());
		
		if (busy == true) { moving = false; } //Stay put.
		//Priority 1: ALERT triggered. All worker citizens will run home until the alarm is disabled(maybe have to build an alarm?)
		else if (alarmEnabled == true) {

		}
		//Priority 2: If the Citizen is injured it will do this.
		else if (healthPercent < 100) {
			ManageStats("Medical"); //ManageStats function is below.
		}
		//Priority 3: If the Citizen is hungry it will do this.
		else if (hungerPercent > 75) {
			ManageStats("Food"); //ManageStats function is below.
		}
		//Priority 4: If the Citizen is tired it will do this.
		else if (energyPercent < 85) {
			ManageStats("Home"); //ManageStats function is below.
		}
		//Priority 5: Do work depending on what current job is.
		else {
			switch (currentJob) {
			case jobList.None:
				Debug.Log("Citizen: Doing nothing.");
				break;
			case jobList.Logger:
				jobLogger.Go(isBusy);
				break;
			case jobList.Miner:
				break;
			case jobList.Builder:
				break;
			case jobList.Warrior:
				break;
			}
		}
		
		if (moving == true)
		{ anim.SetFloat("Speed",1); }
		else
		{ anim.SetFloat("Speed",0); }

		
	}

	//May at some point split ManageStats out to its own class.
	private void ManageStats(string stat) {
		if(jobInProgress != stat) {
			jobObject = findClosest.Go(stat); jobLocation = jobObject.transform.position;
			Debug.Log("Citizen: Found Job(" + jobObject.name + ") at location " + jobLocation);
			jobInProgress = stat;
		}
		//PERFORMANCE - Must prevent list generation for same type of job while on that job.
		if(transform.position != jobLocation) {
			moveTo.Go(jobLocation, jobLocation, speed, turnSpeed);
			moving = true;
		}
		else { 
			moving = false;
			restoreStats.Go(isBusy, stat);
		}
	}

	#region Stat Changes
	IEnumerator hungerOT() { //This increases hunger over time.
		while (hungerOverTime) {
			yield return new WaitForSeconds (addHungerEvery);
			hunger++;
		}
	}

	IEnumerator energyOT() { //This drains energy over time.
		while (EnergyOverTime) {
			yield return new WaitForSeconds (minusEnergyEvery);
			energy--;
		}
	}

	void RestrictRanges() {
		if(health > maxHealth) { health = maxHealth; }
		if(health < 0) { health = 0; }
		if(hunger > maxHunger) { hunger = maxHunger; }
		if(hunger < 0) { hunger = 0; }
		if(energy > maxEnergy) { energy = maxEnergy; }
		if(energy < 0) { energy = 0; }
	}
	#endregion
}
