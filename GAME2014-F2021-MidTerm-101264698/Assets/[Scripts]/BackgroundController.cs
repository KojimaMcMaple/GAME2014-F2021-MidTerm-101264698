/// <summary>
/// Source file Name: BackgroundController.cs
/// Student Name: Trung Le (Kyle)
/// StudentID: 101264698 
/// Date last Modified: 
/// Program description: 
/// Short Revision History: 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveBoundary = 10f;
    [SerializeField] private bool is_second_bkg_ = false;
    private GameManager game_manager_;

    void Start() //to wait for Screen orientation set from GameManager
    {
        game_manager_ = FindObjectOfType<GameManager>();
        Bounds sprite_bounds = GetComponent<SpriteRenderer>().sprite.bounds;
        float scale_factor_x = (game_manager_.GetTopRightMaxPos().x / sprite_bounds.extents.x);
        float scale_factor_y = (game_manager_.GetTopRightMaxPos().y / sprite_bounds.extents.y);
        float scale_factor = scale_factor_x > scale_factor_y ? scale_factor_x : scale_factor_y;
        transform.localScale = new Vector3(scale_factor, scale_factor, 1);
        
        moveBoundary = (Mathf.Abs(game_manager_.GetBottomLeftMaxPos().x) + (sprite_bounds.extents.x * transform.localScale.x)); //find the screen bounds in world, then add the sprite size to set bounds
        
        transform.position = new Vector3(0, 0, 0);
        if (is_second_bkg_)
        {
            transform.position = new Vector3(game_manager_.GetTopRightMaxPos().x + (sprite_bounds.extents.x * transform.localScale.x), 0, 0);
        }
        
    }

    void Update()
    {
        _Move();
        _CheckBounds();
    }

    /// <summary>
    /// Resets position of bkg
    /// </summary>
    private void _Reset()
    {
        transform.position = new Vector3(moveBoundary, 0.0f);
    }

    /// <summary>
    /// Moves bkg from right to left
    /// </summary>
    private void _Move()
    {
        transform.position -= new Vector3(moveSpeed, 0.0f) * Time.deltaTime;
    }

    /// <summary>
    /// Checks if bkg has hit bounds
    /// </summary>
    private void _CheckBounds()
    {
        // if the background is lower than the bottom of the screen then reset
        if (transform.position.x <= -moveBoundary)
        {
            _Reset();
        }
    }
}
