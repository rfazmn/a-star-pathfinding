using UnityEngine;

public class PlayerClickFollow : PlayerPathFollow
{
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            FollowPath(worldPos);
        }
    }
}
