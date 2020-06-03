using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour {
    // config params
    WaveConfig waveConfig;
    List<Transform> waypoints;

    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start() {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update() {
        Move();
    }

    public void SetWaveConfig(WaveConfig setWaveConfig) {
        this.waveConfig = setWaveConfig;
    }

    private void Move() {
        if (waypointIndex <= waypoints.Count - 1) {
            Vector3 targetPosition = waypoints[waypointIndex].transform.position;
            float movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition) {
                waypointIndex++;
            }
        } else {
            Destroy(gameObject);
        }
    }
}
