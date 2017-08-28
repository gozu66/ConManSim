/***** Subclass of Job, handles cleaning jobs *******/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningJob : Job                                                              
{
    private GameObject itemToCleanGameobject;                   // gameobject ref
    public GameObject getItemToCleanGameobject                  // getter
    {
        get
        {
            return itemToCleanGameobject;
        }
    }

    private CleanableItem itemToClean;                         // item_cleanable script ref
    public CleanableItem getItemToClean                         
    {
        get
        {
            return itemToClean;
        }
    }

    public CleaningJob(GameObject itemToCleanGameobject, CleanableItem itemToClean) : base(itemToCleanGameobject.transform.position)
    {
        this.itemToClean = itemToClean;
        this.itemToCleanGameobject = itemToCleanGameobject;
    }

}