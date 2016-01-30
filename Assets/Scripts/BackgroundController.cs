using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {

	public bool scrollEnabled = true;
	public float scrollSpeed = 0.1f;
	public SpaceshipController mainCharacter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (scrollEnabled) {
			Vector2 offset = new Vector2(0, 1);
			renderer.material.mainTextureOffset += offset * scrollSpeed * Time.deltaTime;
		}

		mainCharacter.rocketEngine.emit = scrollEnabled;
	}
}
