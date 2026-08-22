using UnityEngine;

public class ВеревкаRenderer : MonoBehaviour
{
    private LineRenderer линия;

    private void Start()
    {
        линия = GetComponent<LineRenderer>();

        линия.positionCount = transform.childCount;

        for (int i = 0; i < transform.childCount; i++)
        {
            линия.SetPosition(i, transform.GetChild(i).position);
        }
    }

    private void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            линия.SetPosition(i, transform.GetChild(i).position);
        }
    }
}