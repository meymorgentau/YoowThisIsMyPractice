using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Параметры")]
    [SerializeField] float скорость;
    [SerializeField] float скоростьПоворота;
    [SerializeField] float силаПрыжка;
    [SerializeField] float гравитация = -9.81f;

    [Header("Текущее")]
    [SerializeField] float вперед_назад;
    [SerializeField] float влево_вправо;
    [SerializeField] float вертикальнаяСкорость;

    [Header("Компоненты")]
    [SerializeField] CharacterController контроллер;

    void Update()
    {
        вперед_назад = 0;
        влево_вправо = 0;

        if (Input.GetKey(KeyCode.W))
        {
            вперед_назад = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            вперед_назад = -1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            влево_вправо = 1f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            влево_вправо = -1f;
        }

        Vector3 движение = new Vector3(
            влево_вправо,
            0,
            вперед_назад
        );

        движение = transform.TransformDirection(движение);

        контроллер.Move(движение * скорость * Time.deltaTime);

        if (контроллер.isGrounded)
        {
            if (вертикальнаяСкорость < 0)
            {
                вертикальнаяСкорость = -2f;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                вертикальнаяСкорость = силаПрыжка;
            }
        }

        вертикальнаяСкорость += гравитация * Time.deltaTime;

        Vector3 вертикальноеДвижение = new Vector3(
            0,
            вертикальнаяСкорость,
            0
        );

        контроллер.Move(вертикальноеДвижение * Time.deltaTime);

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(
                0,
                -скоростьПоворота * Time.deltaTime,
                0
            );
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(
                0,
                скоростьПоворота * Time.deltaTime,
                0
            );
        }
    }
}