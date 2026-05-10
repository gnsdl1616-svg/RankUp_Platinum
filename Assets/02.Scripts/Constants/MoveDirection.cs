using UnityEngine;

public enum MoveDirection
{
    None,
    Front, Back, Left, Right,
    LeftFront, RightFront, LeftBack, RightBack,
}

public enum PlayerState
{
    None,
    Idle,
    Move,
    Jump,
    Fall,
}

public static class Direction8
{
    public static Vector3 ToVector3(MoveDirection direction)
    {
        return direction switch
        {
            MoveDirection.Front => Vector3.forward,
            MoveDirection.Back => Vector3.back,
            MoveDirection.Left => Vector3.left,
            MoveDirection.Right => Vector3.right,
            MoveDirection.LeftFront => new Vector3(-1f,0f, 1f).normalized,
            MoveDirection.LeftBack => new Vector3(-1f, 0f, -1f).normalized,
            MoveDirection.RightFront => new Vector3(1f, 0f, 1f).normalized,
            MoveDirection.RightBack => new Vector3(1f, 0f, -1f).normalized,
            _ => Vector3.zero
        };
    }
}

/*public static class PlayerState4
{
    public static PlayerState ToState(bool state)
    {
        return state switch
        {
             => 
        
        }
    }
}*/
