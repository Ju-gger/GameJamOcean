using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour
{

	// Starts game
    public void StartGame()
	{
		SceneManager.LoadScene("GameScene"); // Loads Scene
	}


	public void Quit()
	{
		Application.Quit(); // Loads Scene
	}
}
