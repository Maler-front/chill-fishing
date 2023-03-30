using UnityEngine;

public static class CameraAnalizer
{
    public static Vector3 GetLeftMainCameraCorner()
    {
        return Camera.main.ScreenPointToRay(
            new Vector3(0, 0)).GetPoint(Camera.main.farClipPlane);
    }

    public static Vector3 GetRightMainCameraCorner()
    {
        return Camera.main.ScreenPointToRay(
            new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight)).GetPoint(Camera.main.farClipPlane);
    }

    public static float GetMinRadius(Vector3 leftCorner, Vector3 rightCorner)
    {
        return Mathf.Max(Mathf.Abs(leftCorner.x - rightCorner.x), Mathf.Abs(leftCorner.z - rightCorner.z));
    }
}
