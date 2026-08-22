using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Параметры")]
    [SerializeField] private float скоростьСледования = 5f;
    [SerializeField] private float скоростьПоворота = 5f;

    [Header("Объект")]
    [SerializeField] private Transform персонаж;

    private Vector3 смещение;

    private void Start()
    {
        смещение = transform.position - персонаж.position;
    }

    private void LateUpdate()
    {
        if (персонаж == null)
            return;

        Vector3 целевоеСмещение = персонаж.rotation * смещение;

        Vector3 целеваяПозиция = персонаж.position + целевоеСмещение;

        transform.position = Vector3.Lerp(
            transform.position,
            целеваяПозиция,
            скоростьСледования * Time.deltaTime
        );

        Vector3 направление = персонаж.position - transform.position;

        if (направление != Vector3.zero)
        {
            Quaternion целевойПоворот = Quaternion.LookRotation(направление);

            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                целевойПоворот,
                скоростьПоворота * Time.deltaTime
            );
        }
    }
}