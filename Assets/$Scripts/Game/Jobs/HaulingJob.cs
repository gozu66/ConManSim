/****** Subclass of job, handles hauling jobs ****************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaulingJob : Job                                                               
{
    private Vector3 endLoaction;                                                            //Hauling jobs also have a destination.
    public Vector3 getEndLoaction
    {
        get
        {
            return endLoaction;
        }
    }
    private HaulableItem itemToHaul;                                                       // class ref
    public HaulableItem getItemToHaul
    {
        get
        {
            return itemToHaul;
        }
    }

    private GameObject itemToHaulGamobject;                                                 // gameobject ref

    public HaulingJob(GameObject itemToHaulGamobject, HaulableItem itemToHaul) : base(itemToHaulGamobject.transform.position)
    {
        this.itemToHaulGamobject = itemToHaulGamobject;
        this.itemToHaul = itemToHaul;
        CalculateHaulingData();
    }

    protected void CalculateHaulingData()                                                   //method to trigger item to pick haul location
    {
        endLoaction = itemToHaul.getBestLocation;
    }
}



