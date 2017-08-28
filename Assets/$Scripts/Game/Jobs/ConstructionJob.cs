/****** Subclass of Job, handles construcion jobs *********/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConstrucatbleItem                                   //Global enum for strucure type [TEMP?]
{
    Wall
}

public class ConstructionJob : Job
{
    private Vector3 jobSite;                                    //location to build at
    public Vector3 getJobsite   
    {
        get
        {
            return jobSite;
        }
    }

    private MaterialType matType;

    private GameObject constructionMaterial;                    //Construction material gameobject
    public GameObject getConstructionMaterial
    {
        get
        {
            return constructionMaterial;
        }
    }

    private MaterialItem materialForJob;                       //item material class ref
    public MaterialItem getSetMaterialForJob
    {
        get
        {
            return materialForJob;
        }
    }

    private ConstrucatbleItem itemToConstruct;                  //Construcatable item class ref (ie wall)     
    public ConstrucatbleItem getItemTocOnstruct
    {
        get
        {
            return itemToConstruct;
        }
            
    }

    public ConstructionJob(Vector3 jobSite, MaterialType matType, MaterialItem mat, ConstrucatbleItem itemToConstruct) : base(jobSite)
    {
        this.jobSite = jobSite;                                     //[TEMP HARD CODED JOBSITE]
        this.matType = matType;
        materialForJob = mat;
        this.itemToConstruct = itemToConstruct;
    }
}