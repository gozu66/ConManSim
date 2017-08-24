using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_Manager : MonoBehaviour
{
    public static GUI_Manager _instance;

    private void Awake()
    {
        if(_instance == null) { _instance = this; }
        else { Destroy(this); }
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
        Debug.Log(currenTileList.Length);

        for (int i = 0; i < currenTileList.Length; i++)
        {
            if (Debugger._instance.construcatbleObjects.Length <= meterialIndex) { return; }
            else
            {
                //Debug.Log(currenTileList.Length);
                ConstructionJob newCJ = new ConstructionJob(currenTileList[i].gameObject.transform.position, MaterialType.Brick, new Item_Material(Debugger._instance.construcatbleObjects[meterialIndex].gameObject, MaterialType.Brick), ConstrucatbleItem.Wall);
                Debugger._instance.constructionJobQueue.Enqueue(newCJ);
            }
            meterialIndex++;
        }
        currenTileList = null;
    }
}