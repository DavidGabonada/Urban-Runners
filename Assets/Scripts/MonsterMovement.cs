using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public Transform[] patrolPoints;  // Array to store patrol points
    public float moveSpeed;           // Movement speed of the monster
    public int patrolDestination;     // The current patrol destination (index)

    void Update()
    {
        // Check if the monster is moving towards patrol point 0
        if (patrolDestination == 0)
        {
            // Move towards patrol point 0
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);

            // If the monster is close to patrol point 0, update its scale and move to the next patrol point
            if (Vector2.Distance(transform.position, patrolPoints[0].position) < 0.2f)
            {
                transform.localScale = new Vector3(1, 1, 1);  // Example: keep the scale normal
                patrolDestination = 1;  // Move to patrol point 1
            }
        }
        // Check if the monster is moving towards patrol point 1
        else if (patrolDestination == 1)
        {
            // Move towards patrol point 1
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);

            // If the monster is close to patrol point 1, update its scale and move to the next patrol point
            if (Vector2.Distance(transform.position, patrolPoints[1].position) < 0.2f)
            {
                transform.localScale = new Vector3(-1, 1, 1);  // Example: flip the scale horizontally (optional)
                patrolDestination = 0;  // Move back to patrol point 0
            }
        }
    }
}
