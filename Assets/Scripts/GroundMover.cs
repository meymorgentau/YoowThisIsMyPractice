using UnityEngine;

public class GroundMover : MonoBehaviour
{
    [Header("Настройки движения")]
    public float скорость = 5f;

    private MeshRenderer рендер;
    private float смещениеX = 0f;

    void Start()
    {
        рендер = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        смещениеX = смещениеX + скорость * Time.deltaTime * 0.1f;
        рендер.material.mainTextureOffset = new Vector2(смещениеX, 0);
    }
}