using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeepneyController : MonoBehaviour
{
    Rigidbody2D jeepneyRB;
    float moveSpeed = 700.0f;
    float direction = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        jeepneyRB = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        jeepneyRB.velocity = new Vector2(direction * moveSpeed * Time.deltaTime, jeepneyRB.velocity.y);
    }
}
