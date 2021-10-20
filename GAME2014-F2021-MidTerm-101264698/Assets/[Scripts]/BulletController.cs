using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for moving the individual bullets
/// </summary>
public class BulletController : MonoBehaviour, IApplyDamage
{
    public float moveSpeed = 4.0f;
    public float moveBoundary = 5.1f;
    public BulletManager bulletManager;
    public int damage;
    
    void Awake()
    {
        Bounds sprite_bounds = GetComponent<SpriteRenderer>().sprite.bounds;
        Vector3 top_right_max_pos = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        moveBoundary = (Mathf.Abs(top_right_max_pos.x) + (sprite_bounds.extents.x * transform.localScale.x)); //find the screen bounds in world, then substract the sprite size to set bounds
        bulletManager = FindObjectOfType<BulletManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
        _CheckBounds();
    }

    private void _Move()
    {
        transform.position += new Vector3(moveSpeed, 0.0f, 0.0f) * Time.deltaTime;
    }

    private void _CheckBounds()
    {
        if (transform.position.x > moveBoundary)
        {
            bulletManager.ReturnBullet(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        bulletManager.ReturnBullet(gameObject);
    }

    public int ApplyDamage()
    {
        return damage;
    }
}
