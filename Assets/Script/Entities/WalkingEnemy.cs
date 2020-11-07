﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : Enemy
{
    public WalkingPath Path;
    [HideInInspector]
    public int Progress = 0;

    public float PointTolerance = .02f;

    public override void Move()
    {
        if (Progress >= Path.waypoints.Count)
            return;

        Waypoint nextPoint = Path.waypoints[Progress];
        Vector3 direction = nextPoint.Position - transform.position;

        if (direction.sqrMagnitude < PointTolerance)
        {
            Progress++;
            nextPoint = Path.waypoints[Progress];
            direction = nextPoint.Position - transform.position;
        }

        direction = direction.normalized * MovementSpeed * Time.fixedDeltaTime;

        rigidbody.MovePosition(transform.position + direction);
    }
}
