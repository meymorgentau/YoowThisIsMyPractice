using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    [Header("Настройки падения")]
    public float начальнаяСкорость = 2f;
    public float ускорение = 15f;
    public float поворот = 180f;

    [Header("Настройки подпрыгивания")]
    public float силаПодпрыгивания = 6f;
    public float времяВВоздухе = 0.15f;

    private float скоростьПадения;
    private bool падает = false;
    private bool подпрыгивает = false;
    private float таймер = 0f;

    public void НачатьПадение()
    {
        падает = false;
        подпрыгивает = true;

        скоростьПадения = силаПодпрыгивания;
        таймер = 0f;
    }

    private void Update()
    {
        if (подпрыгивает)
        {
            таймер += Time.unscaledDeltaTime;

            скоростьПадения -= 18f * Time.unscaledDeltaTime;

            transform.position += Vector3.up * скоростьПадения * Time.unscaledDeltaTime;
            transform.Rotate(0f, 0f, поворот * Time.unscaledDeltaTime);

            if (таймер >= времяВВоздухе)
            {
                подпрыгивает = false;
                падает = true;
                скоростьПадения = начальнаяСкорость;
            }

            return;
        }

        if (падает)
        {
            скоростьПадения += ускорение * Time.unscaledDeltaTime;

            transform.position += Vector3.down * скоростьПадения * Time.unscaledDeltaTime;
            transform.Rotate(0f, 0f, поворот * Time.unscaledDeltaTime);

            if (transform.position.y < -10f)
            {
                gameObject.SetActive(false);
            }
        }
    }
}