/* Item cleanable, sub class of item_interactable*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanableItem : Item
{
    public CleanableItem(GameObject cleanableGameobject) : base(cleanableGameobject)
    {
    }
}