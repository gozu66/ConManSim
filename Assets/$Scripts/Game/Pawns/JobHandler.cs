/************************************************************************************
 *  Pawn Job Handling class
 *      -this class is instantiated with every pawn and is used 
 *       to handle the passing around and executing of jobs
*************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobHandler
{
    GameobjectPawn owner;

    public JobHandler(GameobjectPawn owner)
    {
        this.owner = owner;                                                                             //set owner pawn
    }

    public void LookForJob()                                                                            //check if suitable job available
    {
        switch (owner.pawnType)                                                                         //choose job type base on pawn type
        {
            case PawnType.Hauler:
                if (Debugger._instance.haulingJobQueue.Count > 0)                                       //[TEMP]
                {
                    owner.pawnState = PawnState.Work;
                    GetJob();
                }
                break;

            case PawnType.Cleaner:
                if (Debugger._instance.cleaningJobQueue.Count > 0)
                {
                    owner.pawnState = PawnState.Work;
                    GetJob();
                }
                break;

            case PawnType.Builder:
                if (Debugger._instance.cleaningJobQueue.Count > 0)
                {
                    owner.pawnState = PawnState.Work;
                    GetJob();
                }
                break;
        }
        owner.pawnState = PawnState.Idle;
    }

    private void GetJob()                                                                            //Retrieve a job
    {
        switch (owner.pawnType)                                                                      //choose job type base on pawn type
        {
            case PawnType.Hauler:
                GetHaulingJob();                
                break;

            case PawnType.Cleaner:
                GetCleaningJob();
                break;

            case PawnType.Builder:
                GetConstructionJob();
                break;
        }
    }

    protected virtual void RequestPath(Vector3 start, Vector3 end)                                      // called from subclasses
    {
        PathRequestManager.RequestPath(start, end, RecievePath);
    }

    private void RecievePath(Vector3[] path, bool successful)                                           //callback from PRM
    {
        if (successful)
        {
            owner.getSetCurrentPath = path;
            owner.moveAlongPath(OnPathFinished);                                                        //move along path
        }
        else
        {
            Debug.LogError("PATH NOT FOUND");                                                           //no path
            DiscardJob();                                                                               //[TEMP] discard job
        }
    }

    protected void FinishJob()                                                                            
    {
        owner.getSetHasJob = false;                                                                     //nullify owner pawn current job
        owner.getUtilityHandler.CalculateNextAction();                                                  //Utilities to calc next action
    }

    protected virtual void DiscardJob()                                                                 //Edge case where job is unreachable
    {
        Debug.LogWarning("DISCARDING JOB");
        owner.getSetHasJob = false;                                                                     //nullify owner pawn current job
    }

    protected virtual void OnPathFinished() { }                                                         // Virtual methods to be overriden

    protected virtual void GetHaulingJob() { }                                                          //

    protected virtual void GetCleaningJob() { }                                                         //

    protected virtual void GetConstructionJob() { }                                                     //
}