using UnityEngine;
using System.Collections;

/// <summary>
/// GameOverManager script is mainly written for Game Over Scene and the script is use by UI button
/// to restart or quit the game
/// </summary>
public class GameOverManager : MonoBehaviour {


	#region Utility Function

	public void Level(string LevelName) 						//Get the input from Restart Button, i.e Menu
	{
		Application.LoadLevel(LevelName);					
	}
	
	
	public void QuitGame()			
	{	
		
		//Debug.Log("GAME OVER!");
		Application.Quit();
	}
	#endregion
}
