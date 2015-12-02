using UnityEngine;
using UnityEngine.UI;	
using System.Collections;
/// <summary>
/// PlayerShooting Script deals with Player Shooting function. It's primary function is to shoot Zombie.
/// It use Raycast to find Zombie in the scene.While doing so, it displays also light/on/off ie.gun effect, 
/// Plays, Gun Fire and Gun Reload SoundFx.
/// In addition, it sends EnemyHealth Damage Score,
/// </summary>


public class PlayerShooting : MonoBehaviour {


	#region Public Variables

	public float range = 100f;
	public int damagePerShot = 10;
	public float timeBetweenBullets = 0.15f;
	
	#endregion

	#region Private Variables
	PlayerHealth playerHealth;

	//Gun FireFx
	float timer;
	float effectsDisplayTime = 0.2f;

	Ray shootRay;
		RaycastHit shootHit;

	int shootableMask;

	Light gunLight;

	AudioSource [] GunFX; 								
		AudioSource GunFireFX;
		AudioSource GunReloadFx;

	Slider GunReloadSlider;
		int FullAmmo = 0;

	#endregion

	#region Main Function
	 void Awake()
		{

			GunFX = GetComponents<AudioSource> ();											//Get the audiosource from component
				GunReloadFx = GunFX[0];														
				GunFireFX = GunFX[1];

			gunLight = GetComponent<Light> ();

			shootableMask = LayerMask.GetMask ("shootable"); 								// Level Design as well as ememy

			GunReloadSlider = GameObject.Find("GunReload").GetComponent<Slider>();			//Find Gun Reload Slider
			
			playerHealth = GameObject.Find("Police").GetComponent<PlayerHealth>();

		}

	 void Update ()
		{

			timer += Time.deltaTime;
			
			if(Input.GetButtonDown ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0  && playerHealth.Life <3)
			{

				if (FullAmmo == 0) 							// [3]. [Future Update] - Fire2 to reload the Gun
				{ 	
					GunReloadFx.Play();
					FullAmmo = 7;
					GunReloadSlider.value=FullAmmo;			// Play the reload sound
				}
				else 
				{	
					Shoot ();
					FullAmmo--;
					GunReloadSlider.value=FullAmmo;			
				}
			}


			if(timer >= timeBetweenBullets * effectsDisplayTime)			//Disable effect after 0.2 second
			{
				DisableEffects ();
			}
		}
		 

	#endregion
	
	#region Utility Function

	 void Shoot ()
		{
			timer = 0f;

			GunFireFX.Play ();
			gunLight.enabled = true;

			shootRay.origin = transform.position;
			shootRay.direction = transform.forward; 									 //transform.forward directs to Z
		
			if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))			 // [1]. Shoot the ray from gunBarrel, to all the object thats in ShootableMask Layer in scene
			{
				EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();  //get the enemyHealth component from wherever raycast touches

				if(enemyHealth != null)													   	 //check if the object is enemy or not . 
																						   	//As we are shooting all the object in scene, we need to check if 
				{																			//we have enemyHealth component or not, otherwise, next command will give error
					//[2]. --[Future Project: Add force to enemy] 
					enemyHealth.TakeDamage (damagePerShot, shootHit.point);				    //call for TakeDamage function (10, vector3 value); 
				}
			}

			
		}

		public void DisableEffects ()												//Disable Gun light
		{
			gunLight.enabled = false;
		}


}
	#endregion

#region Reference 

//[1]. http://docs.unity3d.com/ScriptReference/Physics.Raycast.html

		/**[2]. Future Project
		Rigidbody EnemyBody = shootHit.collider.GetComponent<Rigidbody>();
		EnemyBody.AddForceAtPosition(shootRay.direction*1000,shootHit.point);

		[3]. if(Input.GetButtonDown ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0 && FullAmmo > 0 )
		{Shoot());
		elseif(Input.GetButtonDown ("Fire2") && timer >= timeBetweenBullets && Time.timeScale != 0 && FullAmmo == 0 )
		FullAmmo=7;

 		**/

#endregion
