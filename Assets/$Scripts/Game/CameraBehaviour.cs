/*******************************************************************
 *********  Monobehaviour for handling camera movement *************
 *********  and behaviors, called from InputHandler    *************
 *******************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    Camera thisCamera;
    private sbyte speed = 10;                                                                   //[SQL
    int worldedge;
    
    private void Start()
    {
        thisCamera = GetComponent<Camera>();
        worldedge = (int)TileManager._instance.getTiles.Length;
    }

    public void TranslateCamera(string key)                                                     //method for camera movement
    {                                                                                           //with string paramaters (key press)
        float zoomLevelMultiplier = thisCamera.orthographicSize / 4;
        switch(key)
        {
            case "Move_North":                
                transform.Translate(0, speed * Time.deltaTime * zoomLevelMultiplier, 0);
                break;

            case "Move_South":
                transform.Translate(0, -(speed * Time.deltaTime * zoomLevelMultiplier), 0);
                break;

            case "Move_West":
                transform.Translate(-(speed * Time.deltaTime * zoomLevelMultiplier), 0, 0);
                break;

            case "Move_East":
                transform.Translate(speed * Time.deltaTime * zoomLevelMultiplier, 0, 0);
                break;
        }
    }

    public void TranslateCamera (Vector3 direction)                                                     //method for camera movement
    {                                                                                                   //with vector parameters (mouse movement)
        int _speed = 3;
        transform.Translate( _speed * direction.x * Time.deltaTime, _speed * direction.z * Time.deltaTime, 0.0f);
    }

    public void ZoomCamera(sbyte delta, bool zoomToCursor)                                              //method to zoom in/out | camera orthographic size
    {                                                                                           
        thisCamera.orthographicSize += delta * Time.deltaTime;
        thisCamera.orthographicSize = Mathf.Clamp(thisCamera.orthographicSize, 2, 25);
        if (zoomToCursor && delta < 0 && thisCamera.orthographicSize > 2)
        {
            Vector3 mousePos = thisCamera.ScreenToWorldPoint(Input.mousePosition);                      //move camera towards mouse pos wile zooming in
            Vector3 zoomMoveDirection = mousePos - this.gameObject.transform.position;
            zoomMoveDirection.y = 0;

            TranslateCamera(zoomMoveDirection * 5);
        }
    }
}
