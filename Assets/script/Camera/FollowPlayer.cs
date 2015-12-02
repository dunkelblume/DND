using UnityEngine;
using System.Collections;

/// <summary>
/// FollowPlayer Script deals with movement of camera. The movement of camera is based on 
/// player current position + difference between Main Camera and player position. 
/// ie. in this cause, Y position of Player is fix so Camera will have fixed Y, whereas X and Z are checked and change in every second
/// </summary>
public class FollowPlayer : MonoBehaviour {


	#region Private Variables
	Transform FindthePolice;
	float smoothing = 5.0f;
	Vector3 offset;
	#endregion

	#region Main Function
	void Start()
	{
		
		FindthePolice  = GameObject.Find ("Police").GetComponent<Transform>();

		offset = transform.position - FindthePolice.position;				//transform.position is camara position 
																			//& target.position is position of player and offset is difference between them
	}
	
	void Update ()
	{

		if (FindthePolice != null)
		{
		Vector3 targetCamPos = FindthePolice.position + offset;				//Create a V3 with current PlayerPosition and distance between Player and camera
			//[1] -- to move the camara position, we use Vector3 lerp, 
			//just like morph -- to give a smooth feeling and how fast / per second
			transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime); 
		
		}
	}
}
	#endregion


/* [1] http://docs.unity3d.com/ScriptReference/Vector3.Lerp.html */