using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    TopRight,
    BottomRight,
    BottomLeft,
    TopLeft,
    Right,
    Left,
    Top,
    Bottom,
    Center
}
public class Vector3DirectionCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Direction Check(Vector3 inputVector)
    {
        if (inputVector.x > 0 && inputVector.y > 0)
        {
            return Direction.TopRight;
        }
        else if (inputVector.x > 0 && inputVector.y < 0)
        {
            return Direction.BottomRight;
        }
        else if (inputVector.x < 0 && inputVector.y < 0)
        {
            return Direction.BottomLeft;
        }
        else if (inputVector.x < 0 && inputVector.y > 0)
        {
            return Direction.TopLeft;
        }
        else if (inputVector.x > 0)
        {
            return Direction.Right;
        }
        else if (inputVector.x < 0)
        {
            return Direction.Left;
        }
        else if (inputVector.y > 0)
        {
            return Direction.Top;
        }
        else if (inputVector.y < 0)
        {
            return Direction.Bottom;
        }
        else
        {
            return Direction.Center;
        }
    }
}
