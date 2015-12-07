using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetLanguage : MonoBehaviour {

    public Image TranslationImg;                  //UI Image to set the Sprite Corresponding to the Title Translated

    #region Tranlation SpritesTranslation
    public Sprite TranslationEng;
    public Sprite TranslationSpa;
    public Sprite TranslationRus;
    public Sprite TranslationFre;
    public Sprite TranslationGer;
    public Sprite TranslationIta;
    public Sprite TranslationPor;
    public Sprite TranslationPol;
    #endregion

    void Start()
    {
        switch (PlayerPrefs.GetString("Country", "ENG")                    // This switch calls the corresponding sprite to the language selected
        {
            case "ENG":
                TranslationImg.sprite = TranslationEng;
                break;
            case "SPA":
                TranslationImg.sprite = TranslationSpa;
                break;
            case "RUS":
                TranslationImg.sprite = TranslationRus;
                break;
            case "FRE":
                TranslationImg.sprite = TranslationFre;
                break;
            case "GER":
                TranslationImg.sprite = TranslationGer;
                break;
            case "POL":
                TranslationImg.sprite = TranslationPol;
                break;
            case "ITA":
                TranslationImg.sprite = TranslationIta;
                break;
            case "POR":
                TranslationImg.sprite = TranslationPor;
                break;
        }
    }
}
