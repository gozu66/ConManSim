/****************************************************************************
 *** Monobehaviour that takes all input and send to appropriate class *******
 ****** string key dictionary allows for custom inputs @ runtime ************
 ***************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static InputHandler _instance;
    private Dictionary<string, KeyCode> inputs = new Dictionary<string, KeyCode>();                         

    private WorldManager world;
    private CameraBehaviour mainCamera;

    public bool zoomToCursor = true;
    
    private void Awake()
    {
        if(_instance == null) { _instance = this; }
        else { Destroy(this); }

        inputs.Add("Move_North",    KeyCode.W);
        inputs.Add("Move_South",    KeyCode.S);
        inputs.Add("Move_West",     KeyCode.A);
        inputs.Add("Move_East",     KeyCode.D);
        inputs.Add("Move_North_2",  KeyCode.UpArrow);
        inputs.Add("Move_South_2",  KeyCode.DownArrow);
        inputs.Add("Move_West_2",   KeyCode.LeftArrow);
        inputs.Add("Move_East_2",   KeyCode.RightArrow);

        inputs.Add("Zoom_In",    KeyCode.PageUp);
        inputs.Add("Zoom_Out",   KeyCode.PageDown);


        inputs.Add("SpeedUp_Time",  KeyCode.Period);
        inputs.Add("SlowDown_Time", KeyCode.Comma);
        inputs.Add("TimeScale_1",   KeyCode.Alpha1);
        inputs.Add("TimeScale_2",   KeyCode.Alpha2);
        inputs.Add("TimeScale_3",   KeyCode.Alpha3);


        mainCamera = Camera.main.GetComponent<CameraBehaviour>();
    }

    Vector3 origMousePosition;
    public bool dragging;

    private void Update()
    {
        /******************************************************** KEY inputs ********************************************************/
        
        if (Input.GetKey(inputs["Move_North"]) || Input.GetKey(inputs["Move_North_2"])) { mainCamera.TranslateCamera("Move_North"); }
        if (Input.GetKey(inputs["Move_South"]) || Input.GetKey(inputs["Move_South_2"])) { mainCamera.TranslateCamera("Move_South"); }
        if (Input.GetKey(inputs["Move_West"])  || Input.GetKey(inputs["Move_West_2"]))  { mainCamera.TranslateCamera("Move_West");  }
        if (Input.GetKey(inputs["Move_East"])  || Input.GetKey(inputs["Move_East_2"]))  { mainCamera.TranslateCamera("Move_East");  }

        if (Input.GetKey(inputs["Zoom_In"]))    { mainCamera.ZoomCamera(-25, false);   }           //[SQL]
        if (Input.GetKey(inputs["Zoom_Out"]))   { mainCamera.ZoomCamera(25, false);    }           //[SQL]

        if (Input.GetKeyDown(inputs["SpeedUp_Time"]))   { TimeManager._instance.AdjustTime( 1, false);  }
        if (Input.GetKeyDown(inputs["SlowDown_Time"]))  { TimeManager._instance.AdjustTime(-1, false);  }
        if (Input.GetKeyDown(inputs["TimeScale_1"]))    { TimeManager._instance.AdjustTime( 1, true);   }
        if (Input.GetKeyDown(inputs["TimeScale_2"]))    { TimeManager._instance.AdjustTime( 2, true);   }
        if (Input.GetKeyDown(inputs["TimeScale_3"]))    { TimeManager._instance.AdjustTime( 5, true);   }


        /******************************************************* MOUSE inputs ********************************************************/

        if (Input.GetMouseButtonUp(0))
        {
            //Call Mouse Click Stuff Here...
        }

        if (Input.GetMouseButton(1))
        {
            if (Input.GetMouseButtonDown(1))
            {
                dragging = true;
                origMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                origMousePosition.y = 0;
            }
            Vector3 difference = getMouseDelta();
            mainCamera.TranslateCamera(difference);
        }
        if (Input.mouseScrollDelta.y > 0) { mainCamera.ZoomCamera(-100, zoomToCursor); }
        else if (Input.mouseScrollDelta.y < 0) { mainCamera.ZoomCamera(100, zoomToCursor); }
    }

    Vector3 getMouseDelta()
    {
        Vector3 newOrigMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newOrigMousePosition.y = 0;
        Vector3 difference = origMousePosition - newOrigMousePosition;

        return difference;
    }

    public void BeginCoroutine(string methodName, Action callback)
    {
        if (callback != null) { StartCoroutine(methodName, callback); }
        else { StartCoroutine(methodName); }
    }

    IEnumerator GetInputTiles(Action callBack)
    {

        List<Tile> tilesToBuildOn = new List<Tile>();
        while(!Input.GetMouseButton(0))
        {
            yield return null;
        }
        while (Input.GetMouseButton(0))
        {
            Tile tile = TileManager._instance.getTileFromWorldPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (tilesToBuildOn.Contains(tile))
            {
                yield return null;
            }
            else
            {
                tilesToBuildOn.Add(tile);
                yield return null;
            }
        }

        GUIManager._instance.currenTileList = tilesToBuildOn.ToArray();
        callBack();

    }
}