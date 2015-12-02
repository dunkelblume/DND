using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

/// <summary>
/// Overall Controller of Sound in Game as well as keep the background music alive though out the scene
/// </summary>

public class AudioStatus : MonoBehaviour {
	#region Public Variables
	//public AudioMixer MasterAudioMixer;							//Get the Master Audio Mixer [Cancelled]
	#endregion	

	#region Private Variables										  
	AudioSource [] BackgroudMusic;
		AudioSource MainMenuBGSoundFx;
		AudioSource Level1SoundFX;
		AudioSource Level2SoundFX;

	//bool isVolumeOn; 												-- [future update]
	#endregion

	#region Main Function

	void Start(){
		BackgroudMusic = GetComponents<AudioSource>();				//Get the Background Musics from gameObj;
			MainMenuBGSoundFx = BackgroudMusic[0];
			Level1SoundFX = BackgroudMusic [1];
			Level2SoundFX = BackgroudMusic [2];
			
			Level1SoundFX.enabled=false;
			Level2SoundFX.enabled=false; 

			DontDestroyOnLoad(gameObject);							//Donot Destroy this music while loading the new scene

	}

	/// <summary>
	/// If the Level return Menu aka main menu then do nothing as audio will be playing at the beginning of the scene.
	/// whereas if its in other scene then change audio clip accourdingly. 
	/// </summary>
	void Update(){
	
		if (Application.loadedLevelName == "Menu")
		{
			return;
		}

		else if (Application.loadedLevelName == "Scene1")
		{
			MainMenuBGSoundFx.enabled= false;
			Level1SoundFX.enabled = true;							//Enable Level1 Background Music
			Level2SoundFX.enabled=false;

		}
		else if (Application.loadedLevelName == "Scene2")
		{
			MainMenuBGSoundFx.enabled= false;
			Level1SoundFX.enabled = false;
			Level2SoundFX.enabled=true; 							//Enable Level2 Background Music
		}
	
	
							 
	}

	#endregion

	#region Utiltiy Function

//	[Cancelled due to no time --  future project]
//	public void MasterVolume(bool isVolumeOn)
//	{
//		if(isVolumeOn)
//		{
//			MasterAudioMixer.SetFloat("Master",100); // This is needed for Mute UI button
//		}					
//		else
//		{
//			MasterAudioMixer.SetFloat("Master",0);
//		}
//	}
//	
	#endregion

}





#region reference
//[1]. https://unity3d.com/learn/tutorials/modules/beginner/5-pre-order-beta/exposed-audiomixer-parameters
//[2]. http://answers.unity3d.com/questions/11314/audio-or-music-to-continue-playing-between-scene-c.html
#endregion




