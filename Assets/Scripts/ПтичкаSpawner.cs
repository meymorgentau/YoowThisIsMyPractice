using UnityEngine;

public class ПтичкаSpawner : MonoBehaviour
{
    [Header("Настройки спавна")]
    public GameObject префабПтички;

    public float минимальныйИнтервал = 3f;
    public float максимальныйИнтервал = 6f;

    public float высотаПтички = 1f;

    [Header("Настройки усложнения")]
    public float скоростьДвижения = 10f;
    public float ускорение = 0.1f;

    private float таймер;

    private void Start()
    {
        таймер = Random.Range(минимальныйИнтервал, максимальныйИнтервал);
    }

    private void Update()
    {
        таймер -= Time.deltaTime;

        if (таймер <= 0f)
        {
            СоздатьПтичку();

            таймер = Random.Range(минимальныйИнтервал, максимальныйИнтервал);
        }

        скоростьДвижения += ускорение * Time.deltaTime;
    }

    private void СоздатьПтичку()
    {
        Vector3 позиция = new Vector3(12f, высотаПтички, 0f);

        GameObject новаяПтичка = Instantiate(
            префабПтички,
            позиция,
            Quaternion.identity
        );

        ПтичкаMover движение = новаяПтичка.GetComponent<ПтичкаMover>();

        if (движение != null)
        {
            движение.скоростьДвижения = скоростьДвижения;
        }
    }
}