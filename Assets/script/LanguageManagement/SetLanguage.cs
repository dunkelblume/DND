using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetLanguage : MonoBehaviour {

    public string language;                 //stores the Language
    public Text txtBox;                 //sets the info in the textBox called Idioma(Language in spanish)

    public void OnPress()
    {
        PlayerPrefs.SetString("Country",language);
        txtBox.text = language;             // puts the info in the textBox
        Application.LoadLevel("Menu");          // to go to Menu
    }

    
}
