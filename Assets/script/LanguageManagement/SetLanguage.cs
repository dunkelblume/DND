using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetLanguage : MonoBehaviour {

    public string language;                 //stores the Language

    public void OnPress()
    {
        PlayerPrefs.SetString("Country",language);
        Application.LoadLevel("Menu");          // to go to Menu
    }

    
}
