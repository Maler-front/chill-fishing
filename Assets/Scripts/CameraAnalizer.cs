using UnityEngine;

public static class CameraAnalizer
{
    public static float SpawnRadius { get; private set; }

    public static Vector3 GetLeftMainCameraCorner(Camera camera)
    {
        return camera.ScreenPointToRay(
            new Vector3(0, 0)).GetPoint(Camera.main.farClipPlane);
    }

    public static Vector3 GetRightMainCameraCorner(Camera camera)
    {
        return camera.ScreenPointToRay(
            new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight)).GetPoint(Camera.main.farClipPlane);
    }

    public static float GetMinRadius(Vector3 leftCorner, Vector3 rightCorner)
    {
        return Mathf.Max(Mathf.Abs(leftCorner.x - rightCorner.x), Mathf.Abs(leftCorner.z - rightCorner.z));
    }

    public static void CalculateSpawnRadius(Camera camera)
    {
        float indent = 1f;

        Vector3 leftCorner = GetLeftMainCameraCorner(camera);
        Vector3 rightCorner = GetRightMainCameraCorner(camera);

        SpawnRadius = GetMinRadius(leftCorner, rightCorner) + indent;
    }
}
