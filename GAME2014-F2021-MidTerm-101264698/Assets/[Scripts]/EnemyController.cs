/// <summary>
/// Source file Name: EnemyController.cs
/// Student Name: Trung Le (Kyle)
/// StudentID: 101264698 
/// Date last Modified: 
/// Program description: 
/// Short Revision History: 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float moveBoundary = 2.1f;
    public float direction = 1.0f;

    private float screen_pos_ratio = 0.7f;
    private GameManager game_manager_;

    void Start() //to wait for Screen orientation set from GameManager
    {
        game_manager_ = FindObjectOfType<GameManager>();
        Bounds sprite_bounds = GetComponent<SpriteRenderer>().sprite.bounds;
        moveBoundary = (Mathf.Abs(game_manager_.GetTopRightMaxPos().y) - (sprite_bounds.extents.y * transform.localScale.y)); //find the screen bounds in world, then add the sprite size to set bounds

        screen_pos_ratio = transform.position.x / 10.55556f; //Samsung Galaxy S10+ screen world pos max, which is the standard
    }

    void Update()
    {
        _SetXPosOnScreenOrientation();
        _Move();
        _CheckBounds();
    }

    /// <summary>
    /// Moves the enemy up and down, depending on direction
    /// </summary>
    private void _Move()
    {
        transform.position += new Vector3(0.0f, moveSpeed * direction * Time.deltaTime, 0.0f);
    }

    /// <summary>
    /// Reflects direction if bound is hit
    /// </summary>
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

    /// <summary>
    /// Sets the x pos based on screen orientation
    /// </summary>
    private void _SetXPosOnScreenOrientation()
    {
        //ship pos  / screen world pos max              = what percentage of the screen the ship should be positioned at 
        //-7.34     / 10.55556 (Samsung Galaxy S10+)    = 0.69536812826     = 0.7f rounded
        switch (Screen.orientation)
        {
            case ScreenOrientation.Portrait:
            case ScreenOrientation.PortraitUpsideDown:
                transform.position = new Vector3(game_manager_.GetTopRightMaxPos().y / game_manager_.GetScreenAspect() * screen_pos_ratio,
                                                    transform.position.y, transform.position.z); //player will always be 70% to the left, has to divide by GetScreenAspect or ship will go off screen
                break;
            case ScreenOrientation.LandscapeLeft:
            case ScreenOrientation.LandscapeRight:
                transform.position = new Vector3(game_manager_.GetTopRightMaxPos().x * screen_pos_ratio,
                                                    transform.position.y, transform.position.z); //player will always be 70% to the left
                break;
            default:
                break;
        }
    }
}
