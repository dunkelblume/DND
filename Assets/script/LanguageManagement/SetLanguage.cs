using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetLanguage : MonoBehaviour {

    public string info;                 //stores the Language
    public Text txtBox;                 //sets the info in the textBox called Idioma(Language in spanish)

    public perennialInfo Script;             //Loads the script that contains the perennial Objec

    public void OnPress()
    {
        txtBox.text = info;             // puts the info in the textBox
        Script.info = info;             //passes the info to the perennial Object
        Application.LoadLevel("Menu");          // to go to Menu
    }

    
}
