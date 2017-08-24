using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    public Transform target;
    public float speed = 1;
    public bool Debug;

    Vector3[] path;
    int targetIndex;

    void Start()
    {
        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("target").transform;
        }
        //path = null;
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    public void otherStart()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("target").transform;
        }
        
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if(pathSuccessful)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];

        while(true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if(targetIndex >= path.Length)
                {
                    //Destroy(gameObject);
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }

#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        if(path != null && Debug)
        {
            for(int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);
                Gizmos.color = Color.green;

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
#endif
}