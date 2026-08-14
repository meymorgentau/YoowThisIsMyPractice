using UnityEngine;

public class DinoController : MonoBehaviour
{
    [Header("Настройки прыжка")]
    public float силаПрыжка = 10f;

    private Rigidbody2D физика;
    private bool наЗемле = false;

    void Start()
    {
        физика = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ПопытатьсяПрыгнуть();
        }
    }

    void ПопытатьсяПрыгнуть()
    {
        if (наЗемле)
        {
            физика.linearVelocity = new Vector2(0, силаПрыжка);
            наЗемле = false;
        }
    }

    void OnCollisionEnter2D(Collision2D столкновение)
    {
        if (столкновение.gameObject.name == "Ground")
        {
            наЗемле = true;
        }

        if (столкновение.gameObject.GetComponent<CactusMover>() != null)
        {
            физика.linearVelocity = Vector2.zero;
            Time.timeScale = 0f;
        }
    }
}