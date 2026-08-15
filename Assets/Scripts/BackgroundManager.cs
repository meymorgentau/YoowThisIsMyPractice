using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [Header("Фоны")]
    public Sprite фонДень;
    public Sprite фонЗакат;
    public Sprite фонНочь;
    public Sprite фонРассвет;

    [Header("Настройки времени")]
    public float длительностьЭтапа = 20f;

    private SpriteRenderer рендер;
    private float таймер = 0f;
    private int текущийЭтап = 0;

    void Start()
    {
        рендер = GetComponent<SpriteRenderer>();

        рендер.sprite = фонДень;
    }

    void Update()
    {
        таймер = таймер + Time.deltaTime;

        if (таймер >= длительностьЭтапа)
        {
            таймер = 0f;
            текущийЭтап = текущийЭтап + 1;

            if (текущийЭтап > 3)
            {
                текущийЭтап = 0;
            }

            СменитьФон();
        }
    }

    void СменитьФон()
    {
        if (текущийЭтап == 0)
        {
            рендер.sprite = фонДень;
        }
        else if (текущийЭтап == 1)
        {
            рендер.sprite = фонЗакат;
        }
        else if (текущийЭтап == 2)
        {
            рендер.sprite = фонНочь;
        }
        else if (текущийЭтап == 3)
        {
            рендер.sprite = фонРассвет;
        }
    }
}   