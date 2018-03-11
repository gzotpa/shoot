using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (!this.gameObject.GetComponent<Collider2D> ().isTrigger) {
			GameObject obj = Instantiate (this.gameObject);
			obj.GetComponent<Collider2D> ().isTrigger = true;
			obj.transform.SetParent (this.gameObject.transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
