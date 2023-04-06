using UnityEngine;

public class FishModel 
{
    public float Speed { get; private set; }
    public Vector2 MovingDirection { get; private set; }

    public FishModel(float fishMinSpeed, float fishMaxSpeed, Vector2 fishMinDeviationFromTheMovingDirection, Vector2 fishMaxDeviationFromTheMovingDirection, Vector3 spawnPosition)
    {
        Speed = Random.Range(fishMinSpeed, fishMaxSpeed);

        Vector3 movingDirection3D = Vector3.zero - spawnPosition;
        MovingDirection = new Vector2(movingDirection3D.x, movingDirection3D.z);

        float deviationX = Random.Range(fishMinDeviationFromTheMovingDirection.x, fishMaxDeviationFromTheMovingDirection.x);
        float deviationY = Random.Range(fishMinDeviationFromTheMovingDirection.y, fishMaxDeviationFromTheMovingDirection.y);
        Vector2 deviation = new Vector2(deviationX, deviationY);
        MovingDirection += deviation;
    }

    public Vector3 GetChangePosition() {
        return new Vector3(MovingDirection.x, 0, MovingDirection.y).normalized * Speed;
    }
}
