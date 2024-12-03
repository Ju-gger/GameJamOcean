using UnityEngine;

public class Gulpert : MonoBehaviour
{
	[SerializeField] EatHandler eatHandler;

	[Header("Camera Helper")]
	[SerializeField] Camera mainCamera;
	float initialCameraScale;
	float initialPlayerScale;
	float currentPlayerScale;
	float cameraScaleRatio;


	[Header("Movement")]
	[SerializeField] float speed = 100;
	[SerializeField] float speedLimit = 7;

	[Header("Growth Settings")]
	[SerializeField] float baseEatThreshold = 0.4f;  // Base threshold at the initial scale
	[SerializeField] float thresholdScalingFactor = 1f;  // Adjust based on how sensitive you want the threshold to be
	float eatThreshold = .4f;
	float playerGrowthFactor = .1f;
	float playerColliderSize;


	Rigidbody2D rb;
	SpriteRenderer sr;

	void Awake()
	{
		// connect to component i.e RigidBody
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();

		playerColliderSize = GetComponent<CapsuleCollider2D>().bounds.size.magnitude;
		initialPlayerScale = transform.localScale.x;  // Assuming uniform scaling

		// Get the orthographic size of the camera (only works if the camera is orthographic)
		initialCameraScale = mainCamera.orthographicSize;
		Debug.Log("Camera Size: " + initialCameraScale); // Optional: Log the camera size for debugging
	}

	void FixedUpdate()
	{
		/* Sets a speedLimit, Useful when dealing with forces that cause movement (i.e AddForce)*/
		if (rb.velocity.magnitude > speedLimit)
		{
			rb.velocity = rb.velocity.normalized * speedLimit;
		}
	}

	public void Move(Vector3 movement)
	{
		// Adds a force that pushes object around. Useful for Glidey, Space-like movement
		rb.AddForce(movement * speed);
	}

	public void MoveToward(Vector3 target)
	{

		target.z = 0; // since it is 2D we dont want to change Z

		Vector3 direction = target - transform.position; // face towards target
		Move(direction.normalized); // normalize to give a CONSTANT value to so attributes stay constant despite distance

	}


	// TODO 
	// REFERENCE GAMEOBJECT ON COLLISION, THEN USE IT ATTRIBUTES
	// CHANGE COLLIDER SIZE ACCURATELY CHECK MAGNITUDE
	void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log("Collision Detected with " + other.gameObject);
		if (other.gameObject.CompareTag("WorldBorder")) { return; } // Ignore collisions with world borders

		eatHandler.AttemptToEat(other);
	}


	// Handles death situation
	public void HandleDeath()
	{

	}


	// Grow the player after consuming an object
	public void GrowPlayer(float consumedSize)
	{
		// Calculate how much the player will grow based on the consumed object's size
		float growthAmount = consumedSize * playerGrowthFactor;
		Debug.Log($"Consumed Size: {consumedSize}, Growth Amount: {growthAmount}");

		// Increase the player's scale (x, y, z)
		transform.localScale += Vector3.one * growthAmount;
		Debug.Log($"New Player Scale: {transform.localScale}");

		// **Update eatThreshold dynamically based on player scale**
		eatThreshold = baseEatThreshold + (currentPlayerScale - initialPlayerScale) * thresholdScalingFactor;
		Debug.Log($"Updated Eat Threshold: {eatThreshold}");

		// Update the camera scaling ratio based on the current player scale relative to the initial scale
		currentPlayerScale = transform.localScale.x;  // Assuming uniform scaling, so we use the x-axis
		cameraScaleRatio = (currentPlayerScale / initialPlayerScale) * .8f;
		Debug.Log($"Camera Scale Ratio: {cameraScaleRatio}");

		// Adjust the camera's orthographic size to keep the player proportionally visible as they grow
		mainCamera.orthographicSize = initialCameraScale * cameraScaleRatio;
		Debug.Log($"Updated Camera Orthographic Size: {mainCamera.orthographicSize}");
	}


	public float EatThreshold
	{
		get { return eatThreshold; }
		set { eatThreshold = value; }
	}

	public float PlayerColliderSize
	{
		get { return playerColliderSize; }
		set { playerColliderSize = value; }
	}

}
