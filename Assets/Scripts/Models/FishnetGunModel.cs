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

    public EventHandler OnSimulationStart;
    public EventHandler OnSimulationEnd;


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

    public void Fire(GameObject fishnet, PlayerInputModel.OnFingerMoovingEventArgs e)
    {
        FireArguments fireArguments = CalculateFireArguments(e);
        float force = Mathf.Clamp(fireArguments.PlayerForce * _forceCoefficientForPlayer, _minForce, _maxForce);
        Vector3 impulseDirection = new Vector3(-fireArguments.Direction.x, fireArguments.Direction.y, -fireArguments.Direction.z);
        fishnet.GetComponent<Rigidbody>().AddForce(impulseDirection * force, ForceMode.Impulse);
    }

    public Vector3[] SimulateFishnetPath(GameObject simulatingFishnet, PlayerInputModel.OnFingerMoovingEventArgs e)
    {
        FireArguments fireArguments = CalculateFireArguments(e);
        //Подготовка к симуляции--------------------------------
        List<Vector3> points = new List<Vector3>();
        List<Vector3> fishnetPositions = new List<Vector3>();
        

        Fire(simulatingFishnet, e);

        float force = Mathf.Clamp(fireArguments.PlayerForce * _forceCoefficientForPlayer, _minForce, _maxForce);
        //Начальная скорость объекта
        float V = (fireArguments.Direction * force).magnitude;
        float tan = Mathf.Tan(_fireAngle / 180 * Mathf.PI);
        //Время, через которое объект упадет на воду
        float t = 2 * V * Mathf.Sin(_fireAngle / 180 * Mathf.PI) / -Physics.gravity.y;
        //Конец подготовки к симуляции--------------------------------

        //Симуляция
        OnSimulationStart?.Invoke(this, EventArgs.Empty);
        Physics.autoSimulation = false;
        for (float timeLeft = t; timeLeft > 0f; timeLeft -= _simulationDeltaTime)
        {
            Physics.Simulate(_simulationDeltaTime);
            points.Add(simulatingFishnet.transform.position);
        }
        Physics.autoSimulation = true;
        
        OnSimulationEnd?.Invoke(this, EventArgs.Empty);

        //Обработка полученных точек пути
        Vector3[] positions = new Vector3[points.Count];
        points.CopyTo(positions);

        return positions;
    }

    private FireArguments CalculateFireArguments(PlayerInputModel.OnFingerMoovingEventArgs e)
    {
        Vector2 direction2D = e.movedTo - e.firstTouch;
        Vector2 directionNormalized2D = direction2D.normalized;
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
