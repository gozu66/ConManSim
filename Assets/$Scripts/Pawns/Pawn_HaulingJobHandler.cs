/**** handles hauling jobs, extends from job handle **********/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn_HaulingJobHandler : Pawn_JobHandler
{
    private Pawn owner;
    private HaulingJob currHaulingJob;
    private sbyte index = 0;

    public Pawn_HaulingJobHandler(Pawn owner) : base(owner)
    {
        this.owner = owner;
    }

    protected override void GetHaulingJob()
    {
        base.GetHaulingJob();
        if (Debugger._instance.haulingJobQueue.Count > 0)
        {
            owner.getSetHasJob = true;                                                                  //Set hasJob bool on owner pawn gameobject
            currHaulingJob = Debugger._instance.haulingJobQueue.Dequeue();                              //get hauling job            
            base.RequestPath(owner.transform.position, currHaulingJob.startLocation);
        }
    }

    protected override void OnPathFinished()                                                            //Called when pawn reaches end of current path
    {
        base.OnPathFinished();        
        if (index == 0)                                                                                 //if first time around
        {
            index++;
            currHaulingJob.getItemToHaul.PickUpPutDown(owner.transform);                                //pick up obj, continue hauling
            base.RequestPath(currHaulingJob.startLocation, currHaulingJob.getEndLoaction);
        }
        else if (index >= 1)                                                                            //if second time around
        {
            index = 0;
            currHaulingJob.getItemToHaul.PickUpPutDown(null);                                           //put down obj 
            base.FinishJob();                                                                           //finish job
        }
    }

    protected override void DiscardJob()
    {
        base.DiscardJob();
        currHaulingJob.getItemToHaul.PickUpPutDown(null);
        currHaulingJob = null;
        owner.getUtilityHandler.CalculateNextAction();                                                  //Utilities to calc next action
    }
}