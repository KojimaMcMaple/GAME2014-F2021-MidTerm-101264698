using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float moveBoundary = 2.1f;
    public float direction = 1.0f;

    void Awake()
    {
        Bounds sprite_bounds = GetComponent<SpriteRenderer>().sprite.bounds;
        Vector3 top_right_max_pos = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        moveBoundary = (Mathf.Abs(top_right_max_pos.y) - (sprite_bounds.extents.y * transform.localScale.y)); //find the screen bounds in world, then substract the sprite size to set bounds
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
        _CheckBounds();
    }

    private void _Move()
    {
        transform.position += new Vector3(0.0f, moveSpeed * direction * Time.deltaTime, 0.0f);
    }

    private void _CheckBounds()
    {
        // check right boundary
        if (transform.position.y >= moveBoundary)
        {
            direction = -1.0f;
        }

        // check left boundary
        if (transform.position.y <= -moveBoundary)
        {
            direction = 1.0f;
        }
    }
}
