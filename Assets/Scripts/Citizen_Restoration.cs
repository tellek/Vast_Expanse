using UnityEngine;
using System.Collections;

public class Citizen_Restoration : MonoBehaviour {
	
	private bool restoring = false;
	private string myTag;
	
	void Start () {
		myTag = transform.tag;
	}
	
	void Update () {
		GameObject[] citizens = GameObject.FindGameObjectsWithTag("Citizen");
		foreach (GameObject aCit in citizens) {
			if(aCit.transform.position == transform.position) {
				StartCoroutine(BeginWork(aCit));
				
				double health = aCit.GetComponent<Citizen_Behavior>().health;
				double hunger = aCit.GetComponent<Citizen_Behavior>().hunger;
				double energy = aCit.GetComponent<Citizen_Behavior>().energy;
				double maxHealth = aCit.GetComponent<Citizen_Behavior>().maxHealth;
				double maxEnergy = aCit.GetComponent<Citizen_Behavior>().maxEnergy;
				
				if(myTag == "Medical" && health >= maxHealth) {
					aCit.GetComponent<Citizen_Behavior>().restoring = false;
				}
				else if(myTag == "Food" && hunger <= 0) {
					aCit.GetComponent<Citizen_Behavior>().restoring = false;
				}
				else if(myTag == "Home" && energy >= maxEnergy) {
					aCit.GetComponent<Citizen_Behavior>().restoring = false;
				}
			}
		}
	}
	
	IEnumerator BeginWork(GameObject aCit)
	{
		if(myTag == "Medical" && restoring == false) {
			restoring = true;
			yield return new WaitForSeconds (0.1f);
			aCit.GetComponent<Citizen_Behavior>().health++;
			restoring = false;
			}
		else if(myTag == "Food" && restoring == false) {
			restoring = true;
			yield return new WaitForSeconds (0.2f);
			aCit.GetComponent<Citizen_Behavior>().hunger--;
			restoring = false;
			}
		else if(myTag == "Home" && restoring == false) {
			restoring = true;
			yield return new WaitForSeconds (0.3f);
			aCit.GetComponent<Citizen_Behavior>().energy++;
			restoring = false;
			}
	
	}
}
