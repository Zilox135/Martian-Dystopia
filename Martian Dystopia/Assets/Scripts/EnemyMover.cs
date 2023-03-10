using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    List<Node> path = new List<Node>();
    [SerializeField] [Range(0f, 5f)] float speed = 1f;

    Enemy enemy;
    GridManager gridManager;
    Pathfinder pathfinder;
    
    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
    }

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if(resetPath)
        {
            coordinates = pathfinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    void FinishPath()
    {
        enemy.StealCredit();
        gameObject.SetActive(false);
    }
    
    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }

    IEnumerator FollowPath()
    {
        for(int i = 1; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercnet = 0f;
            
            transform.LookAt(endPosition);
            
            while(travelPercnet < 1f)
            {
                travelPercnet += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercnet);
                yield return new WaitForEndOfFrame();
            }
        }

        FinishPath();
    }
}
