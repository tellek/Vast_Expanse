using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Citizen_Behavior : MonoBehaviour {
	
	public enum jobList {
		None,
		WoodCutter,
		Miner,
		Builder
	};
	public jobList currentJob;
	
	public double 
		health = 100, maxHealth = 100,
		hunger, maxHunger = 100,
		energy = 100, maxEnergy = 100;
	public float 
		secondsWillIdle = 3, speed = 10f, turnSpeed = 10f,
		addHungerEvery = 10, minusEnergyEvery = 30;
	public bool hungerOverTime = true, EnergyOverTime = true, restoring = false;
	
	static bool alarmEnabled = false;
	
	private Citizen_SelfRestore selfRestore;
	
	void Awake () {
		selfRestore = GetComponent<Citizen_SelfRestore>();
	}
	
	void Start () {
		StartCoroutine(hungerOT());
		StartCoroutine(energyOT());

	}

	void Update () {
		RestrictRanges(); // Prevent Health, Hunger and Energy from getting exceding a max or min.
		double healthPercent = (health / maxHealth) * 100;
		double hungerPercent = (hunger / maxHunger) * 100;
		double energyPercent = (energy / maxEnergy) * 100;
		
		if (restoring == true) {  } //Stay put.
		//Priority 1: ALERT triggered. All worker citizens will run home until the alarm is disabled(maybe have to build an alarm?)
		else if (alarmEnabled == true) {
			selfRestore.Go("Home",speed, turnSpeed);
		}
		//Priority 2: If the Citizen is injured it will do this.
		else if (healthPercent < 100) {
			if(selfRestore.Go("Medical",speed, turnSpeed)) { restoring = true; }
		}
		//Priority 3: If the Citizen is hungry it will do this.
		else if (hungerPercent > 75) {
			if(selfRestore.Go("Food",speed, turnSpeed)) { restoring = true; }
		}
		//Priority 4: If the Citizen is tired it will do this.
		else if (energyPercent < 85) {
			if(selfRestore.Go("Home",speed, turnSpeed)) { restoring = true; }
		}
		//Priority 5: Do work
		else {
			Idle(secondsWillIdle);
		}
	}
	
	IEnumerator Idle(float seconds) {
		Debug.Log("Citizen: I have nothing to do!");
		yield return new WaitForSeconds (seconds);
	}
	
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
	
}
