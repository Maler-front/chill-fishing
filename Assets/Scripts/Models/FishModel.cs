using UnityEngine;

public class FishModel 
{
    public float Speed { get; private set; }
    public Vector2 MovingDirection { get; private set; }

    public FishModel(float speed, Vector2 movingDirection)
    {
        Speed = speed;
        MovingDirection = movingDirection;
    }

    public Vector3 GetChangePosition() {
        return new Vector3(MovingDirection.x, 0, MovingDirection.y).normalized * Speed;
    }
}
