using UnityEngine;

public class DinoController : MonoBehaviour
{
    [Header("Настройки прыжка")]
    public float силаПрыжка = 10f;

    private Rigidbody2D физика;
    private bool наЗемле = false;
    private GameOverManager менеджерОкончания;
    private Animator аниматор;

    void Start()
    {
        физика = GetComponent<Rigidbody2D>();
        менеджерОкончания = FindObjectOfType<GameOverManager>();
        аниматор = GetComponent<Animator>();
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

            аниматор.SetBool("Бежит", false);
        }
    }

    void OnCollisionEnter2D(Collision2D столкновение)
    {
        if (столкновение.gameObject.name == "Ground")
        {
            наЗемле = true;

            аниматор.SetBool("Бежит", true);
        }

        if (столкновение.gameObject.CompareTag("Cactus"))
        {
            менеджерОкончания.ПоказатьМеню();
        }
    }
}