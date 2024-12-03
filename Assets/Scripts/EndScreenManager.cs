using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndScreenManager : MonoBehaviour
{
	
	[SerializeField] TextMeshProUGUI textMeshProUGUI;

    void Start()
    {
        textMeshProUGUI.SetText("Total Fish Eaten: " + EatHandler.fishEaten);
    }

	public void PlayAgain(){
		EatHandler.fishEaten = 0;
		SceneManager.LoadScene("GameScene");
	}


	public void Quit(){
		Application.Quit();
	}
}
