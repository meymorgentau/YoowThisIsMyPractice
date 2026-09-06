using UnityEngine;
using System.Collections;

public class DinoController : MonoBehaviour
{
    public static bool автоматическийРежим = false;

    [Header("Настройки прыжка")]
    public float силаПрыжка = 10f;

    [Header("Настройки двойного прыжка")]
    public float множительВторогоПрыжка = 0.5f;

    [Header("Настройки ИИ")]
    public float дистанцияОбнаружения = 3f;
    public float высотаНижнегоЛуча = -0.2f;
    public float высотаВерхнегоЛуча = 0.5f;
    public float задержкаМеждуРешениями = 0.4f;

    [Header("Анимация проигрыша")]
    public DeathAnimation анимацияСмерти;

    [Header("Частицы приземления")]
    public ParticleSystem частицыПриземления;

    [Header("Звуки")]
    public AudioSource звукПрыжка;
    public AudioSource звукПриземления;
    public AudioSource звукУдара;
    public AudioSource звукМяу;

    private Rigidbody2D физика;
    private bool наЗемле = false;
    private GameOverManager менеджерОкончания;
    private Animator аниматор;

    private int количествоПрыжков = 0;
    private int максимальноеКоличествоПрыжков = 2;

    private bool проигрыш = false;

    private float времяПоследнегоРешения = -10f;

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

        if (автоматическийРежим)
        {
            АнализироватьОбстановку();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            ПопытатьсяПрыгнуть();
        }
    }

void АнализироватьОбстановку()
{
    Vector2 нижняяТочка = (Vector2)transform.position +
                          new Vector2(0f, высотаНижнегоЛуча);

    Vector2 верхняяТочка = (Vector2)transform.position +
                          new Vector2(0f, высотаВерхнегоЛуча);

    RaycastHit2D[] нижниеПопадания = Physics2D.RaycastAll(
        нижняяТочка,
        Vector2.right,
        дистанцияОбнаружения
    );

    RaycastHit2D[] верхниеПопадания = Physics2D.RaycastAll(
        верхняяТочка,
        Vector2.right,
        дистанцияОбнаружения
    );

    Debug.DrawRay(
        нижняяТочка,
        Vector2.right * дистанцияОбнаружения,
        Color.red
    );

    Debug.DrawRay(
        верхняяТочка,
        Vector2.right * дистанцияОбнаружения,
        Color.yellow
    );

    bool кактусОбнаружен = false;
    bool пчелаОбнаружена = false;

    foreach (RaycastHit2D попадание in нижниеПопадания)
    {
        if (попадание.collider.CompareTag("Cactus"))
        {
            кактусОбнаружен = true;
        }

        if (попадание.collider.CompareTag("Bird"))
        {
            пчелаОбнаружена = true;
        }
    }

    foreach (RaycastHit2D попадание in верхниеПопадания)
    {
        if (попадание.collider.CompareTag("Cactus"))
        {
            кактусОбнаружен = true;
        }

        if (попадание.collider.CompareTag("Bird"))
        {
            пчелаОбнаружена = true;
        }
    }

 if (пчелаОбнаружена && количествоПрыжков == 1)
{
    Debug.Log("ИИ увидел пчелу во время первого прыжка — второй прыжок!");

    ПопытатьсяПрыгнуть();

    времяПоследнегоРешения = Time.time;
}
else if ((кактусОбнаружен || пчелаОбнаружена) &&
         количествоПрыжков == 0 &&
         Time.time >= времяПоследнегоРешения + задержкаМеждуРешениями)
{
    Debug.Log("ИИ обнаружил препятствие — первый прыжок!");

    ПопытатьсяПрыгнуть();

    времяПоследнегоРешения = Time.time;
}
}

    void ПопытатьсяПрыгнуть()
    {
        if (количествоПрыжков < максимальноеКоличествоПрыжков)
        {
            float силаТекущегоПрыжка = силаПрыжка;

            if (количествоПрыжков == 1)
            {
                силаТекущегоПрыжка =
                    силаПрыжка * множительВторогоПрыжка;
            }

            физика.linearVelocity =
                new Vector2(0, силаТекущегоПрыжка);

            количествоПрыжков++;

            наЗемле = false;

            if (звукПрыжка != null)
            {
                звукПрыжка.Play();
            }

            if (количествоПрыжков == 2)
            {
                if (звукМяу != null)
                {
                    звукМяу.Play();
                }
            }

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
            bool былПрыжок = количествоПрыжков > 0;

            наЗемле = true;
            количествоПрыжков = 0;

            аниматор.SetBool("Прыгает", false);
            аниматор.SetBool("ВторойПрыжок", false);
            аниматор.SetBool("Бежит", true);

            if (былПрыжок)
            {
                if (частицыПриземления != null)
                {
                    ContactPoint2D точкаСтолкновения =
                        столкновение.GetContact(0);

                    частицыПриземления.transform.position =
                        точкаСтолкновения.point;

                    частицыПриземления.Play();
                }

                if (звукПриземления != null)
                {
                    звукПриземления.Play();
                }
            }
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

        if (звукУдара != null)
        {
            звукУдара.Play();
        }

        GameObject объектМузыки = GameObject.Find("Музыка");

        if (объектМузыки != null)
        {
            AudioSource музыка =
                объектМузыки.GetComponent<AudioSource>();

            if (музыка != null)
            {
                музыка.Stop();
            }
        }

        физика.linearVelocity = Vector2.zero;
        физика.simulated = false;

        аниматор.enabled = false;

        SpriteRenderer спрайтКотика =
            GetComponent<SpriteRenderer>();

        if (спрайтКотика != null)
        {
            спрайтКотика.enabled = false;
        }

        if (анимацияСмерти != null)
        {
            анимацияСмерти.transform.position =
                transform.position;

            анимацияСмерти.gameObject.SetActive(true);
            анимацияСмерти.НачатьПадение();
        }

        StartCoroutine(ПоказатьПроигрыш());
    }

    IEnumerator ПоказатьПроигрыш()
    {
        yield return new WaitForSecondsRealtime(1.5f);

        менеджерОкончания.ПоказатьМеню();
    }
}