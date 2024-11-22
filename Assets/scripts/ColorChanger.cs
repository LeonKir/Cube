using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private void ChangeColor()
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            Color randomColor = new Color(Random.value, Random.value, Random.value);
            renderer.material.color = randomColor;
        }
        else
        {
            Debug.LogWarning("Renderer not found on object: " + gameObject.name);
        }
    }
}
