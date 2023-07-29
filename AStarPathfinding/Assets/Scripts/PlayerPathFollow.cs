using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPathFollow : MonoBehaviour
{
    public float speed = 3f;
    public bool drawPath;

    protected void FollowPath(Vector3 targetPos)
    {
        List<Vector3> path = AStarPathfinding.Instance.FindPath(transform.position, targetPos);
        if (path == null)
            return;

        DrawPath(path);
        StopAllCoroutines();
        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<Vector3> path)
    {
        int iter = 0;
        int pathLen = path.Count;
        while (iter < pathLen)
        {
            Vector3 currentWayPoint = path[iter];
            if (transform.position == currentWayPoint)
                iter++;

            transform.position = Vector3.MoveTowards(transform.position, currentWayPoint, speed * Time.deltaTime);

            yield return null;
        }
    }

    void DrawPath(List<Vector3> path)
    {
        if (!drawPath)
            return;

        for (int i = 0; i < path.Count; i++)
        {
            if (i + 1 < path.Count)
                Debug.DrawLine(path[i], path[i + 1], Color.green, 5f);
        }
    }
}
