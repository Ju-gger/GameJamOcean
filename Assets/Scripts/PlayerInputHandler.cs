using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{

	[SerializeField] Gulpert playerCharacter;

	void Start()
	{
		playerCharacter.GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{

		// call Aim Player with a mouse as a param
		//playerCharacter.AimPlayer(Camera.main.ScreenToWorldPoint(Input.mousePosition));

	}


	void FixedUpdate()
	{
		Vector3 movement = Vector3.zero;

		if (Input.GetKey(KeyCode.W))
		{
			movement += new Vector3(0, 1, 0);   // Move up
		}
		if (Input.GetKey(KeyCode.A))
		{
			movement += new Vector3(-1, 0, 0);  // Move left
			playerCharacter.transform.rotation = Quaternion.Euler(0, 180, 0); 
		}
		if (Input.GetKey(KeyCode.S))
		{
			movement += new Vector3(0, -1, 0);  // Move down
		}
		if (Input.GetKey(KeyCode.D))
		{
			movement += new Vector3(1, 0, 0);  // Move right
			playerCharacter.transform.rotation = Quaternion.Euler(0, 0, 0); 

		}

		playerCharacter.Move(movement);
	}


	// returns object playerCharacter
	public Gulpert GetGulpert()
	{
		return playerCharacter;
	}

}
