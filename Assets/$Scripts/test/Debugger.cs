using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour {

    public static Debugger _instance;

    public Queue<Job> jobQueue = new Queue<Job>();

    public Queue<HaulingJob> haulingJobQueue = new Queue<HaulingJob>();
    public Queue<CleaningJob> cleaningJobQueue = new Queue<CleaningJob>();
    public Queue<ConstructionJob> constructionJobQueue = new Queue<ConstructionJob>();

    public GameObject[] haulingObjects;
    public GameObject[] cleaningObjects;
    public GameObject[] construcatbleObjects;

    public bool DebugFPS;

    private void Start()
    {
        if (_instance == null) { _instance = this; }
        else { Destroy(this); }

        for (int i = 0; i < haulingObjects.Length; i++)
        {
            HaulingJob newHaulingJob = new HaulingJob(haulingObjects[i], new HaulableItem(haulingObjects[i]));
            haulingJobQueue.Enqueue(newHaulingJob);
        }

        for (int j = 0; j < cleaningObjects.Length; j++)
        {
            CleaningJob newCleaningJob = new CleaningJob(cleaningObjects[j], new CleanableItem(cleaningObjects[j]));
            cleaningJobQueue.Enqueue(newCleaningJob);
        }
    }

    int i = 0;
    private void Update()
    {
        if (DebugFPS)
        {
            i++;
            if (i % 100 == 0)
            {
                Debug.Log(1 / Time.deltaTime);
            }
        }


        //if (Input.GetKeyDown(KeyCode.H))
        //{
            //Debug.Log(TileManager._instance.getTiles.Length);

            //Tile t = TileManager._instance.getTileFromWorldPoint(transform.position);
            //Debug.Log(t.getSetCoordX + " x ......... " + t.getSetCoordY + " y");
        //}   
    }
}
