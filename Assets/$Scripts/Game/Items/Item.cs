/************* Item Interactable parent class ***************
***** anything that can be acted upon extends from here ****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class Item                                                              //All items will inherit from here
{
    protected GameObject itemGameobject;                                                    //Gameobject reference
    public GameObject getItemGameobject
    {
        get
        {
            return itemGameobject;
        }
    }

    protected GameobjectItem itemInstance;                                    

    public Item(GameObject itemGameobject)
    {
        this.itemGameobject = itemGameobject;                                               //set Gamobject ref
        itemGameobject.GetComponent<GameobjectItem>().getItemInteractableInstance = this;             //set Gameobject's ref to this
    }
}