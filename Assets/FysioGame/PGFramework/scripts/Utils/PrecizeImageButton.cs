using UnityEngine;
using UnityEngine.UI;

public class PrecizeImageButton : MonoBehaviour
{
    public float alphaHitTestMinimumThreshold = 0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Image img = GetComponent<Image>();
        img.alphaHitTestMinimumThreshold = alphaHitTestMinimumThreshold;   
    }
}
