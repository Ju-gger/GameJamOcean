using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
	// Creates a transform object used to control or move those objects 
	[SerializeField] Transform playerTransform;

    // Update is called once per frame
    void Update()
    {
		// the camera position is moved to the x and y position of our playerTransform obj (which we will put in as character BUT can put anything to follow)
        transform.position = new Vector3(playerTransform.position.x,playerTransform.position.y,-10);
    }



}
