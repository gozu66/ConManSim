using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public static GUIManager _instance;

    Text uiText;

    void Update()
    {
        uiText.text = TimeManager._instance.dateTime;
    }


    private void Awake()
    {
        if(_instance == null) { _instance = this; }
        else { Destroy(this); }
        uiText = GetComponentInChildren<Text>();
    }

    public void PrimeItemBuilder(string s)
    {
        string[] stringArray = s.Split('_');
        string thing = stringArray[0];
        string material = stringArray[1];

        switch(thing)
        {
            case "Wall":
                InputHandler._instance.BeginCoroutine("GetInputTiles", RecieveInputTiles);
                break;

            case "Door":
                break;
        }
    }

    public Tile[] currenTileList;
    int meterialIndex = 0;
    private void RecieveInputTiles()
    {
        Debug.Log("Tiles selected to build on: " + currenTileList.Length);

        for (int i = 0; i < currenTileList.Length; i++)
        {
            if (Debugger._instance.construcatbleObjects.Length <= meterialIndex) { return; }
            else
            {
                //Debug.Log(currenTileList.Length);
                ConstructionJob newCJ = new ConstructionJob(currenTileList[i].gameObject.transform.position, MaterialType.Brick, new MaterialItem(Debugger._instance.construcatbleObjects[meterialIndex].gameObject, MaterialType.Brick), ConstrucatbleItem.Wall);
                Debugger._instance.constructionJobQueue.Enqueue(newCJ);
            }
            meterialIndex++;
        }
        currenTileList = null;
    }
}