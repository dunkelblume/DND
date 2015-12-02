using UnityEngine;
using System.Collections;

/// <summary>
/// EnemyMovement Script deals with Zombie Movement. In this class, Zombie are given a target position ie. PlayerLocation 
/// </summary>
public class EnemyMovement : MonoBehaviour {
	
#region Private Variables
														
	GameObject Player;																//Get a Player GameObject
	Transform PlayerLocation;	
	PlayerHealth playerHealth;

	EnemyHealth enemyHealth;
	NavMeshAgent ZombieNav;  														// [1] About NavMesh


#endregion

#region Main Function

	void Start () {

		Player = GameObject.Find("Police");									    	//Had to use Police rather than player as it was not findingwith tag

		PlayerLocation = Player.transform;
		playerHealth = Player.GetComponent<PlayerHealth>();							//Get PlayerHealth Script from Player

		ZombieNav = GetComponent<NavMeshAgent> ();								   //Get NavMesh Component from Zombie 
		enemyHealth = GetComponent<EnemyHealth>();								   //Get enemyHealth Script from Zombie

	}

	// [Note] - Why if? - After Enenmy Dies, navmesh agent will give a error for not haveing any navmesh agent
	/// <summary>
	/// The update function here checks the health of Zombie, Player and also life of player. If health is higher than 0 and life is less than 3
	/// then Nav Mesh agent of Zombie is given a Location the player as target. 
	/// </summary>
	void Update () {

			if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth >0 && playerHealth.Life < 3)	//Check the health of Zombie, Player and life too
			{
				ZombieNav.SetDestination (PlayerLocation.position); 			//Give Nav Agent a target
			}		

			else 
			{
				ZombieNav.enabled = false;										//Turn off NavAgent if it Zombies/Player is dead
			}
		}
 

}
#endregion

#region Reference

//[1] https://unity3d.com/learn/tutorials/modules/beginner/live-training-archive/navmeshes

#endregion