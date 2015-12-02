
using UnityEngine;
using UnityEngine.UI;													//to use new UI system
using System.Collections;

/// <summary>
/// The PlayerHealth script manage the PlayerHealth, Player AudioFX, Hurt FX, and also takes input from EnemyAttack and PlayerManager Class
/// </summary>

public class PlayerHealth : MonoBehaviour {

	#region Public Variable //[1]

	public int InitialHealth = 100;
	public int currentHealth;

	public float flashSpeed = 0.5f;
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

	public int Life = 0;


	#endregion

	#region Private Variable
	PlayerManager PlayerManager;
	PlayerShooting PlayerShooting;

	RawImage PlayerHurtImage;

	AudioSource [] PlayerSoundFX;
		AudioSource HurtClip;
		AudioSource DeadClip;

	Slider HealthSlider;


	//Player Statues 
		bool damaged;

	//Death Particle Animation for player
	ParticleSystem DeadBloodParticleSystem;

	#endregion

	#region Main Function
	void Awake () {

		currentHealth = InitialHealth;

		PlayerShooting = GetComponentInChildren <PlayerShooting> ();

		HealthSlider = GameObject.Find("HealthBar").GetComponent<Slider>();

		PlayerSoundFX = GetComponents<AudioSource> ();									//GetComponents gives multi value
			HurtClip = PlayerSoundFX[0];
			DeadClip = PlayerSoundFX[1];

		PlayerHurtImage = GameObject.Find ("PlayerHurtImage").GetComponent<RawImage>();

		DeadBloodParticleSystem = GetComponentInChildren<ParticleSystem>();

		PlayerManager = GameObject.Find ("PlayerMan").GetComponent<PlayerManager>();


	}
	/// <summary>
	/// Update function checks the damaged boolen, and if its true, it displays a dark red color and if its false, it change 
	/// PlayerHurtImage's color to have a fade effect by the help of Color.lerp (original color, target color, speed)
	/// </summary>

	void Update ()
	{

		if(damaged) 																	//if TakeDamage is called it will change damaged to true
		{
			PlayerHurtImage.color = flashColour;										//flash the damage image
		}
		else
		{																				//smoother transition from damage color to transparent 
			PlayerHurtImage.color = Color.Lerp (PlayerHurtImage.color, Color.clear, flashSpeed * Time.deltaTime); 
		}

		damaged = false; 																 //set damaged to false

	}

	#endregion

	#region Utilty Function
	/// <summary>
	/// TakeDamage function takes in Damage Amount from Enemy Attack script and reduce current player health by the damage amount
	/// also change the value of HealthSilder value to current health value. In addition, it plays Player Hurt Clip send PlayerLifeManger
	/// from PlayerManager with current life value. 
	/// <param name="DamageAmount">Damage amount.</param>
	public void TakeDamage (int DamageAmount) 											//public need -- function is called from other script
	{
		damaged = true;
		
		currentHealth -= DamageAmount;													//minus the currentHealth with amount you recieve from other function
		HealthSlider.value = currentHealth; 											//set the slider value to currentHealth Value
		HurtClip.Play ();
																						//play the sound of hurt	
				 											
		if(currentHealth <= 0 && Life <3)  												 //check if currentHealth is 0 or less then call the function Death 
		{

			PlayerManager.PlayerLifeManager(Life);
			Life++;
		}
	

	}	
	
	/// <summary>
	/// In Death function, which is called by PlayerManager script,disable gun light, and change the deadBloodParticleSystem 
	/// to the position where player is and plays it. 
	/// </summary>
	public void Death ()
	{
		PlayerShooting.DisableEffects ();											//Disable Gun Fire Light Effect
			DeadBloodParticleSystem.transform.position = transform.position;		//Run Dead Blood Particle System
			DeadBloodParticleSystem.Play();
	
																					
		DeadClip.Play();															//play - the DeadFX sound	

	}
	

	#endregion
	 

}


//[1].http://www.codeproject.com/Tips/438830/Make-your-code-Speak-by-using-Csharp-Region