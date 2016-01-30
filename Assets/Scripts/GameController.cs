using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public GameObject mainPlayer;
	public List<Transform> enemies = new List<Transform>();

	public int hordeNumber = 1;
	public float enemyInstantiationInterval = 1.0f;
	public float asteroidsInstantiationInterval = 1.0f;
	public bool shouldInstanceEnemies = true;

	private float timeSinceLastEnemyInstantiation = 0.0f;
	private float timeSinceLastAsteroidInstantiation = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		UpdateInstatiationTimes ();

		if (ShouldInstanceNewEnemy ())
			InstanceNewEnemy ();
	}

	bool ShouldInstanceNewEnemy() {
		bool intervalElapsed = timeSinceLastEnemyInstantiation >= enemyInstantiationInterval;
		return intervalElapsed && shouldInstanceEnemies;
	}

	void InstanceNewEnemy() {		
		timeSinceLastEnemyInstantiation = 0.0f;

		float positionX = Random.value > 0.5 ? 0 : Screen.width;
		float positionY = Random.Range (0, Screen.height);

		Vector2 position = Camera.main.ScreenToWorldPoint(new Vector3(positionX, positionY, 0));

		Transform enemy = Instantiate (GetRandomEnemy(), 
		                               position - new Vector2(0, mainPlayer.transform.position.y + mainPlayer.collider2D.bounds.size.y), 
		                               Quaternion.Euler(Vector3.zero)) as Transform;
		enemy.GetComponent<EnemyController> ().SetTarget (mainPlayer);
	}

	Transform GetRandomEnemy() {
		int enemyIndex = Mathf.FloorToInt (Random.Range (0, enemies.Count));
		return enemies[enemyIndex];
	}

	void UpdateInstatiationTimes() {
		timeSinceLastEnemyInstantiation += Time.deltaTime;
		timeSinceLastAsteroidInstantiation += Time.deltaTime;
	}
}
