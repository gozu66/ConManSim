/**********************************************************
 *  Monobehaviour for pawns
 *      -this class is the monobehaviour that governs 
 *       pawn behaviors
***************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PawnState                                                                               //enum used by pawns to represent state
{
    Idle,
    Work, 
    Rest
};

public enum PawnType
{
    Hauler,
    Scientist,
    Cleaner,
    Builder
};


public class GameobjectPawn : MonoBehaviour
{
    public bool debugInfo;                                                                          //[DEBUG]
 
    public PawnState pawnState;                                                                     //pawns current state

    public PawnType pawnType;                                                                       //pawn curren type

    private UtilityHandler utilityHandler;                                                     //script ref
    public UtilityHandler getUtilityHandler
    {
        get
        {
            return utilityHandler;
        }
    }

    private JobHandler jobHandler;                                                             //script ref
    public JobHandler getJobHandler
    {
        get
        {
            return jobHandler;
        }
    }

    private Vector3[] currentPath;                                                                  //container 4 current path                     
    public Vector3[] getSetCurrentPath
    {
        get
        {
            return currentPath;
        }

        set
        {
            currentPath = value;
        }
    }

    protected bool hasJob;                                                                          
    public bool getSetHasJob
    {
        get
        {
            return hasJob;
        }
        set
        {
            hasJob = value;
        }
    }

    public ParticleSystem ptl;

    private void Awake()                                            // on awake
    {                                                               
        utilityHandler = new UtilityHandler(this);             // create util handler
        switch(pawnType)                                            //swith on type
        {
            case PawnType.Hauler:
                jobHandler = new JobHandlerHauling(this);      //assign correct job handler
                break;

            case PawnType.Cleaner:
                jobHandler = new JobHandlerCleaning(this);     //
                break;

            case PawnType.Scientist:
                break;

            case PawnType.Builder:
                jobHandler = new JobHandlerConstruction(this); //
                break;
        }
    }

    private void Start()
    {
        this.pawnState = PawnState.Idle;
        ptl = GetComponentInChildren<ParticleSystem>();             // sleep particle system
    }

    float timePassed = 0;                                           // float timer ti count ticks
    private void Update()
    {
        timePassed += Time.deltaTime;
        if(timePassed >= 2 / TimeManager._instance.getTimeScale)
        {            
            timePassed = 0;
            utilityHandler.Tick();                                  // tick util
        }
    }

    public void moveAlongPath(Action callback)                      // called from job handlers to pass callback to coroutine
    {
        StartCoroutine("MoveAlongPath", callback);
    }

    int targetIndex = 0;                                            //Target Index global for [DEBUG] purposes only [TEMP]
    private IEnumerator MoveAlongPath(Action callback)
    {
        if (currentPath.Length <= 0)                                //if path goal is alredy occupied by pawn, finish path
        {
            if (callback != null) { callback(); }                       //if callback, call back
            yield break;
        }
        int speed = 5;                                              //[SQL]
        Vector3 currentWaypoint = currentPath[0];                   
        targetIndex = 0;
        while (true)
        {
            if (transform.position == currentWaypoint)              //if we reach next waypoint
            {
                targetIndex++;                                      //add to index
                if (targetIndex >= currentPath.Length)              //if we are at end of path nodes
                {
                    break;                                          //finish path   
                }
                currentWaypoint = currentPath[targetIndex];         //otherwise, set next waypoint
            }                                                       //move to next waypoint

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, (speed * Time.deltaTime) * TimeManager._instance.getTimeScale);      

            yield return null;                                      //yield next frame
        }
        if (callback != null) { callback(); }                       //if callback, call back
    }

#if UNITY_EDITOR
    public void OnDrawGizmos()                                                                                      //[DEBUG] shows pawns current path
    {
        if (currentPath != null && debugInfo)
        {
            for (int i = targetIndex; i < currentPath.Length; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(currentPath[i], Vector3.one * 0.25f);
                Gizmos.color = Color.green;


                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, currentPath[i]);
                }
                else
                {
                    Gizmos.DrawLine(currentPath[i - 1], currentPath[i]);
                }
            }
        }
    }
#endif
}