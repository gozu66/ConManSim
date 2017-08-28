/******** Item haulable subclass of item interactable ********/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaulableItem : Item                                              //All haulable items will inherit from here
{
    private Vector3 bestLocation;                                                           //Best Location to haul this object to
    public Vector3 getBestLocation
    {
        get
        {
            return bestLocation;
        }
    }

    public HaulableItem(GameObject haulableGameobject) : base(haulableGameobject)
    {
        bestLocation = GetBestLocation();
    }

    public Vector3 GetBestLocation()                                                        //Find best location, rnd for now [TEMP]
    {
        float x = Random.Range(-10, 10);
        float y = 0.0f;
        float z = Random.Range(-10, 10);
        return new Vector3(x, y, z);
    }

    public void PickUpPutDown(Transform newParent)                                          //Parent this to the pawn_gameobject (pick up)
    {                                                                                       //or parent to null (put down)
        getItemGameobject.transform.parent = newParent;
    }
}