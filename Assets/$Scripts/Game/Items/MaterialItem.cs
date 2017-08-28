using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MaterialType                                    //Global enum for mat types
{
    Wood,
    Brick,
    Metal
};

/*** Subclass of item interactable ******
** handles materials for construction **/

public class MaterialItem : HaulableItem
{
    public MaterialType mat;
    public MaterialItem(GameObject materialGameobject, MaterialType mat) : base(materialGameobject)
    {
        this.mat = mat;
    }

    new private void PickUpPutDown(Transform newParent)                     //hides parent method (as it must remain public)
    {
        base.PickUpPutDown(newParent);                                      //call parent method
    }
}