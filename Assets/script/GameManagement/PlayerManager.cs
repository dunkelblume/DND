using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// PlayerManager When Player is killed, This function will check if Player has any life left if it has
/// it will regenerate the player health and also detect if there is any zombie around and kill them
/// </summary>

public class PlayerManager : MonoBehaviour {
	#region Public Variables

	public Sprite [] LifeImage;								//To Change the Heart Image 
	public Canvas HUD;										//To disable the HUD bar

	#endregion

	#region Private Variables
	GameObject Player;
	PlayerHealth PoliceHealth;
	PlayerMovement playerMovement;
	PlayerShooting playerShooting;

	//find Alive Zombie
	GameObject[] ZombieFinder;	

	//Fade in and out effect for scence
	LevelMan LevelManager;

	Image HeartImage;
		Sprite SecondLife;
		Sprite ThirdLife;
		Sprite ForthLife;

	//To update health slider
	Slider HealthSlider;

	Animator PlayerAnimator;

	Image MissionOver;


	#endregion

	#region Main Function

	/// <summary>
	/// Get Player Detail like Player Health, Player Movement and Player Shoot to disable when
	/// Game is over
	/// </summary>

	void Awake()
	{

		Player = GameObject.Find("Police");													//Find Police GameObj
		PoliceHealth = Player.GetComponent<PlayerHealth>();									//Get PlayerHealth Script from Police
		playerMovement = Player.GetComponent<PlayerMovement>();								//Get PlayerMovement 
		playerShooting = Player.GetComponentInChildren <PlayerShooting>();					//Get the Playershooting from GunBarrel i.e child of player
		PlayerAnimator = Player.GetComponent<Animator>();									//Get the animator component from player

		LevelManager = GameObject.Find("LevelManager").GetComponent<LevelMan>();			//Get the LevelMan Script from LevelManager GameObj

		ZombieFinder = GameObject.FindGameObjectsWithTag("ZombieTag");						//Find all the Alive zombie 

		HealthSlider = HUD.GetComponentInChildren<Slider>();								//Get the Health Slider from HealthBar Canvas
			
		HeartImage = GameObject.Find ("HeartImage").GetComponent<Image>();					// Get the Image from HeartImage UI
				//Get the Sprite Image from LifeImage Array
				SecondLife = LifeImage[0];
				ThirdLife = LifeImage[1];
				ForthLife = LifeImage[2];

		MissionOver = GameObject.Find ("DayComplete").GetComponent<Image>();				//Get DayComplete Gameobj
	
		MissionOver.color = Color.clear;													//Set Image transparent

	}

	/// <summary>
	/// The Update function checks if it can find any zombie in the scene, if it did, it does nothing, if all the zombies are dead, it calls
	/// for MissionComplete function.
	/// </summary>
	void Update()
	{
		if (ZombieFinder.Length != 0 )								//check if Zombie array has length 0
		{	
			foreach(GameObject element in ZombieFinder )
			{
				if (element != null)								//if element if not empty do nothing
				{
					return;
				}
			}
			StartCoroutine(MissionComplete());			  //if found nothing in element then call for missionComplete
		
		}
	}
	#endregion
	

	#region Utiltity Functions

	/// <summary>
	/// The function manage the repawn of Player Health and also load game over screen
	/// </summary>
	/// <param name="PlayerLifeCount">Player life count.</param>

	public void PlayerLifeManager(int PlayerLifeCount)								//will Get the Player Life number from PlayerHealth Script. 
	{
																					//Send Heart Sprite According to the Player Life
		if (PlayerLifeCount == 0)
			{	
				HeartImage.sprite = SecondLife;										
				PlayerStatus();
				
			}
		else if (PlayerLifeCount == 1)
			{	
				HeartImage.sprite = ThirdLife;
				PlayerStatus();
			}
		else
			{	//When player finish his life
				HeartImage.sprite = ForthLife;										
				PoliceHealth.Death();												//Call for Player Death function 
				
				playerMovement.enabled = false;										//Disable PlayerMovement & Shooting Script
				playerShooting.enabled = false;

				StartCoroutine(GameOverSplash());									//To use IEnumerator we should use StartCoroutine 

			}

	}


	void PlayerStatus()
	{
		PoliceHealth.currentHealth = 100;										//Update the player Health and Healthslider value to 100;
		HealthSlider.value = PoliceHealth.currentHealth; 						 
	}

	/// <summary>
	/// GameOverSplash function displays the Player Death animation as well as call for gameOver scene
	/// </summary>
	IEnumerator GameOverSplash()												//this function is used for delaying the game
	{
		PlayerAnimator.SetTrigger("PlayerIsDead");
		yield return new WaitForSeconds (4f);									 //Wait 2 second for Player Death animation to finis
	
		float fadeTime = LevelManager.BeginFade(1);								// Get the BeginFade value from LevelMan Script

		yield return new WaitForSeconds (fadeTime);								//Wait till fade is complete
		Application.LoadLevel("GameOver"); 										//Load GameOver Scene

	}


	/// <summary>
	/// This function Displays the Missiover Text as well as loads the new scene according to current level
	/// </summary>
	IEnumerator MissionComplete()
	{
		MissionOver.color = Color.Lerp(Color.clear, Color.white, Time.time); 	//source.color = Color.Lerp (SourceColor.color, target.color, t)

		yield return new WaitForSeconds (5f);

		float fadeTime = LevelManager.BeginFade(2);								// Get the BeginFade value from LevelMan Script
		yield return new WaitForSeconds (fadeTime);								//Wait till fade is complete

		if (Application.loadedLevelName == "Scene1")
		{
			Application.LoadLevel(2); 											//Load Level 2
		}
		else if (Application.loadedLevelName == "Scene2")
		{
			Application.LoadLevel(3); 											//Load Game Over
		}
											
		
	}

#endregion

}
	
