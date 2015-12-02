using UnityEngine;
using System.Collections;

/// <summary>
/// EnemyAttack Script deals with Zombie attacking player . It does this by use of Trigger Collider. When Player collids with Zombie / visevesa,
/// it runs Attack function which send the AttackDamage Value to PlayerHealth Scipt to be minue player health. 
/// In addition, attack is done based of second rather than number of Game Frame Rate.
/// </summary>
public class EnemyAttack : MonoBehaviour {
	
#region Public Variables
	public float AttackInterval = 0.5f; 
	public int AttackDamage = 10;

#endregion

#region Private Variables
	GameObject player;
		bool playerInRange;
		PlayerHealth playerHealth;

	EnemyHealth enemyHealth;

	float timer;


#endregion

#region Main Function
	void Awake ()
	{
		player = GameObject.Find("Police");												//Find the police GameObject
		playerHealth = player.GetComponent <PlayerHealth> (); 							//get the PlayerHealth Script 
		enemyHealth = GetComponent <EnemyHealth>();										//Get EnemyHealth Script
	
	}

	void Update ()
	{
		//keep the record of time
		timer += Time.deltaTime;
		
		//check if time is higher than the last attack time and playerInRage is true, and enemy is not dead
		if(timer >= AttackInterval && playerInRange && enemyHealth.currentHealth > 0)
		{
			Attack ();
		}
		
	}
#endregion

#region Utility Function
	//Is Enemy close to other object (Rigidbody), and check if the other GameObject is player
	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject == player)
		{
			playerInRange = true;
		}
	}
	
	//did other object left the collision zone? and the object is player, if yes set playerInRage to false
	void OnTriggerExit (Collider other)		
	{
		if(other.gameObject == player)
		{
			playerInRange = false;
		}
	}

	//reset the timer to 0.0f and called function playerHealth.TakeDame(10) from PlayerHealth Class
	void Attack ()
	{
		timer = 0f;
		
		if(playerHealth.currentHealth > 0)
		{
			playerHealth.TakeDamage (AttackDamage);
		}
		else if (playerHealth.Life > 3)				//Give a relife time for player
		{
			return;
		}
	}
}
#endregion
