using UnityEngine;

public class CameraAnalizer : IHaveToBeCalledBeforeTheStart
{
    [SerializeField] private Camera _camera;

    public float SpawnRadius { get; private set; }

    public Vector3 GetLeftCameraCorner()
    {
        return _camera.ScreenPointToRay(
            new Vector3(0, 0)).GetPoint(Camera.main.farClipPlane);
    }

    public Vector3 GetRightCameraCorner()
    {
        return _camera.ScreenPointToRay(
            new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight)).GetPoint(Camera.main.farClipPlane);
    }

    public float GetMinRadius(Vector3 leftCorner, Vector3 rightCorner)
    {
        return Mathf.Max(Mathf.Abs(leftCorner.x - rightCorner.x), Mathf.Abs(leftCorner.z - rightCorner.z));
    }

    public void CalculateSpawnRadius()
    {
        float indent = 1f;

        Vector3 leftCorner = GetLeftCameraCorner();
        Vector3 rightCorner = GetRightCameraCorner();

        SpawnRadius = GetMinRadius(leftCorner, rightCorner) + indent;
    }

    public override void Call()
    {
        CalculateSpawnRadius();
    }
}
