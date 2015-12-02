using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// ScoreManager Script manage the score of player and It does it though out the various Game level. 
/// To achieve that, a PlayerPrefs is use to store value in prefs between the stages. 
/// It also take in ScoreValue from EnemyHealth. 
/// </summary>
public class ScoreManager : MonoBehaviour {

	#region Private Variables
	
	Text ScoreLable;
	int TotalScore;

	#endregion

	#region Main Function

	void Start()
	{
		ScoreLable = GameObject.Find ("ScoreLable").GetComponent<Text>();		//get the Text component from ScoreLable gameobj
		DontDestroyOnLoad(gameObject);											//Donot Destroy this music while loading the new scene

		if (Application.loadedLevelName == "Scene2")							//If new scene is Scene 2, 
		{			
			TotalScore= PlayerPrefs.GetInt("ScoreLable");						//Get the stored score value from stored prefab 
			ScoreLable.text=(""+TotalScore);									//Display it.
		}
		else if(Application.loadedLevelName == "GameOver")
		{
			TotalScore= PlayerPrefs.GetInt("ScoreLable");						//Get the stored score value from stored prefab 
			ScoreLable.text=("Total Score: "+TotalScore);	
		}
	}

	//This function is called from EnemyHealth & Gun
	public void score(int ScoreValue)
	{

		TotalScore +=ScoreValue;													//Add the given ScoreValue
		ScoreLable.text=(""+TotalScore);											//Display the Score	
		PlayerPrefs.SetInt("ScoreLable", TotalScore);								//[1] Store the totalScore in ScoreLable prefab. 
																					//Was need to take value to next level
	}			
	
}
#endregion

#region reference
//[1.]http://docs.unity3d.com/ScriptReference/PlayerPrefs.GetInt.html
#endregion

