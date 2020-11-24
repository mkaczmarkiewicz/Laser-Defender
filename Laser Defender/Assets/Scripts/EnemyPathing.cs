using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> waypoints;
    float moveSpeed;

    int indexOfCurrentTarget = 0;

    private void Start()
    {
        moveSpeed = waveConfig.GetMoveSpeed();
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[indexOfCurrentTarget].transform.position;       
    }

    private void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig _waveConfig)
    {
        this.waveConfig = _waveConfig;
    }

    private void Move()
    {
        if (indexOfCurrentTarget <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[indexOfCurrentTarget].transform.position;
            var moveThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveThisFrame);

            if (transform.position == targetPosition)
            {
                indexOfCurrentTarget++;
            }

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
