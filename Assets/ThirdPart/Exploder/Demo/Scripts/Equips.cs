using UnityEngine;
using System.Collections;

public class Equips : MonoBehaviour {
	
	public GameObject gun;
	public GameObject grenade;
	// Use this for initialization
	public void HideWeapons(bool hide)
	{
		if (hide) {
			gun.transform.Find ("Reload").gameObject.SetActive (false);
			gun.transform.Find ("ShotgunMain").gameObject.SetActive (false);
			grenade.transform.GetComponentInChildren<Renderer> ().enabled = false;
		} else {
			gun.transform.Find ("Reload").gameObject.SetActive (true);
			gun.transform.Find ("ShotgunMain").gameObject.SetActive (true);
			grenade.transform.GetComponentInChildren<Renderer> ().enabled = true;
		}
	}

}
