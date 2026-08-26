using UnityEngine;

public class ПтичкаMover : MonoBehaviour
{
    public float скоростьДвижения = 10f;

    private void Update()
    {
        transform.Translate(Vector3.left * скоростьДвижения * Time.deltaTime);

        if (transform.position.x < -12f)
        {
            Destroy(gameObject);
        }
    }
}