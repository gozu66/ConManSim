﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn_CleaningJobHandler : Pawn_JobHandler
{
    Pawn owner;
    private CleaningJob currCleaningJob;

    public Pawn_CleaningJobHandler(Pawn owner) : base(owner)
    {
        this.owner = owner;
    }

    protected override void GetCleaningJob()
    {
        base.GetCleaningJob();

        if (Debugger._instance.cleaningJobQueue.Count > 0)
        {
            currCleaningJob = Debugger._instance.cleaningJobQueue.Dequeue();
            owner.getSetHasJob = true;
            base.RequestPath(owner.transform.position, currCleaningJob.startLocation);
        }
    }

    protected override void OnPathFinished()
    {
        base.OnPathFinished();
        //CLEAN ITEM WITH DELAY
        Object.Destroy(currCleaningJob.getItemToCleanGameobject);
        FinishJob();
    }

    protected override void DiscardJob()
    {
        base.DiscardJob();
    }
}
