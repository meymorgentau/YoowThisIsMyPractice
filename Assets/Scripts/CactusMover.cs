using UnityEngine;

public class CactusMover : MonoBehaviour
{
    public float скорость = 10f;

    void Update()
    {
        float новаяX = transform.position.x - скорость * Time.deltaTime;
        transform.position = new Vector3(новаяX, transform.position.y, 0);
        
        if (transform.position.x < -12f)
        {
            Destroy(gameObject);
        }
    }
}