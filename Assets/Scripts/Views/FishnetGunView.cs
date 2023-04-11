using System.Collections.Generic;
using UnityEngine;

public class FishnetGunView : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _pathRenderer;
    [SerializeField]
    private LineRenderer _circleRenderer;
    [SerializeField]
    private float _circleRadius;
    private List<Vector3> _fishnetsPositions;
    private List<Vector3> _fishnetsVelosities;

    private void Awake()
    {
        _fishnetsPositions = new List<Vector3>();
        _fishnetsVelosities = new List<Vector3>();
        Hide();
    }

    public void Hide()
    {
        _pathRenderer.positionCount = 0;
        _pathRenderer.enabled = false;
        _circleRenderer.positionCount = 0;
        _circleRenderer.enabled = false;
    }

    public void Show()
    {
        _pathRenderer.enabled = true;
        _circleRenderer.enabled = true;
    }

    public void ReDraw(Vector3[] positions)
    {
        if (positions != null)
        {
            ReDrawPath(positions);
            positions[positions.Length - 1].y = 0f;
            ReDrawCircle(positions[positions.Length - 1]);
        }
    }

    private void ReDrawPath(Vector3[] positions)
    {
        _pathRenderer.positionCount = positions.Length;
        _pathRenderer.SetPositions(positions);
    }

    private void ReDrawCircle(Vector3 center)
    {
        int circleDots = 30;
        float step = (360f / circleDots) / 180 * Mathf.PI;

        _circleRenderer.positionCount = circleDots + 2;
        for (int i = 0; i < circleDots + 2; i++)
        {
            _circleRenderer.SetPosition(i, new Vector3(center.x + _circleRadius * Mathf.Cos(step * i), 0f, center.z + _circleRadius * Mathf.Sin(step * i)));
        }
    }

    public void BakeFishnetPositions()
    {
        foreach (Fishnet component in transform.GetComponentsInChildren<Fishnet>())
        {
            _fishnetsPositions.Add(component.transform.position);
            _fishnetsVelosities.Add(component.Rigidbody.velocity);
            component.Rigidbody.useGravity = false;
        }
    }

    public void UnbakeFishnetPositions()
    {
        int i = 0;
        foreach (Fishnet component in transform.GetComponentsInChildren<Fishnet>())
        {
            component.transform.position = _fishnetsPositions[i];
            component.Rigidbody.velocity = _fishnetsVelosities[i++];
            component.Rigidbody.useGravity = true;
        }

        _fishnetsPositions.Clear();
        _fishnetsVelosities.Clear();
    }

    public void DeleteFishnet(GameObject fishnet) => Destroy(fishnet);
}
