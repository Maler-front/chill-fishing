using System.Collections.Generic;
using UnityEngine;

public class FishnetGunView : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private List<Vector3> _fishnetPositions;

    private void Awake()
    {
        _fishnetPositions = new List<Vector3>();
        _lineRenderer = GetComponent<LineRenderer>();
        Hide();
    }

    public void Hide()
    {
        _lineRenderer.positionCount = 0;
        _lineRenderer.enabled = false;
    }

    public void Show() => _lineRenderer.enabled = true;

    public void ReDraw(Vector3[] positions)
    {
        if (positions != null)
        {
            _lineRenderer.positionCount = positions.Length;
            _lineRenderer.SetPositions(positions);
        }
    }

    public void BakeFishnetPositions()
    {
        foreach (Fishnet component in transform.GetComponentsInChildren<Fishnet>())
        {
            _fishnetPositions.Add(component.transform.position);
            component.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    public void UnbakeFishnetPositions()
    {
        int i = 0;
        foreach (Fishnet component in transform.GetComponentsInChildren<Fishnet>())
        {
            component.transform.position = _fishnetPositions[i++];
            component.GetComponent<Rigidbody>().useGravity = true;
        }

        _fishnetPositions.Clear();
    }
}
