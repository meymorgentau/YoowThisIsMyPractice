using UnityEngine;

public class CactusSpawner : MonoBehaviour
{
    [Header("Настройки спавна")]
    public GameObject префабКактуса;

    public float минимальныйИнтервал = 0.2f;
    public float максимальныйИнтервал = 1f;

    public float скоростьДвижения = 10f;

    [Header("Настройки усложнения")]
    public float ускорение = 0.5f;
    public float интервалУскорения = 5f;
    public float максимальнаяСкорость = 20f;

    private float таймер = 0f;
    private float интервалСпавна;
    private float таймерУскорения = 0f;

    void Start()
    {
        интервалСпавна = Random.Range(минимальныйИнтервал, максимальныйИнтервал);
    }

    void Update()
    {
        таймер = таймер + Time.deltaTime;

        if (таймер >= интервалСпавна)
        {
            таймер = 0f;
            интервалСпавна = Random.Range(минимальныйИнтервал, максимальныйИнтервал);
            СоздатьКактус();
        }

        таймерУскорения = таймерУскорения + Time.deltaTime;

        if (таймерУскорения >= интервалУскорения)
        {
            таймерУскорения = 0f;
            скоростьДвижения = скоростьДвижения + ускорение;

            if (скоростьДвижения > максимальнаяСкорость)
            {
                скоростьДвижения = максимальнаяСкорость;
            }
        }
    }

    void СоздатьКактус()
    {
        Vector3 позицияСпавна = new Vector3(12f, -1.6632f, 0);
        GameObject новыйКактус = Instantiate(
            префабКактуса,
            позицияСпавна,
            Quaternion.identity
        );

        if (новыйКактус.GetComponent<CactusMover>() == null)
        {
            новыйКактус.AddComponent<CactusMover>();
        }

        CactusMover движок = новыйКактус.GetComponent<CactusMover>();
        движок.скорость = скоростьДвижения;
    }
}