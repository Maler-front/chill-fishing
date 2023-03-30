using System.Collections.Generic;
using UnityEngine;

public class FishnetGun : MonoBehaviour
{
    [SerializeField]
    private FishnetPathRenderer _fishnetPathRenderer;
    [SerializeField]
    private GameObject _fishnetPrefab;
    [SerializeField][Range(0.01f, 0.5f)]
    private float _simulationDeltaTime;
    [SerializeField]
    private float _maxForce;
    [SerializeField]
    private float _minForce;
    [SerializeField]
    private float _forceCoefficientForPlayer;
    [SerializeField][Range(0.01f,89.99f)]
    private float _fireAngle;

    private void Start()
    {
        PlayerInput playerInput = PlayerInput.Instance;
        playerInput.OnFingerMoved += PlayerInput_OnFingerMoved;
        playerInput.OnScreenUntouched += PlayerInput_OnScreenUntouched;
    }

    private void PlayerInput_OnFingerMoved(object sender, PlayerInput.OnFingerMoovingEventArgs e)
    {
        _fishnetPathRenderer.ReDraw(
            SimulateFishnetPath(
                CalculateFireArguments(e)
                )
            );
    }

    private void PlayerInput_OnScreenUntouched(object sender, PlayerInput.OnFingerMoovingEventArgs e)
    {
        FireArguments fireArguments = CalculateFireArguments(e);
        GameObject fishnet = CreateFishnet();
        Fire(fishnet, fireArguments);
    }

    private void Fire(GameObject fishnet, FireArguments fireArguments)
    {
        float force = Mathf.Clamp(fireArguments.PlayerForce * _forceCoefficientForPlayer, _minForce, _maxForce);
        Vector3 impulseDirection = new Vector3(-fireArguments.Direction.x, fireArguments.Direction.y, -fireArguments.Direction.z);
        fishnet.GetComponent<Rigidbody>().AddForce(impulseDirection * force, ForceMode.Impulse);
    }

    private Vector3[] SimulateFishnetPath(FireArguments fireArguments)
    {
        //Подготовка к симуляции--------------------------------
        List<Vector3> points = new List<Vector3>();
        List<Vector3> fishnetPositions = new List<Vector3>();
        foreach (Fishnet component in transform.GetComponentsInChildren<Fishnet>())
        {
            fishnetPositions.Add(component.transform.position);
            component.GetComponent<Rigidbody>().useGravity = false;
        }

        GameObject simulatedFishnet = CreateFishnet(isFishnetNeedToBeSimuleted: true);
        Fire(simulatedFishnet, fireArguments);

        float force = Mathf.Clamp(fireArguments.PlayerForce * _forceCoefficientForPlayer, _minForce, _maxForce);
        //Начальная скорость объекта
        float V = (fireArguments.Direction * force).magnitude;
        float tan = Mathf.Tan(_fireAngle / 180 * Mathf.PI);
        //Время, через которое объект упадет на воду
        float t = 2 * V * Mathf.Sin(_fireAngle / 180 * Mathf.PI) / -Physics.gravity.y;
        //Конец подготовки к симуляции--------------------------------

        //Симуляция
        Physics.autoSimulation = false;
        for (float timeLeft = t; timeLeft > 0f; timeLeft -= _simulationDeltaTime)
        {
            Physics.Simulate(_simulationDeltaTime);
            points.Add(simulatedFishnet.transform.position);
        }
        Destroy(simulatedFishnet);

        int i = 0;
        foreach (Fishnet component in transform.GetComponentsInChildren<Fishnet>())
        {
            component.transform.position = fishnetPositions[i++];
            component.GetComponent<Rigidbody>().useGravity = true;
        }

        Physics.autoSimulation = true;

        //Обработка полученных точек пути
        Vector3[] positions = new Vector3[points.Count];
        points.CopyTo(positions);

        return positions;
    }

    private GameObject CreateFishnet(bool isFishnetNeedToBeSimuleted = false)
    {
        Vector3 origin = new Vector3(transform.parent.position.x, transform.parent.position.y + 1f, transform.parent.position.z);
        return isFishnetNeedToBeSimuleted ? Instantiate(_fishnetPrefab, origin, Quaternion.identity) :
                                            Instantiate(_fishnetPrefab, origin, Quaternion.identity, transform);
    }

    private FireArguments CalculateFireArguments(PlayerInput.OnFingerMoovingEventArgs e)
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
