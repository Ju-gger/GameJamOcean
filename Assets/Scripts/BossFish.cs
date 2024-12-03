using UnityEngine;

public class BossFish : MonoBehaviour
{

	[Header("Attributes")]
	float speed = 80;
	[SerializeField] float randomScale = 45f;
	float travelDistance = 500f;
	float minDistance = 120f;
	float searchDistance = 350f;
	float colliderSize = 0;
	bool hasSize = false;

	void Awake(){
		transform.localScale = new Vector3(randomScale, randomScale, randomScale);
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
