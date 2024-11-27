using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;

    [SerializeField] private float moveSpeed = 2f;

    private int waypointIndex = 0;


    private bool isFacingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
    }
    void Update()
    {
        //Flip(); Need to fix
        Move();
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector2 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private void Move()
    {
        // if enemy didnt reach last waypoint it can move
        // if enemy reached last waypoint then it stops
        if (waypointIndex <= waypoints.Length - 1)
        {

            transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);

            //if enemy reaches position of waypoint he moves towards
            //then the waypointindex is increased by 1
            //and enemy starts to walk to next 1
                if (transform.position == waypoints[waypointIndex].transform.position)
                {
                waypointIndex += 1;
                
                Debug.Log("Waypoint" +  waypointIndex);
            }

        }
        if (waypointIndex < waypoints.Length)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;

            GetComponent<Rigidbody>().MovePosition(newPosition);

            // Check if the object is close to the waypoint, then change the waypointIndex
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                waypointIndex++;
                Debug.Log("Increment");

                if (waypointIndex >= waypoints.Length)
                {
                    waypointIndex = 0;
                    Debug.Log("Reset Waypoint");
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, waypoints[waypointIndex].position);
    }
}
