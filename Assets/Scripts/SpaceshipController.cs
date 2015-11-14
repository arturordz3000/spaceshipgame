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
	private GameObject mainGunProjectile;
	private float mainGunShootDelta = 0;

	public bool rotateEnabled = true;
	public float maxSpeed = 0.2f;
	public float acceleration = 0.1f;
	public float friction = 0.3f;
	public float rotationalSpeed = 100f;
	public Camera camera;
	public float mainGunShootFrequency = 0.1f;

	// Use this for initialization
	void Start () {
		mainGunProjectile = Resources.Load ("Prefabs/RayPrefab") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateInput ();
		UpdateSpeedValues ();
		ApplyTransformations ();
		UpdateDeltas ();
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

		if (Input.GetKey (KeyCode.Space)) {
			ShootMainGun();
		}
	}
	
	void UpdateSpeedValues() {
		if (didAccelerate) {
			currentSpeed = currentSpeed < maxSpeed ? currentSpeed + acceleration * Time.deltaTime : maxSpeed;
		} else {
			currentSpeed = currentSpeed > 0 ? currentSpeed - friction * Time.deltaTime : 0;
		}
	}

	void UpdateDeltas() {
		mainGunShootDelta += Time.deltaTime;
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

	void ShootMainGun() {
		if (mainGunShootDelta > mainGunShootFrequency) {
			Transform mainGunTransform = transform.FindChild ("MainGunShooter");
			GameObject ray = Instantiate (mainGunProjectile, mainGunTransform.position, mainGunTransform.rotation) as GameObject;
			ray.rigidbody2D.AddForce(transform.up * 10, ForceMode2D.Impulse);

			mainGunShootDelta = 0;
		}
	}

	public override string ToString ()
	{
		return string.Format ("Current Rotation: {0}\n", transform.up);
	}
}
