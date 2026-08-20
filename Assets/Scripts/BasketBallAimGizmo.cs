using UnityEngine;

public class BasketBallAimGizmo : MonoBehaviour
{
    [SerializeField] private float длинаСтрелки = 2f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 начало = transform.position;
        Vector3 направление = transform.forward * длинаСтрелки;

        Gizmos.DrawRay(начало, направление);

        Vector3 конец = начало + направление;

        Gizmos.DrawSphere(конец, 0.08f);
    }
}