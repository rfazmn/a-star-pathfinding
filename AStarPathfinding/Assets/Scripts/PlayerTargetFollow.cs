using UnityEngine;

public class PlayerTargetFollow : PlayerPathFollow
{
    public Transform target;

    void Start()
    {
        FollowPath(target.position);
    }
}
