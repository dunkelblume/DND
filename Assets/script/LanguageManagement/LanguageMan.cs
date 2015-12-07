using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LanguageMan : MonoBehaviour {
    public Image titleImg;                  //UI Image to set the Sprite Corresponding to the Title Translated
    public GameObject perennialobject;      //this will get the Perennial Info from last scene
    public perennialInfo Script;            //this helps to store the the Perennial Info from last scene
    public perennialInfo Info2pass;         //this one stores the the Perennial Info to pass it to the lvl1


    // --- Sprites with the game title for each language
    #region
    public Sprite Eng;
    public Sprite Spa;
    public Sprite Rus;
    public Sprite Fre;
    public Sprite Ger;
    public Sprite Ita;
    public Sprite Por;
    public Sprite Pol;
    #endregion
    // Use this for initialization
    void Awake() {
        perennialobject = GameObject.FindGameObjectsWithTag("perennialObject")[0] as GameObject;    // asigns the Perennial Info from last scene
        Script = perennialobject.GetComponent<perennialInfo>();                                     //stores the the Perennial Info from last scene
        Info2pass.info = Script.info;             //passes the info to the perennial Object
        switch (Script.info)                    // This switch calls the corresponding sprite to the language selected
        {
        }
    }
}
