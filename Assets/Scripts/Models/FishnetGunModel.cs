using System;
using System.Collections.Generic;
using UnityEngine;

public class FishnetGunModel
{
    private readonly float _simulationDeltaTime;
    private readonly float _maxForce;
    private readonly float _minForce;
    private readonly float _forceCoefficientForPlayer;
    private readonly float _fireAngle;

    public Action OnSimulationStart;
    public Action OnSimulationEnd;

    public FishnetGunModel(
        float simulationDeltaTime,
        float maxForce,
        float minForce,
        float forceCoefficientForPlayer,
        float fireAngle
        )
    {
        _simulationDeltaTime = simulationDeltaTime;
        _maxForce = maxForce;
        _minForce = minForce;
        _forceCoefficientForPlayer = forceCoefficientForPlayer;
        _fireAngle = fireAngle;
    }

    public void Fire(GameObject fishnet, TouchController.OnFingerMoovingEventArgs e)
    {
        FireArguments fireArguments = CalculateFireArguments(e);
        float force = Mathf.Clamp(fireArguments.PlayerForce * _forceCoefficientForPlayer + _minForce, 0f, _maxForce);
        Vector3 impulseDirection = new Vector3(-fireArguments.Direction.x, fireArguments.Direction.y, -fireArguments.Direction.z);
        fishnet.GetComponent<Rigidbody>().AddForce(impulseDirection * force, ForceMode.Impulse);
    }

    public Vector3[] SimulateFishnetPath(GameObject simulatingFishnet, TouchController.OnFingerMoovingEventArgs e)
    {
        FireArguments fireArguments = CalculateFireArguments(e);

        List<Vector3> points = new List<Vector3>();

        OnSimulationStart?.Invoke();
        Physics.autoSimulation = false;

        Fire(simulatingFishnet, e);

#if UNITY_EDITOR
        int i = 500;
        do
        {
            Physics.Simulate(_simulationDeltaTime);
            points.Add(simulatingFishnet.transform.position);
        } while (simulatingFishnet.transform.position.y >= 0f && i-- >= 0);
        if (i < 0)
            Debug.LogError("The simulation was forcibly interrupted!");
#else
        do
        {
            Physics.Simulate(_simulationDeltaTime);
            points.Add(simulatingFishnet.transform.position);
        } while (simulatingFishnet.transform.position.y >= 0f);
#endif

        Physics.autoSimulation = true;
        OnSimulationEnd?.Invoke();

        Vector3[] positions = new Vector3[points.Count];
        points.CopyTo(positions);

        return positions;
    }

    private FireArguments CalculateFireArguments(TouchController.OnFingerMoovingEventArgs e)
    {
        Vector2 direction2D = e.movedTo - e.firstTouch;
        Vector2 directionNormalized2D = direction2D != Vector2.zero ? direction2D.normalized : -Vector2.up;
        Vector3 direction3D = new Vector3(directionNormalized2D.x, Mathf.Tan(_fireAngle / 180 * Mathf.PI), directionNormalized2D.y);
        return new FireArguments(direction3D, direction2D.magnitude);
    }

    private class FireArguments
    {
        public Vector3 Direction { get; private set; }
        public float PlayerForce { get; private set; }

        public FireArguments(Vector3 direction, float playerForce)
        {
            Direction = direction;
            PlayerForce = playerForce;
        }
    }
}
