using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private Transform[] layers;
    [SerializeField] private float[] coeff;

    private int LayersCount;

    void Start()
    {
        LayersCount = layers.Length;
    }
    void Update()
    {
        for (int i = 0; i < LayersCount; i++)
        {
            layers[i].position = transform.position * coeff[i];
        }
    }
}
