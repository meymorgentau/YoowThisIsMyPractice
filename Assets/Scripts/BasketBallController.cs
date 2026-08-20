using UnityEngine;

public class BasketBallController : MonoBehaviour
{
    [SerializeField] private float силаБроска = 2f;

    private Rigidbody rb;
    private Vector3 начальнаяПозиция;

    private Vector2 началоБроска;
    private bool мячЗапущен;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        начальнаяПозиция = transform.position;

        rb.isKinematic = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            началоБроска = new Vector2(
                Input.mousePosition.x / Screen.width,
                1f - Input.mousePosition.y / Screen.height
            );
        }

        if (Input.GetMouseButtonUp(0) && !мячЗапущен)
        {
            Vector2 конецБроска = new Vector2(
                Input.mousePosition.x / Screen.width,
                1f - Input.mousePosition.y / Screen.height
            );

            Vector3 направление = new Vector3(
                0f,
                конецБроска.y - началоБроска.y,
                началоБроска.x - конецБроска.x
            );

            rb.isKinematic = false;
            rb.AddForce(направление * силаБроска, ForceMode.Impulse);

            мячЗапущен = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ВернутьМяч();
        }
    }

    private void ВернутьМяч()
    {
        rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = начальнаяПозиция;

        мячЗапущен = false;
    }
}