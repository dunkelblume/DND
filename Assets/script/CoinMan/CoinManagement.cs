using UnityEngine;
using System.Collections;
/// <summary>
/// CoinManagement scrip will be added to coin PreFab. It will find the PlayerHealth and see if coin collides with player
/// if it does then it adds the 20 point toward the player score (ScoreManager). 
/// </summary>
public class CoinManagement : MonoBehaviour {

	#region Private Variables
	PlayerHealth PoliceHealth;
	ScoreManager ScoreMan;
	AudioSource CoinSoundFX;

	int coinValue= 20;
	#endregion

	#region Main Function
	void Awake()
	{
		PoliceHealth = GameObject.Find("Police").GetComponent<PlayerHealth>();
		ScoreMan = GameObject.Find ("ScoreManager").GetComponent<ScoreManager>();
		CoinSoundFX = GetComponent<AudioSource>();
	}


	void OnTriggerEnter (Collider other)
	{

		if(other.gameObject.CompareTag ("Player") && PoliceHealth.currentHealth > 0 && PoliceHealth.Life <3 )
		{

			ScoreMan.score(coinValue);						//Send Value of 20 to score board
			CoinSoundFX.Play();								//Play Coin SoundFx

			StartCoroutine(DestroyCoin());					//Call DestroyCoin() function. 
										
		}
	}
	IEnumerator DestroyCoin()
	{
	yield return new WaitForSeconds (0.30f);			//Temp Fix, Wait 0.30s to let Coin SoundFx to finish playing 
	Destroy(gameObject);								//Destroy Coin
	}


	#endregion
}


	