/******** Job Parent class*******************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Job
{
    public Vector3 startLocation;                                                           //All jobs have start location
    Job job;

    public Job (Vector3 _sLocation)     
    {
        startLocation = _sLocation;
    }
}