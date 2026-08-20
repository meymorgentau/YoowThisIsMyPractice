using UnityEngine;

public class BasketBallGoal : MonoBehaviour
{
    [SerializeField] private BasketBallScoreManager счётчик;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BasketBallController>() != null)
        {
            счётчик.ДобавитьПопадание();
        }
    }
}