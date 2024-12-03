using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EatHandler : MonoBehaviour
{
	[SerializeField] Spawner fishSpawner;
	[SerializeField] Gulpert player;
	[SerializeField] AudioSource audioSource;
	TunaFish tunaFish;
	BungaFish bungaFish;
	BossFish bossFish;

	public static int fishEaten = 0;

	void Start()
	{
		if (player == null)
		{
			Debug.LogError("Player reference is not assigned in EatHandler.");
		}
	}

	public void AttemptToEat(Collision2D other)
	{

		if (other.gameObject.CompareTag("Tuna"))
		{
			// Try to get the TunaFish component from the collided object
			tunaFish = other.gameObject.GetComponent<TunaFish>();

			if (tunaFish == null) // Check if the TunaFish component is missing
			{
				Debug.LogError($"TunaFish component not found on {other.gameObject.name}");
				return;
			}

			if (other.gameObject.GetComponent<CapsuleCollider2D>() == null)
			{
				Debug.LogError($"CapsuleCollider2D not found on {other.gameObject.name}");
				return;
			}

			// Prevent changing Collider value depending on collision point
			if (!tunaFish.HasSize)
			{
				// Calculate collider size and assign it to the TunaFish instance
				tunaFish.ColliderSize = other.gameObject.GetComponent<CapsuleCollider2D>().bounds.size.magnitude;
				tunaFish.HasSize = true;
			}

			// Update ColliderSize
			player.PlayerColliderSize = player.GetComponent<CapsuleCollider2D>().bounds.size.magnitude;

			Debug.Log($"The size of {other.gameObject.name} is {tunaFish.ColliderSize}");
			Debug.Log($"The size of {player.gameObject.name} is {player.PlayerColliderSize}");

			// Compare player and tuna sizes for eating mechanics
			if (player.PlayerColliderSize > tunaFish.ColliderSize + player.EatThreshold)
			{
				Destroy(other.gameObject);
				fishEaten++;

				audioSource.Play();

				Debug.Log($"Fish eaten: {fishEaten}");
				player.GrowPlayer(tunaFish.RandomScale);
				fishSpawner.TunaCount--;
			}
			else if (tunaFish.ColliderSize > player.PlayerColliderSize + player.EatThreshold)
			{
				SceneManager.LoadScene("MainMenu");
			}
		}

		if (other.gameObject.CompareTag("Bunga"))
		{
			// Try to get the TunaFish component from the collided object
			bungaFish = other.gameObject.GetComponent<BungaFish>();

			if (bungaFish == null) // Check if the TunaFish component is missing
			{
				Debug.LogError($"BungaFish component not found on {other.gameObject.name}");
				return;
			}

			if (other.gameObject.GetComponent<CapsuleCollider2D>() == null)
			{
				Debug.LogError($"CapsuleCollider2D not found on {other.gameObject.name}");
				return;
			}

			// Prevent changing Collider value depending on collision point
			if (!bungaFish.HasSize)
			{
				// Calculate collider size and assign it to the TunaFish instance
				bungaFish.ColliderSize = other.gameObject.GetComponent<CapsuleCollider2D>().bounds.size.magnitude;
				bungaFish.HasSize = true;
			}

			// Update ColliderSize
			player.PlayerColliderSize = player.GetComponent<CapsuleCollider2D>().bounds.size.magnitude;

			Debug.Log($"The size of {other.gameObject.name} is {bungaFish.ColliderSize}");
			Debug.Log($"The size of {player.gameObject.name} is {player.PlayerColliderSize}");

			// Compare player and tuna sizes for eating mechanics
			if (player.PlayerColliderSize > bungaFish.ColliderSize + player.EatThreshold)
			{
				Destroy(other.gameObject);
				fishEaten++;

				audioSource.Play();

				Debug.Log($"Fish eaten: {fishEaten}");
				player.GrowPlayer(bungaFish.RandomScale);

				fishSpawner.BungaCount--;
			}
			else if (bungaFish.ColliderSize > player.PlayerColliderSize + player.EatThreshold)
			{
				SceneManager.LoadScene("MainMenu");
			}
		}


		if (other.gameObject.CompareTag("Boss"))
		{
			// Try to get the TunaFish component from the collided object
			bossFish = other.gameObject.GetComponent<BossFish>();

			if (bossFish == null) // Check if the TunaFish component is missing
			{
				Debug.LogError($"BungaFish component not found on {other.gameObject.name}");
				return;
			}

			if (other.gameObject.GetComponent<CapsuleCollider2D>() == null)
			{
				Debug.LogError($"CapsuleCollider2D not found on {other.gameObject.name}");
				return;
			}

			// Prevent changing Collider value depending on collision point
			if (!bossFish.HasSize)
			{
				// Calculate collider size and assign it to the TunaFish instance
				bossFish.ColliderSize = other.gameObject.GetComponent<CapsuleCollider2D>().bounds.size.magnitude;
				bossFish.HasSize = true;
			}

			// Update ColliderSize
			player.PlayerColliderSize = player.GetComponent<CapsuleCollider2D>().bounds.size.magnitude;

			Debug.Log($"The size of {other.gameObject.name} is {bossFish.ColliderSize}");
			Debug.Log($"The size of {player.gameObject.name} is {player.PlayerColliderSize}");

			// Compare player and tuna sizes for eating mechanics
			if (player.PlayerColliderSize > bossFish.ColliderSize + player.EatThreshold)
			{
				Destroy(other.gameObject);
				fishEaten++;

				audioSource.Play();

				Debug.Log($"Fish eaten: {fishEaten}");

				WinGame();
			}
			else if (bossFish.ColliderSize > player.PlayerColliderSize + player.EatThreshold)
			{
				SceneManager.LoadScene("MainMenu");
			}
		}
	}

	void WinGame()
	{
		StartCoroutine(PauseAndLoadScene());

		IEnumerator PauseAndLoadScene()
		{
			Time.timeScale = 0;  // Pause the game

			yield return new WaitForSecondsRealtime(3f);  // Wait for 3 seconds using real-time

			Time.timeScale = 1;  // Pause the game

			SceneManager.LoadScene("WinningScene");  // Load the winning scene
		}
	}

}
