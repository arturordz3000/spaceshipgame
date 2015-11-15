using UnityEngine;
using System.Collections;

public class RayController : MonoBehaviour {

	public AudioClip soundEffect;
	private AudioSource audioPlayer;

	// Use this for initialization
	void Start () {
		audioPlayer = GetComponent<AudioSource> ();
		audioPlayer.PlayOneShot (soundEffect);
	}


	
	// Update is called once per frame
	void Update () {
		DestroyIfOutOfScreen ();
	}

	void DestroyIfOutOfScreen() {
		Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
		if (screenPosition.y > Screen.height || screenPosition.y < 0 || screenPosition.x > Screen.width || screenPosition.x < 0)
			Destroy(this.gameObject);
	}
}
