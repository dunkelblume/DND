using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetLanguage : MonoBehaviour {

    public Text txtBox;                     //UI Image to set the Perennial Info from last scene
    public Image titleImg;                  //UI Image to set the Sprite Corresponding to the Title Translated
    public GameObject perennialobject;      //this will get the Perennial Info from last scene
    public perennialInfo Script; //this helps to store the the Perennial Info from last scene

    // --- Sprites with the game title for each language
#region
    public Sprite TitleEng;
    public Sprite TitleSpa;
    public Sprite TitleRus;
    public Sprite TitleFre;
    public Sprite TitleGer;
    public Sprite TitleIta;
    public Sprite TitlePor;
    public Sprite TitlePol;
#endregion

    void Awake()
    {
        perennialobject = GameObject.FindGameObjectsWithTag("perennialObject")[0] as GameObject;    // asigns the Perennial Info from last scene
        Script = perennialobject.GetComponent<perennialInfo>();                                     //stores the the Perennial Info from last scene
        txtBox.text = Script.info;                                                                  //this is a debug thing I used to check that the Perennial info was passed properly
        switch (Script.info)                    // This switch calls the corresponding sprite to the language selected
        { 
            case "English":
                titleImg.sprite = TitleEng;
                //txtBox.text = "Die Next Day";
            break;
            case "Spanish":
                titleImg.sprite = TitleSpa;
                break;
            case "Russian":
                titleImg.sprite = TitleRus;
                break;
            case "French":
                titleImg.sprite = TitleFre;
                //txtBox.text = "Survivez Un Autre Jour";
                break;
            case "German":
                titleImg.sprite = TitleGer;
                //txtBox.text = "Überlieben Bis Morgen";
                break;
            case "Polish":
                titleImg.sprite = TitlePol;
                break;
            case "Italian":
                titleImg.sprite = TitleIta;
                break;
            case "Portuguese":
                titleImg.sprite = TitlePor;
                break;
        }
    }
}
