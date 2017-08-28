/***** Handler for contsruction jobs ************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobHandlerConstruction : JobHandler
{
    private GameobjectPawn owner;

    private ConstructionJob currConstJob;

    public JobHandlerConstruction(GameobjectPawn owner) : base(owner)
    {
        this.owner = owner;
    }

    protected override void GetConstructionJob()                                //Looks for available const job and dequeus
    {
        base.GetConstructionJob();
        if (Debugger._instance.constructionJobQueue.Count > 0)
        {
            owner.getSetHasJob = true;
            currConstJob = Debugger._instance.constructionJobQueue.Dequeue();
            base.RequestPath(owner.transform.position, currConstJob.getSetMaterialForJob.getItemGameobject.transform.position);            
        }                                                                       //get path to material for job
    }

    int index = 0;
    protected override void OnPathFinished()
    {
        base.OnPathFinished();
        if(index == 0)                                                              //if at material
        {
            currConstJob.getSetMaterialForJob.PickUpPutDown(owner.transform);       //pick up
            base.RequestPath(owner.transform.position, currConstJob.getJobsite);    //get path to job site
            index++;
        }
        else if (index == 1)                                                        //if at job site
        {
            index = 0;
            Construct(currConstJob.getItemTocOnstruct);                                             //construct thing
            currConstJob.getSetMaterialForJob.PickUpPutDown(null);                                  //drop material            
            UnityEngine.GameObject.Destroy(currConstJob.getSetMaterialForJob.getItemGameobject);    //destroy material
            currConstJob = null;                                                    
            FinishJob();                                                                            //finish
        }
    }

    private void Construct(ConstrucatbleItem thing)                                 //Construct an item
    {
        if(thing == ConstrucatbleItem.Wall)
        {
            GameObject newWallgo = new GameObject();                                //create Gameobject                    
            newWallgo.AddComponent<SpriteRenderer>();                               //add sprite renderer
            newWallgo.transform.Rotate(-90, 0, 0);                                  //Rotate upwards
            StructuralWall newWall = newWallgo.AddComponent<StructuralWall>();    //add wal script
            newWall.SetWallMaterial(currConstJob.getSetMaterialForJob.mat);         //call set materiual method on wall
            newWallgo.transform.position = TileManager._instance.getTileFromWorldPoint(currConstJob.getJobsite).gameObject.transform.position;
                                                                                    //snap to position of tile it sits on
        }
    }
}
