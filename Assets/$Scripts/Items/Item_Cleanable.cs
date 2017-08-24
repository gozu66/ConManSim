/* Item cleanable, sub class of item_interactable*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Cleanable : Item_Interactable
{
    public Item_Cleanable(GameObject cleanableGameobject) : base(cleanableGameobject)
    {
    }
}