using UnityEngine;
using System.Collections;

public enum MovementType {
	Translation,
	Inertia,
	None
}

public class SpaceshipController : MonoBehaviour {

	// This speed value is only used by the background
	private float currentSpeed = 0;

	private bool didAccelerate = false;
	private int rotationDirection = 0;

	public bool rotateEnabled = true;
	public float maxSpeed = 0.2f;
	public float acceleration = 0.1f;
	public float friction = 0.3f;
	public float rotationalSpeed = 100f;
	public Camera camera;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		UpdateInput ();
		UpdateSpeedValues ();
		ApplyTransformations ();
	}

	void UpdateInput() {
		didAccelerate = false;
		rotationDirection = 0;

		if (Input.GetKey(KeyCode.UpArrow)) {
			didAccelerate = true;
		}

		if (Input.GetKey(KeyCode.RightArrow)) {
			rotationDirection = -1;
		} else if (Input.GetKey(KeyCode.LeftArrow)) {
			rotationDirection = 1;
		}
	}
	
	void UpdateSpeedValues() {
		if (didAccelerate) {
			currentSpeed = currentSpeed < maxSpeed ? currentSpeed + acceleration * Time.deltaTime : maxSpeed;
		} else {
			currentSpeed = currentSpeed > 0 ? currentSpeed - friction * Time.deltaTime : 0;
		}
	}

	void ApplyTransformations() {

		ApplyMovement ();

		if (rotateEnabled) {
			transform.Rotate (Vector3.forward * ((rotationDirection * rotationalSpeed) * Time.deltaTime));
		}
	}

	void ApplyMovement() {
		// Impulse
		if (didAccelerate)
			rigidbody2D.AddForce (transform.up * maxSpeed * Time.deltaTime, ForceMode2D.Impulse);

		// Friction
		rigidbody2D.AddForce (-rigidbody2D.velocity.normalized * friction * Time.deltaTime, ForceMode2D.Impulse);
	}

	public float GetCurrentSpeed()
	{
		return currentSpeed;
	}

	public override string ToString ()
	{
		return string.Format ("Current Rotation: {0}\n", transform.up);
	}
}
