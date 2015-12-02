using UnityEngine;
using System.Collections;

/// <summary>
/// PlayerMovement Class manage the overall control of Player Movement, including rotation. Movement is achieve by using 
/// Input.GetAxis while Rotation of player is
/// achive by Raycasting and follwing the mouse movement in the screen. 
/// </summary>
public class PlayerMovement : MonoBehaviour {

	#region Public Variables

	public float MovementSpeed = 10.0f;
	public Rigidbody PlayerRigidbody;
	public float playerSpeed = 1f;

	#endregion 

	#region Private Variables

	Vector3 movement;
	Animator PoliceAnimator;
	int floorMask;	
	
	#endregion

	#region Main Function
		void Awake()
		{
			PlayerRigidbody = GetComponent<Rigidbody>();					  //Get the Rigidbody component for player
			floorMask = LayerMask.GetMask("floor");							  //[4] -- get the layerMask in floor layer set in Unity

			PoliceAnimator = GetComponent<Animator>();						 //Get the Animator Component from player
			
		}
	
		//FixedUpdate gave the batter movement of character than Update so was used insted of Update function.
		void FixedUpdate()
		{

			float moveHorizontal = Input.GetAxisRaw("Horizontal"); 				// [1] -- we use GetAxisRaw to either of three state 
			float moveVertical = Input.GetAxisRaw("Vertical");					//(-1, 0, 1) not in range of -1 to 1.

		/**[Mobile Input]
			float moveHorizontal =  Input.acceleration.x;					
			float moveVertical= Input.acceleration.y; 						
		**/
			//call function
			MovementFunction (moveHorizontal, moveVertical);					//Call Movement Function - moves Player up/down, left/right
			Turning ();															//Call Turning - Look to mouse position on the screen
			Animation(moveHorizontal, moveVertical);							//Call Animation - Check if V/H are not 0, and play walking animation
			
		}
		
	#endregion

	#region Utility Functions
	  void MovementFunction(float moveHorizontal, float moveVertical)
		{
																				
		movement.Set (moveHorizontal, 0.0f, moveVertical);			      				//Assiging movement with x,z. Fixed y to restrict up/down
		movement = movement.normalized * MovementSpeed * Time.deltaTime;   //[2] normalized was necessary because if you go in diagonal h+v will give 1.04		

		PlayerRigidbody.MovePosition (transform.position + movement); 					// move player according to input / using Physics

		}

 void Turning()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);  // [3] -- now we are casting a ray from the camera but from the position where mouse icon is and store it to camRay
		RaycastHit floorHit;											  //to get the data that we sent in first like ^ we need RaycastHit.

		/** 
		 [5] -- RayCast is boolen (Give a position of ray, give a variable to store ray, 
		 how long should ray be and where should ray hit)
		 RayCast(postion, out [variable name], length of ray, location) 
		 */

		if (Physics.Raycast(camRay,out floorHit, 100.0f, floorMask ))		  //if its true
		{
			Vector3 playerToMouse = floorHit.point - transform.position;     //Find the relative position in the scene
			playerToMouse.y = 0.0f; 										  //make sure vector only has x and z value
								

			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);  //[7]Quaternion is used to store data for rotation of player -- this change forward direction of player towards playerToMouse
			PlayerRigidbody.MoveRotation(newRotation);						 //[8]assign the newRotation value to player's rigidbody rotation value
		}
	}

		void Animation(float moveHorizontal, float moveVertical)
		{
			bool isWalking = moveHorizontal != 0.0f || moveVertical != 0.0f;	//check if player is moving or not
			PoliceAnimator.SetBool ("Walking", isWalking); 					  //[6] --Set Walking bool to True or Fals

		}
	


}

	#endregion


#region Reference
/** 
 * Reference: 
1. http://docs.unity3d.com/ScriptReference/Input.GetAxisRaw.html
2. http://www.wolframalpha.com/input/?i=diagonal%20of%201x1%20square
3. http://docs.unity3d.com/ScriptReference/Camera.ScreenPointToRay.html
4. http://docs.unity3d.com/ScriptReference/LayerMask.GetMask.html
5. http://docs.unity3d.com/ScriptReference/Physics.Raycast.html
6. http://docs.unity3d.com/ScriptReference/Animator.html
7. http://docs.unity3d.com/ScriptReference/Quaternion.LookRotation.html
8.http://docs.unity3d.com/ScriptReference/Rigidbody.MoveRotation.html
 **/
#endregion