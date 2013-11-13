using UnityEngine;
using System.Collections;

public class Citizen_Behavior : MonoBehaviour {
	public double 
		health = 100, maxHealth = 100,
		hunger, maxHunger = 100,
		energy = 100, maxEnergy = 100;
	public float 
		secondsWillIdle = 3, speed = 5f, turnSpeed = 10f,
		addHungerEvery = 10, minusEnergyEvery = 30;
	public bool hungerOverTime = true, EnergyOverTime = true;
	
	private Citizen_GetMedical getMedical;
	private Citizen_GetFood getFood;
	private Citizen_GetRest getRest;
	
	void Awake () {
		getMedical = GetComponent<Citizen_GetMedical>();
		getFood = GetComponent<Citizen_GetFood>();
		getRest = GetComponent<Citizen_GetRest>();
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
			
		//Priority 1: If the Citizen is injured it will do this.
		if (healthPercent < 75) {
			getMedical.Go();
		}
		//Priority 2: If the Citizen is hungry it will do this.
		else if (hungerPercent > 75) {
			getFood.Go();
		}
		//Priority 3: If the Citizen is tired it will do this.
		else if (energyPercent < 85) {
			getRest.Go();
		}
		//Priority 4: If there are resources related to it's job on the field, it will do this.
		//else if () {
		//	
		//}
		//Priority 5: If there are locations to do it's currently assigned job, it will do this.
		//else if () {
		//	
		//}
		//Priority 6: If there is nothing to do the Citizen will do this.
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
