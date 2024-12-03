using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

	[SerializeField] List<GameObject> TunaPrefabs; // List of Tuna PrefabObjects
	[SerializeField] List<GameObject> BungaPrefabs; // List of Bunga PrefabObjects
	[SerializeField] List<GameObject> BossPrefabs; // List of Boss PrefabObjects
	[SerializeField] Transform playerTransform; // player Transfrom(location)
	[SerializeField] float spawnTime = 40; // Time it takes to spawn a new Tuna
	[SerializeField] int startingTunasCount = 50; // Number of Tunas at start
	[SerializeField] int startingBungasCount = 30; // Number of Tunas at start
	[SerializeField] int tunaCount = 0;
	[SerializeField] int bungaCount = 0;
	bool hasBoss = false;

	public float timer = 0; // time until next spawn
	float xSpawnRange = 1750;
	float ySpawnRange = 1350;
	//float timer = 0; // time until next spawn
	Vector3 SpawnPos;
	Vector3 playerPosition;



	void Start()
	{
		SpawnStartingTunas(); // Spawns Tunas at the start of the game
		SpawnStartingBungas();
	}

	void Update()
	{
		if (tunaCount < startingTunasCount) { SpawnTuna(); } // if we have spawned in tunaLimit stop
		if (bungaCount < startingBungasCount) { SpawnBunga(); } // if we have spawned in tunaLimit stop
		SpawnBoss();
	}


	Vector3 FindSpawnPos()
	{
		playerPosition = playerTransform.position; // Assuming you have a player reference

		do
		{
			// Set a random world spawn for new Tuna
			SpawnPos = new Vector3(
				Random.Range(-xSpawnRange, xSpawnRange),
				Random.Range(-ySpawnRange, ySpawnRange),
				0
			);

		} while (Vector3.Distance(SpawnPos, playerPosition) < 100f); // Adjust the distance threshold as needed

		return SpawnPos; // Return the valid spawn position
	}


	void SpawnTuna()
	{
		// Find a valid spawn position
		SpawnPos = FindSpawnPos();

		// Spawn a random tuna prefab at the calculated position with no rotation
		Instantiate(TunaPrefabs[Random.Range(0, TunaPrefabs.Count)], SpawnPos, Quaternion.identity);

		tunaCount++; // Increment tuna count to limit the number of Tunas
	}


	void SpawnBunga()
	{
		// Find a valid spawn position
		SpawnPos = FindSpawnPos();

		// Spawn a random tuna prefab at the calculated position with no rotation
		Instantiate(BungaPrefabs[Random.Range(0, BungaPrefabs.Count)], SpawnPos, Quaternion.identity);

		bungaCount++; // Increment tuna count to limit the number of Tunas
	}


	void SpawnBoss()
	{

		timer += Time.deltaTime;
		if (timer >= spawnTime && !hasBoss)
		{
			timer = 0; // resets timer

			// Find a valid spawn position
			SpawnPos = FindSpawnPos();

			// Spawn a random tuna prefab at the calculated position with no rotation
			Instantiate(BossPrefabs[Random.Range(0, BossPrefabs.Count)], SpawnPos, Quaternion.identity);

			hasBoss = true;
		}
	}



	// Spawns Tunas at the beginning of game 
	void SpawnStartingTunas()
	{
		for (int i = 0; i < startingTunasCount; i++)
		{
			SpawnTuna();
		}
	}

	// Spawns Tunas at the beginning of game 
	void SpawnStartingBungas()
	{
		for (int i = 0; i < startingBungasCount; i++)
		{
			SpawnBunga();
		}
	}


	public int TunaCount
	{
		get { return tunaCount; }
		set { tunaCount = value; }
	}

	public int BungaCount
	{
		get { return bungaCount; }
		set { bungaCount = value; }
	}


	public int BossCount
	{
		get { return bungaCount; }
		set { bungaCount = value; }
	}



}
