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

    private Item_Cleanable itemToClean;                         // item_cleanable script ref
    public Item_Cleanable getItemToClean                         
    {
        get
        {
            return itemToClean;
        }
    }

    public CleaningJob(GameObject itemToCleanGameobject, Item_Cleanable itemToClean) : base(itemToCleanGameobject.transform.position)
    {
        this.itemToClean = itemToClean;
        this.itemToCleanGameobject = itemToCleanGameobject;
    }

}