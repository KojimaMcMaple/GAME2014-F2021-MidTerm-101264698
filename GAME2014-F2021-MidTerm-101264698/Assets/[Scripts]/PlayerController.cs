﻿/// <summary>
/// Source file Name: PlayerController.cs
/// Student Name: Trung Le (Kyle)
/// StudentID: 101264698 
/// Date last Modified: 
/// Program description: 
/// Short Revision History: 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public BulletManager bulletManager;

    [Header("Player Speed")]
    public float moveSpeed = 2.0f;
    public float maxSpeed = 6.0f;
    public float moveTValue = 0.1f;

    [Header("Bullet Firing")]
    public float fireDelay;

    // Private variables
    private Rigidbody2D m_rigidBody;
    private Vector3 m_touchesEnded;
    [Header("Boundary Check")] private float moveBoundary = 1.9f;

    void Awake()
    {
        Bounds sprite_bounds = GetComponent<SpriteRenderer>().sprite.bounds;
        Vector3 top_right_max_pos = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        //Vector3 left_down_max_pos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        moveBoundary = (Mathf.Abs(top_right_max_pos.y) - (sprite_bounds.extents.y * transform.localScale.y)); //find the screen bounds in world, then substract the sprite size to set bounds
        m_touchesEnded = new Vector3();
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
        _CheckBounds();
        _FireBullet();
    }

    /// <summary>
    /// Shoots one bullet every 60 frames
    /// </summary>
     private void _FireBullet()
    {
        // delay bullet firing 
        if(Time.frameCount % 60 == 0 && bulletManager.HasBullets())
        {
            bulletManager.GetBullet(transform.position);
        }
    }

    /// <summary>
    /// Moves the ship up and down based on user input
    /// </summary>
    private void _Move()
    {
        float direction = 0.0f;

        // touch input support
        foreach (var touch in Input.touches)
        {
            var worldTouch = Camera.main.ScreenToWorldPoint(touch.position);

            // if user touches above ship, move ship up
            if (worldTouch.y > transform.position.y)
            {
                // direction is positive
                direction = 1.0f;
            }

            // if user touches above ship, move ship down
            if (worldTouch.y < transform.position.y)
            {
                // direction is negative
                direction = -1.0f;
            }

            m_touchesEnded = worldTouch;

        }

        // keyboard support
        if (Input.GetAxis("Vertical") >= 0.1f) 
        {
            // direction is positive
            direction = 1.0f;
        }

        if (Input.GetAxis("Vertical") <= -0.1f)
        {
            // direction is negative
            direction = -1.0f;
        }

        if (m_touchesEnded.y != 0.0f)
        {
           transform.position = new Vector2(transform.position.x, Mathf.Lerp(transform.position.y, m_touchesEnded.y, moveTValue));
        }
        else
        {
            Vector2 newVelocity = m_rigidBody.velocity + new Vector2(0.0f, direction * moveSpeed);
            m_rigidBody.velocity = Vector2.ClampMagnitude(newVelocity, maxSpeed);
            m_rigidBody.velocity *= 0.99f;
        }
    }

    /// <summary>
    /// Prevents player from going out of bounds
    /// </summary>
    private void _CheckBounds()
    {
        Debug.Log(transform.position.y);
        Debug.Log(moveBoundary);
        // check right bounds
        if (transform.position.y >= moveBoundary)
        {
            transform.position = new Vector3(transform.position.x, moveBoundary, 0.0f);
        }

        // check left bounds
        if (transform.position.y <= -moveBoundary)
        {
            transform.position = new Vector3(transform.position.x, -moveBoundary, 0.0f);
        }

    }

    /// <summary>
    /// Debug for screen positions
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Bounds sprite_bounds = GetComponent<SpriteRenderer>().sprite.bounds;
        Camera camera = Camera.main;
        Vector3 p = camera.ViewportToWorldPoint(new Vector3(1, 1, 0));
        Vector3 p2 = camera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(p, sprite_bounds.size.y * transform.localScale.y);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(p2, sprite_bounds.size.y * transform.localScale.y);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(new Vector3(0, Mathf.Abs(p.y) - sprite_bounds.extents.y * transform.localScale.y, 0), sprite_bounds.extents.y * transform.localScale.y);
    }
}
