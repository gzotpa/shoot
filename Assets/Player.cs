using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public GameObject bulletPrefab;
	public float bulletPositionDelta;
	public float moveVelocity;
	public float maxVelocity;
	public float bulletVelocity;
	public float maxJump;
	public float moveJump;

	private bool toLeft;
	private int jumpMax, jumpNow;

	//    private LineRenderer lineRenderer;
	//    private Vector3 a, b;
	// Use this for initialization
	void Start() {
		jumpMax = 2;
		jumpNow = 0;
		toLeft = true;
		//        lineRenderer.positionCount = 2;
	}
	void KillMe() {
		Destroy(this.gameObject);
	}
	// Update is called once per frame
	void Update() {
		if (jumpNow == 0 && gameObject.GetComponent<Rigidbody2D>().velocity.y != 0f) {
			jumpNow += 1;
		}
		/*        a = new Vector3(transform.localPosition.x - 0.5f, transform.localPosition.y, transform.localPosition.z);
                b = new Vector3(transform.localPosition.x + 0.5f, transform.localPosition.y, transform.localPosition.z);
                lineRenderer.SetPosition(0, a);
                lineRenderer.SetPosition(1, b);*/
		if (transform.localPosition.x < -200 || transform.localPosition.x > 200 ||
			transform.localPosition.y < -200 || transform.localPosition.y > 200) {
			KillMe();
			return;
		}
		// Left mouse key: shoot
		if (Input.GetMouseButtonDown(0)) {
			GameObject bullet = Instantiate(bulletPrefab);
			Transform bulletTransform = bullet.GetComponent<Transform>();
			Rigidbody2D bulletRigidbody2D = bullet.GetComponent<Rigidbody2D>();

			if (toLeft) {
				Vector3 position = transform.localPosition;
				position.x -= bulletPositionDelta;
				bulletTransform.localPosition = position;

				bulletRigidbody2D.velocity = new Vector2(-bulletVelocity, 0f);
			} else {
				Vector3 position = transform.localPosition;
				position.x += bulletPositionDelta;
				bulletTransform.localPosition = position;

				bulletRigidbody2D.velocity = new Vector2(bulletVelocity, 0f);
			}
		}

		// Key A and D: move
		Transform playerTransform = GetComponent<Transform>();
		Rigidbody2D playerRigidbody2D = GetComponent<Rigidbody2D>();

		if (Input.GetKey(KeyCode.A)) {
			toLeft = true;
			playerTransform.rotation = new Quaternion(0f, 0f, 0f, 0f);

			Vector2 velocity = playerRigidbody2D.velocity;
			velocity.x = Mathf.Max(velocity.x - moveVelocity, -maxVelocity);
			playerRigidbody2D.velocity = velocity;
		}
		if (Input.GetKey(KeyCode.D)) {
			toLeft = false;
			playerTransform.rotation = new Quaternion(0f, 180f, 0f, 0f);

			Vector2 velocity = playerRigidbody2D.velocity;
			velocity.x = Mathf.Min(velocity.x + moveVelocity, maxVelocity);
			playerRigidbody2D.velocity = velocity;
		}

		//Key W: jump
		if (Input.GetKeyDown(KeyCode.W) && jumpNow < jumpMax) {
			jumpNow += 1;
			Vector2 velocity = playerRigidbody2D.velocity;
			velocity.y = Mathf.Min(velocity.y + moveJump, maxJump);
			playerRigidbody2D.velocity = velocity;
		}
	}
	void OnCollisionEnter2D(Collision2D collision) {
		if (transform.localPosition.y >= collision.contacts[0].point.y) {
			jumpNow = 0;
		}
		if (transform.localPosition.y <= collision.contacts[0].point.y) {
			//            collision.gameObject.
			//            jumpNow = 0;
		}

	}

	void OnTriggerStay2D(Collider2D collider) {
		if (Input.GetKeyDown (KeyCode.S) && collider.gameObject.tag == "Background") {
			Physics2D.IgnoreCollision (this.GetComponent<Collider2D> (),
				collider.gameObject.transform.parent.GetComponent<Collider2D> ());
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.gameObject.tag == "Background") {
			Physics2D.IgnoreCollision (this.GetComponent<Collider2D> (),
				collider.gameObject.transform.parent.GetComponent<Collider2D> (), false);
		}
	}
}
