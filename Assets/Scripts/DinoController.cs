using UnityEngine;

public class DinoController : MonoBehaviour
{
    [Header("Настройки прыжка")]
    public float силаПрыжка = 10f;

    [Header("Настройки двойного прыжка")]
    public float множительВторогоПрыжка = 0.5f;

    private Rigidbody2D физика;
    private bool наЗемле = false;
    private GameOverManager менеджерОкончания;
    private Animator аниматор;

    private int количествоПрыжков = 0;
    private int максимальноеКоличествоПрыжков = 2;

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
        if (количествоПрыжков < максимальноеКоличествоПрыжков)
        {
            float силаТекущегоПрыжка = силаПрыжка;

            if (количествоПрыжков == 1)
            {
                силаТекущегоПрыжка = силаПрыжка * множительВторогоПрыжка;
            }

            физика.linearVelocity = new Vector2(0, силаТекущегоПрыжка);

            количествоПрыжков++;

            наЗемле = false;
            аниматор.SetBool("Бежит", false);
        }
    }

    void OnCollisionEnter2D(Collision2D столкновение)
    {
        if (столкновение.gameObject.name == "Ground")
        {
            наЗемле = true;
            количествоПрыжков = 0;

            аниматор.SetBool("Бежит", true);
        }

        if (столкновение.gameObject.CompareTag("Cactus"))
        {
            менеджерОкончания.ПоказатьМеню();
        }
    }
}