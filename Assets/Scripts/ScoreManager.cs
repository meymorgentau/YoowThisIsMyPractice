using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text текстСчёта;

    private float счёт = 0f;

    void Update()
    {
        счёт = счёт + Time.deltaTime;
        текстСчёта.text = "Счёт: " + Mathf.FloorToInt(счёт);
    }
}
