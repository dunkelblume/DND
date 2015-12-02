using UnityEngine;
using System.Collections;

/// <summary>
/// LevelMan script deals with Smooth Transition between Scenes.  
/// It is been done by using Unity old GUI system GUI. And public function like BeginFade will be called from other function i.e PlayerManager
/// </summary>

public class LevelMan : MonoBehaviour {
		#region Public Function
		
		public Texture2D FadeOutTexture;
		public float fadeSpeed = 0.8f;
		
		#endregion
		
		#region Private Function
		int drawDepth = -1000;
		float alpha = 1.0f;
		int fadeDir = -1;																	//-1 give a fadeout effect
		
		#endregion
		
		#region Main Function
		
		void OnGUI ()
		{
			alpha += fadeDir * fadeSpeed * Time.deltaTime;
			alpha = Mathf.Clamp01(alpha);													//[2],[3]. Mathf.Clamp01 was need to clamp value of alpah to 0 or 1.
			
			GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);			//Keep RBG from Texture but change Alpha
			GUI.depth = drawDepth;															//Make the texture render on top (draw last)
			GUI.DrawTexture (new Rect (0,0, Screen.width, Screen.height),FadeOutTexture);	//Make Rectangle and fill the screen and apply, given texture
			
		}
		
		public float BeginFade (int direction)												//Take the input direction from PlayerManager Function
		{																					//And return the given fadeSpeed, will be used for WaitForSeconds
			fadeDir = direction;
			return (fadeSpeed);
		}

		#endregion


	#region utility 

	// LevelOne and QuitGame Function are used by Main Menu Button to load and quit the Game
	public void LevelOne(string LevelName) //[1.]
	{
		Application.LoadLevel(LevelName);
	}


	public void QuitGame()
	{
		
		Debug.Log("GAME OVER!");
		Application.Quit();
	}
}
	#endregion

#region Reference
//[1].http://answers.unity3d.com/questions/836635/can-ui-buttons-load-scenes.html
//[2]. https://www.youtube.com/watch?v=0HwZQt94uHQ
//[3]. http://docs.unity3d.com/ScriptReference/Mathf.Clamp01.html
#endregion