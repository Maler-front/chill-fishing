using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [SerializeField]
    private Camera _camera;

    [Header("Системы")]
    [SerializeField]
    private UISystem _UISystem;
    [SerializeField]
    private InputSystem _inputSystem;
    [SerializeField]
    private FishesSystem _fishesSystem;

    public static bool Paused { get; set; }

    public EventHandler OnAwake;
    public EventHandler OnStart;
    public EventHandler OnUpdate;

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("Singleton error (in GameController class)!");
        Instance = this;

        ActivateAllSystems();

        CameraAnalizer.CalculateSpawnRadius(_camera);

        OnAwake?.Invoke(this, EventArgs.Empty);
    }

    private void Start()
    {
        OnStart?.Invoke(this, EventArgs.Empty);
    }

    private void Update()
    {
        if (Paused)
        {
            Time.timeScale = 0;
            return;
        }
        else
        {
            Time.timeScale = 1;
        }

        OnUpdate?.Invoke(this, EventArgs.Empty);
    }

    private void ActivateAllSystems()
    {
        _UISystem.Subscribe();
        _inputSystem.Subscribe();
        _fishesSystem.Subscribe();
    }
}
