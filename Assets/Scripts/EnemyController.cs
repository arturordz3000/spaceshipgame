using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	private Vector3 velocityToTarget = Vector3.zero;

	public float speed = 1.0f;
	public GameObject debugTarget;

	// Use this for initialization
	void Start () {
		SetTarget (debugTarget);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += velocityToTarget * Time.deltaTime;
	}

	void OnCollisionEnter2D (Collision2D collision) {
		Destroy (this.gameObject);
		Destroy (collision.collider.gameObject);
	}

	public void SetTarget(GameObject target) {
		Vector3 position = this.transform.position;
		Vector3 targetPosition = target.transform.position;

		Vector3 distanceVector = targetPosition - position;
		velocityToTarget = distanceVector * speed;
	}
}
