using UnityEngine;

public class TunaAI : MonoBehaviour
{
	[SerializeField] TunaFish enemyFish;
	Rigidbody2D rb;

	float bounceForce = 500f;

	/* AI Helpers */
	delegate void AIState();
	AIState currentState;

	/*-----------Trackers-------------*/
	float stateTime = 0;
	bool justChangedState = false;
	Vector3 patrolPos;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

    	void Start()
	{
		ChangeState(PassiveState);
	}

	void ChangeState(AIState newAIState)
	{
		currentState = newAIState;
		stateTime = 0;
		justChangedState = true;
	}

	/*
	void AttackState()
	{
		// move and aim toward the player 
		MoveToward(player.transform.position);
		AimPlayer(player.transform);

		if (Vector3.Distance(transform.position, player.transform.position) > enemyFish.SightDistance)
		{
			ChangeState(PassiveState);
			return;
		}

	}
	*/

	void PassiveState()
	{

		// If we just started the patrol state; calculate a patrol position to MoveToward
		if (stateTime == 0 || Vector3.Distance(transform.position, patrolPos) < 0.2f)
		{
			// Generate a random direction
			Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

			// Scale to ensure it is within the min and max distance range
			float distance = Random.Range(enemyFish.MinDistance, enemyFish.TravelDistance);
			patrolPos = transform.position + randomDirection * distance;

		}

		MoveToward(patrolPos);
		AimPlayer(patrolPos);

	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("WorldBorder"))
		{
			Vector2 bounceDirection = -rb.velocity.normalized;  // Reverse current direction
			rb.velocity = Vector2.zero;  // Reset current velocity
			rb.AddForce(bounceDirection * bounceForce, ForceMode2D.Impulse);

			// Optional: Switch back to PassiveState to resume normal behavior
			ChangeState(PassiveState);
		}
	}


	// rotates player to aim at target
	public void AimPlayer(Transform targetTransform)
	{
		transform.rotation = Quaternion.LookRotation(Vector3.forward, targetTransform.position - transform.position);
		AimAndFlip();
	}

	public void AimPlayer(Vector3 aimPos)
	{
		transform.rotation = Quaternion.LookRotation(Vector3.forward, aimPos - transform.position);
		AimAndFlip();
	}

	public void MoveToward(Vector3 target)
	{

		target.z = 0; // since it is 2D we dont want to change Z

		Vector3 direction = target - transform.position; // face towards target
		Move(direction.normalized); // normalize to give a CONSTANT value to so attributes stay constant despite distance

	}

	public void Move(Vector3 movement)
	{
		rb.velocity = movement * enemyFish.Speed;
		// Adds a force that pushes object around. Useful for Glidey, Space-like movement
		//rb.AddForce(movement * speed);
	}


	void AITick()
	{
		// Call the current state method to execute its behavior.
		if (justChangedState)
		{
			stateTime = 0;
			justChangedState = false;
		}
		currentState();
		stateTime += Time.deltaTime;
	}


	void Update()
	{
		if (currentState != null)
		{
			AITick();
		}
	}

	// Helper function to aim and flip the sprite
	private void AimAndFlip()
	{
		//float angleFlipThreshold = 10;

		// Flip the sprite based on the X direction
		if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)  // Moving right
		{
			transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		}
		else
		{
			transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		}
	}
}
