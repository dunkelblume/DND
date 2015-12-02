using UnityEngine;
using System.Collections;
/// <summary>
/// The EnemyHealth Script manage the Zombie Health. It takes input from PlayerShooting for Zombie hurt.
/// In addition, it also plays SoundFx as well as particle Effects assigned to it. Various other functionality has also be implemented in script
/// like NavMesh Speed Random Range, which gives feeling of better AI Zombie. 
/// </summary>

public class EnemyHealth : MonoBehaviour {

	#region Public Variables

	public int InitialHealth;
	public int currentHealth;

	public int ScoreValue = 10; 											//Score Per Hurt
	
	#endregion

	#region Private Variables
	AudioSource[] EnemyFX;
	 AudioSource HurtClip;
	 AudioSource deathClip;
	
	Animator EnemyAnimator;

	ParticleSystem [] EnemyParticleSystem;
		ParticleSystem BloodParticleSystem;
		ParticleSystem DeadBloodParticleSystem;

	BoxCollider EnemyboxCollider;

	bool isKilled;

	ScoreManager scoreManager;

	CoinSpawn CoinCountScript;
		Vector3 ZombiePosition;				

	int NavMeshSpeed;															//Random Speed for NavMesh

	#endregion

	#region Main Function
	void Start()
	{
		//Health Status
		InitialHealth = 100;
		currentHealth = InitialHealth;

		EnemyFX = GetComponents<AudioSource>();										//Get all audioSource Component in Zombie
			HurtClip = EnemyFX[0];													//Assign first audioSource as Hurt
			deathClip = EnemyFX[1];													// Second as Dead

		EnemyAnimator = GetComponent<Animator>();

		EnemyParticleSystem = GetComponentsInChildren <ParticleSystem>();			//Get multi Particles System Component from child
			BloodParticleSystem = EnemyParticleSystem[0];
			DeadBloodParticleSystem = EnemyParticleSystem[1];

		EnemyboxCollider = GetComponent<BoxCollider> ();							

		scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
		CoinCountScript = GameObject.Find ("LevelManager").GetComponent<CoinSpawn>();


	}
	

	#endregion

	#region Utility Function

	/// <summary>
	/// this function is called from PlayerShooting script and 
	/// Displays Zombie green blood effect. Also it play zombie hurt sound.
	/// Plus, when zombie gets shots the NavMeshSpeed will be random to give a character AI feeling. 
	/// </summary>

	public void TakeDamage (int amount, Vector3 hitLocation)
	{
		if(isKilled)
			return; 															//if enemy is dead do nothing
										     								
		currentHealth -= amount;												//Decrease the currentHealth by amount send by playerShooting Script

		HurtClip.Play ();  														//play Enemy hurt sound
		BloodParticleSystem.transform.position = hitLocation; 					//change the position of Particles effect
		BloodParticleSystem.Play();												//and run the effect

		NavMeshSpeed = Random.Range(1, 5);
		GetComponent <NavMeshAgent> ().speed= NavMeshSpeed;						 //Once Zombie gets shot it will move fast / slow.. :)

		//if health is equal to 0 or less then call Death function	
		if(currentHealth <= 0)
		{  
			Death ();
		}
	}
	
	/// <summary>
	/// This function displays dead partical system when Zombie dies as well as calls, 
	/// CoinCountScript.CoinInstantiate function and sends Zombie's current postition,
	/// and scoreManager.score function and sends value to be added in score. 
	/// </summary>

	void Death ()
	{
		isKilled = true; 

		//run Death particles Clip
		DeadBloodParticleSystem.transform.position = transform.position;		//Run Dead Blood Particle System
		DeadBloodParticleSystem.Play();
		deathClip.Play ();														//Play Death Sound

		EnemyAnimator.SetTrigger ("Dead"); 										//run Dead animation										
		EnemyboxCollider.isTrigger = true;										//make BoxCollider as trigger so that player can pass through it.
	
		//Vect 3 for position
		ZombiePosition.Set(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y, GetComponent<Transform>().position.z);

		CoinCountScript.CoinInstantiate(ZombiePosition);					//Call CoinInstantiate and send ZombiePosition and CurrentHealth
	
		scoreManager.score(ScoreValue);										//Send Score Value to Score Manager
	}

	//This function is called with in  animation event system of Zombie dead animation. 
	public void StartSinking ()

	{
		GetComponent <NavMeshAgent> ().enabled = false; 				//get NavMeshAgent from enemy and 
																		//disable it as we no longer need this
		GetComponent <Rigidbody> ().isKinematic = true;					//Change Rigidbody to kinematic to stop 
																		//unity from recalculation static object in scene

		Destroy (gameObject, 2f); 										//destory the Zombie after 2seconds
	}
	
	#endregion

}
