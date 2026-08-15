using UnityEngine;

public class DinoController : MonoBehaviour
{
    [Header("Настройки прыжка")]
    public float силаПрыжка = 10f;

   private Rigidbody2D физика;
private bool наЗемле = false;
private GameOverManager менеджерОкончания;

    void Start()
{
    физика = GetComponent<Rigidbody2D>();
    менеджерОкончания = FindObjectOfType<GameOverManager>();
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

    if (столкновение.gameObject.CompareTag("Cactus"))
    {
        менеджерОкончания.ПоказатьМеню();
    }
}
}