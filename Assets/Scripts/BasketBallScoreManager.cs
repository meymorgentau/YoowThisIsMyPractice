using TMPro;
using UnityEngine;

public class BasketBallScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text текстСчёта;

    private int попадания = 0;

    public void ДобавитьПопадание()
    {
        попадания++;
        текстСчёта.text = "Попадания: " + попадания;
    }
}