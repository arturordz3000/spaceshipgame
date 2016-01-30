using UnityEngine;
using System.Collections;

public enum MovementType {
	Translation,
	Inertia,
	None
}

public class SpaceshipController : MonoBehaviour {
	
	private int movementDirection = 0;
	private float mainGunShootDelta = 0;
	private Transform mainGunTransform;

	public ParticleEmitter rocketEngine;
	public float speed = 0.2f;
	public Projectile mainGunProjectile;
	public float mainGunShootFrequency = 0.1f;

	// Use this for initialization
	void Start () {
		mainGunTransform = transform.FindChild ("MainGunShooter");
		rocketEngine = transform.FindChild ("RocketEngine").particleEmitter;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateInput ();
		ApplyTransformations ();
		UpdateDeltas ();
	}

	void UpdateInput() {
		movementDirection = 0;

		if (Input.GetKey(KeyCode.RightArrow)) {
			movementDirection = 1;
		} else if (Input.GetKey(KeyCode.LeftArrow)) {
			movementDirection = -1;
		}

		if (Input.GetKey (KeyCode.Space)) {
			ShootMainGun();
		}
	}

	void UpdateDeltas() {
		mainGunShootDelta += Time.deltaTime;
	}

	void ApplyTransformations() {
		ApplyMovement ();
	}

	void ApplyMovement() {
		transform.position += transform.right * speed * movementDirection;
	}

	void ShootMainGun() {
		if (mainGunShootDelta > mainGunShootFrequency) {
			Projectile ray = Instantiate (mainGunProjectile, mainGunTransform.position, mainGunTransform.rotation) as Projectile;
			mainGunShootDelta = 0;
			ray.shooter = this.gameObject;

			Physics2D.IgnoreCollision(ray.collider2D, this.collider2D);
		}
	}

	public override string ToString ()
	{
		return string.Format ("Current Rotation: {0}\n", transform.up);
	}
}
