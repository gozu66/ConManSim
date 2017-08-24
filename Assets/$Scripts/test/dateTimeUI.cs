using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dateTimeUI : MonoBehaviour {

    Text uiText;
    
	// Use this for initialization
	void Start () {
        uiText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        uiText.text = TimeManager._instance.dateTime;
	}
}
