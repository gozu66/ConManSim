/********** Monobehaiour for world manager *********
 ********** creation of component handlers *********
 *************** delegation of duties***************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static WorldManager _instance;                                       // singleton 

    private Grid pathfindingGrid;                                               // A* grid ref

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError("DUPLICATED SINGLETON - WORLD MANAGER <destroyed>");
            Destroy(gameObject);
        }

        TimeManager timeManager = new TimeManager();

        pathfindingGrid = FindObjectOfType<Grid>();

        TileManager tileManager = new TileManager(pathfindingGrid);

    }

    float timer;                                                        
    private void Update()
    {
        timer += Time.deltaTime;                                                // tick timer 
        if(timer >= 1 / TimeManager._instance.getTimeScale)                     // allow for time scale
        {
            timer = 0;
            TickTime();                                                         // tick time manager
        }        
    }

    private void TickTime()                                                     // tick time manager
    {
        TimeManager._instance.Tick();
    }    
}
