/// <summary>
/// Source file Name: GameManager.cs
/// Student Name: Trung Le (Kyle)
/// StudentID: 101264698 
/// Date last Modified: 
/// Program description: 
/// Short Revision History: 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Vector3 top_right_max_pos_;
    private Vector3 bottom_left_max_pos_;
    private float screen_aspect_;

    void Awake()
    {
        //Screen.orientation = ScreenOrientation.LandscapeLeft; //force ScreenOrientation.LandscapeLeft at first, for easier computations
        top_right_max_pos_ = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        bottom_left_max_pos_ = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        screen_aspect_ = (float)Screen.width / Screen.height;
    }

    public Vector3 GetTopRightMaxPos()
    {
        return top_right_max_pos_;
    }

    public Vector3 GetBottomLeftMaxPos()
    {
        return bottom_left_max_pos_;
    }

    public float GetScreenAspect()
    {
        return screen_aspect_;
    }
}
