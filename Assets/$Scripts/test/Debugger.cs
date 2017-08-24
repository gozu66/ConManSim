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
            HaulingJob newHaulingJob = new HaulingJob(haulingObjects[i], new Item_Haulable(haulingObjects[i]));
            haulingJobQueue.Enqueue(newHaulingJob);
        }
        for (int j = 0; j < cleaningObjects.Length; j++)
        {
            CleaningJob newCleaningJob = new CleaningJob(cleaningObjects[j], new Item_Cleanable(cleaningObjects[j]));
            cleaningJobQueue.Enqueue(newCleaningJob);
        }
        //ConstructionJob newCJob = new ConstructionJob(new Vector3(20, 0, 21), MaterialType.Brick, new Item_Material(construcatbleObjects[0], MaterialType.Brick), ConstrucatbleItem.Wall);
        //constructionJobQueue.Enqueue(newCJob);
        //ConstructionJob newCJob1 = new ConstructionJob(new Vector3(21, 0, 21), MaterialType.Brick, new Item_Material(construcatbleObjects[1], MaterialType.Brick), ConstrucatbleItem.Wall);
        //constructionJobQueue.Enqueue(newCJob1);
        //ConstructionJob newCJob2 = new ConstructionJob(new Vector3(22, 0, 21), MaterialType.Brick, new Item_Material(construcatbleObjects[2], MaterialType.Brick), ConstrucatbleItem.Wall);
        //constructionJobQueue.Enqueue(newCJob2);
        //ConstructionJob newCJob3 = new ConstructionJob(new Vector3(21, 0, 22), MaterialType.Brick, new Item_Material(construcatbleObjects[3], MaterialType.Brick), ConstrucatbleItem.Wall);
        //constructionJobQueue.Enqueue(newCJob3);
        //ConstructionJob newCJob4 = new ConstructionJob(new Vector3(22, 0, 22), MaterialType.Brick, new Item_Material(construcatbleObjects[4], MaterialType.Brick), ConstrucatbleItem.Wall);
        //constructionJobQueue.Enqueue(newCJob4);

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
