using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start() {
		Debug.Log("Start");
	}

	// Update is called once per frame
	void Update() {
		if (transform.localPosition.x < -200 || transform.localPosition.x > 200 ||
			transform.localPosition.y < -200 || transform.localPosition.y > 200)
			Destroy(this.gameObject);
	}

	void OnCollisionEnter2D(Collision2D collision) {
		Destroy(this.gameObject);
	}
}
