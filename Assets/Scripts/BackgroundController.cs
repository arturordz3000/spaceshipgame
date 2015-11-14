using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {

	public bool scrollEnabled = false;
	public SpaceshipController mainCharacter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (scrollEnabled) {
			Vector2 offset = new Vector2(mainCharacter.transform.up.x, mainCharacter.transform.up.y);
			renderer.material.mainTextureOffset += offset * mainCharacter.GetCurrentSpeed() * Time.deltaTime;
		}
	}
}
