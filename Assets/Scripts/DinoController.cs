using UnityEngine;
using System.Collections;

public class DinoController : MonoBehaviour
{
    [Header("Настройки прыжка")]
    public float силаПрыжка = 10f;

    [Header("Настройки двойного прыжка")]
    public float множительВторогоПрыжка = 0.5f;

    [Header("Анимация проигрыша")]
    public DeathAnimation анимацияСмерти;

    private Rigidbody2D физика;
    private bool наЗемле = false;
    private GameOverManager менеджерОкончания;
    private Animator аниматор;

    private int количествоПрыжков = 0;
    private int максимальноеКоличествоПрыжков = 2;

    private bool проигрыш = false;

    void Start()
    {
        физика = GetComponent<Rigidbody2D>();
        менеджерОкончания = FindObjectOfType<GameOverManager>();
        аниматор = GetComponent<Animator>();
    }

    void Update()
    {
        if (проигрыш)
            return;

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
            аниматор.SetBool("Прыгает", true);

            if (количествоПрыжков == 2)
            {
                аниматор.SetBool("ВторойПрыжок", true);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D столкновение)
    {
        if (проигрыш)
            return;

        if (столкновение.gameObject.name == "Ground")
        {
            наЗемле = true;
            количествоПрыжков = 0;

            аниматор.SetBool("Прыгает", false);
            аниматор.SetBool("ВторойПрыжок", false);
            аниматор.SetBool("Бежит", true);
        }

        if (столкновение.gameObject.CompareTag("Cactus"))
        {
            НачатьПроигрыш();
        }

        if (столкновение.gameObject.CompareTag("Bird"))
        {
            НачатьПроигрыш();
        }
    }

    void НачатьПроигрыш()
    {
        проигрыш = true;

        // Останавливаем физику котика
        физика.linearVelocity = Vector2.zero;
        физика.simulated = false;

        // Останавливаем его обычную анимацию
        аниматор.enabled = false;

        // Получаем SpriteRenderer котика
        SpriteRenderer спрайтКотика = GetComponent<SpriteRenderer>();

        // Если он есть — скрываем обычного котика
        if (спрайтКотика != null)
        {
            спрайтКотика.enabled = false;
        }

        // Показываем картинку смерти
        if (анимацияСмерти != null)
        {
            анимацияСмерти.transform.position = transform.position;
            анимацияСмерти.gameObject.SetActive(true);
            анимацияСмерти.НачатьПадение();
        }

        // Через небольшую задержку показываем меню проигрыша
        StartCoroutine(ПоказатьПроигрыш());
    }

    IEnumerator ПоказатьПроигрыш()
    {
        yield return new WaitForSecondsRealtime(0.7f);

        менеджерОкончания.ПоказатьМеню();
    }
}