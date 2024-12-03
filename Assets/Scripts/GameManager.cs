using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[Singleton]
public class GameManager : MonoBehaviour, ISingleton
{
	private GameObject _enemyBasic;

	[NonSerialized] public GameObject Player;

	[SerializeField] private float _timeBetweenSpawns;

	void Awake() {
		Player = GameObject.FindWithTag("Player");

		_enemyBasic = Resources.Load<GameObject>("Prefabs/EnemyBasic");
	}

	void Update() {
		// Crude way of spawning enemies for now, will update in future
		_timeBetweenSpawns -= Time.deltaTime;
		if (_timeBetweenSpawns <= 0) {
			_timeBetweenSpawns = 1;

			bool top = Random.value > 0.5f;
			bool left = Random.value > 0.5f;
			Vector2 spawnPositionScreen;
			switch (top, left) {
				case (true, true):
					spawnPositionScreen = new Vector2(-10, -10);
					break;
				case (true, false):
					spawnPositionScreen = new Vector2(-10, Screen.width + 10);
					break;
				case (false, true):
					spawnPositionScreen = new Vector2(Screen.height + 10, -10);
					break;
				case (false, false):
					spawnPositionScreen = new Vector2(Screen.height + 10, Screen.width + 10);
					break;
			}

			var spawnPositionWorld = Camera.main.ScreenToWorldPoint(spawnPositionScreen);
			spawnPositionWorld.z = 0;
			Instantiate(_enemyBasic, spawnPositionWorld, Quaternion.identity);
		}
	}
}
