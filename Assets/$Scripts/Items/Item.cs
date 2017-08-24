/* Monobehaviour class for Items*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{    
    public Item_Interactable itemInteractableInstance;                  //private ref w/getter to ITem_Interactable parent class
    public Item_Interactable getItemInteractableInstance
    {
        get
        {
            return itemInteractableInstance;
        }
        set
        {
            itemInteractableInstance = value;
        }
    }
}