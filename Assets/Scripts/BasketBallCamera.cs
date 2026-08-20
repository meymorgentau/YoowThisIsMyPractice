using UnityEngine;

public class BasketBallCamera : MonoBehaviour
{
    [SerializeField] private Transform мяч;
    [SerializeField] private float скоростьСледования = 5f;

    private Vector3 начальнаяПозицияКамеры;
    private Vector3 начальнаяПозицияМяча;
    private Vector3 смещение;

    private void Start()
    {
        начальнаяПозицияКамеры = transform.position;
        начальнаяПозицияМяча = мяч.position;

        смещение = transform.position - мяч.position;
    }

    private void LateUpdate()
    {
        if (мяч == null)
            return;

        float расстояниеОтНачала = Vector3.Distance(
            мяч.position,
            начальнаяПозицияМяча
        );

        if (расстояниеОтНачала > 0.01f)
        {
            Vector3 целеваяПозиция = мяч.position + смещение;

            transform.position = Vector3.Lerp(
                transform.position,
                целеваяПозиция,
                скоростьСледования * Time.deltaTime
            );
        }
        else
        {
            transform.position = Vector3.Lerp(
                transform.position,
                начальнаяПозицияКамеры,
                скоростьСледования * Time.deltaTime
            );
        }
    }
}