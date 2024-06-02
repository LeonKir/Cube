using UnityEngine;

public class Cube : MonoBehaviour
{
    public GameObject cubePrefab;
    public float splitChance = 1.0f;

    void OnMouseDown()
    {
        if (Random.value <= splitChance)
        {
            SplitCube();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void SplitCube()
    {
        int newCubesCount = Random.Range(2, 7);
        Vector3 originalPosition = transform.position;
        Vector3 originalScale = transform.localScale;

        Debug.Log(splitChance);

        for (int i = 0; i < newCubesCount; i++)
        {
            Vector3 newPosition = originalPosition + Random.insideUnitSphere * 0.5f;
            GameObject newCube = Instantiate(cubePrefab, newPosition, Random.rotation);

            newCube.transform.localScale = originalScale / 2;

            Color randomColor = new Color(Random.value, Random.value, Random.value);
            newCube.GetComponent<Renderer>().material.color = randomColor;

            Rigidbody rb = newCube.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = newCube.AddComponent<Rigidbody>();
            }

            Vector3 explosionDirection = (newCube.transform.position - originalPosition).normalized;
            rb.AddExplosionForce(300.0f, originalPosition, 5.0f);

            Cube newCubeScript = newCube.GetComponent<Cube>();
            newCubeScript.splitChance = splitChance / 2.0f;
        }

        Destroy(gameObject);
    }
}