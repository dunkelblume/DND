using UnityEngine;
using System.Collections;

public class perennialInfo : MonoBehaviour {
    public string info;                     //stores the perennial Info of selected Language


    void Awake () {
        DontDestroyOnLoad(this);            //this line makes the object on where this script goes survive the load of the Menu scene
	}
}
