using UnityEngine;
using System.Collections;

public class C_RestoreStats : MonoBehaviour {

	private C_Behavior stats;

	void Awake () {
		stats = GetComponent<C_Behavior>();
	}

	public void Go(bool isBusy, string tag) {
		if(isBusy == false) {
			StartCoroutine(ManageStats(tag));
		}
	}

	IEnumerator ManageStats(string tag)
	{
		stats.isBusy = true;

		if(tag == "Medical" && stats.health != stats.maxHealth) {
			yield return new WaitForSeconds (3f);
			stats.health = stats.maxHealth;
			stats.isBusy = false;
		}
		else if(tag == "Food" && stats.hunger != 0) {
			yield return new WaitForSeconds (3f);
			stats.hunger = 0;
			stats.isBusy = false;
		}
		else if(tag == "Home" && stats.energy != stats.maxEnergy) {
			yield return new WaitForSeconds (3f);
			stats.energy = stats.maxEnergy;
			stats.isBusy = false;
		}


	}
}
