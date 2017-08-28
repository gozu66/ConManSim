/* Monobehaviour class for Items*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameobjectItem : MonoBehaviour
{    
    public Item itemInteractableInstance;                  //private ref w/getter to ITem_Interactable parent class
    public Item getItemInteractableInstance
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