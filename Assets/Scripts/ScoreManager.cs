using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text текстСчёта;
    public TMP_Text текстРекорда;

    private float счёт = 0f;
    private int рекорд = 0;

    void Start()
    {
        рекорд = PlayerPrefs.GetInt("Рекорд", 0);

        текстРекорда.text = "Рекорд: " + рекорд;
    }

    void Update()
    {
        счёт = счёт + Time.deltaTime;

        int текущийСчёт = Mathf.FloorToInt(счёт);

        текстСчёта.text = "Счёт: " + текущийСчёт;

        if (текущийСчёт > рекорд)
        {
            рекорд = текущийСчёт;

            текстРекорда.text = "Рекорд: " + рекорд;

            PlayerPrefs.SetInt("Рекорд", рекорд);
            PlayerPrefs.Save();
        }
    }
}