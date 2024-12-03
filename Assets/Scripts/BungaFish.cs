using UnityEngine;

public class BungaFish : MonoBehaviour
{

	[Header("Attributes")]
	float speed = 0;
	[SerializeField] float randomScale = 1f;
	float travelDistance = 200f;
	float searchDistance = 100f;
	float minDistance = 80f;

	float colliderSize = 0;
	bool hasSize = false;

	/*---------ProcGenAttributes----------*/

	int seed = 0;
	float minSpeed = 35;
	float maxSpeed = 75;
	float minSize = 2.5f;
	float maxSize = 15f;

	public void Generate(int newSeed)
	{
		seed = newSeed;
		Random.InitState(seed);
		randomScale = Random.Range(minSize, maxSize); // add random scale * multiplier to match playerfish size
		transform.localScale = new Vector3(randomScale, randomScale, randomScale);
		speed = Random.Range(minSpeed, maxSpeed);
	}

	void Awake()
	{
		Generate(Random.Range(int.MinValue, int.MaxValue));
	}

	public float Speed
	{
		get { return speed; }
		set { speed = value; }
	}

	// Getter and Setter for RandomScale
	public float RandomScale
	{
		get { return randomScale; }
		set { randomScale = value; }
	}

	// Getter and Setter for travelDistance
	public float TravelDistance
	{
		get { return travelDistance; }
		set { travelDistance = value; }
	}

	public float SearchDistance
	{
		get { return searchDistance; }
		set { searchDistance = value; }
	}

	public float MinDistance
	{
		get { return minDistance; }
		set { minDistance = value; }
	}

	// Getter and Setter for Seed
	public int Seed
	{
		get { return seed; }
		set { seed = value; }
	}

	public float ColliderSize
	{
		get { return colliderSize; }
		set { colliderSize = value; }
	}

	public bool HasSize
	{
		get { return hasSize; }
		set { hasSize = value; }
	}
}
