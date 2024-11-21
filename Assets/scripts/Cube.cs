using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Cube : MonoBehaviour
{
    public GameObject cubePrefab;
    private float _splitChance = 1.0f;

    private void OnMouseDown()
    {
        if (Random.value <= _splitChance)
        {
            SplitCube();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SplitCube()
    {
        int minCountCubes = 2;
        int maxCountCubes = 7;

        int newCubesCount = Random.Range(minCountCubes, maxCountCubes);
        Vector3 originalPosition = transform.position;
        Vector3 originalScale = transform.localScale;

        Debug.Log(_splitChance);

        for (int i = 0; i < newCubesCount; i++)
        {
            GameObject newCube = InitializationCubes(originalPosition, originalScale);

            ChangeColor(newCube);

            Rigidbody rigidbody = newCube.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                ScatteringCubes(newCube, originalPosition, rigidbody);
            }

            Cube newCubeScript = newCube.GetComponent<Cube>();
            if (newCubeScript != null)
            {
                newCubeScript._splitChance = _splitChance / 2.0f;
            }
        }

        Destroy(gameObject);
    }

    private GameObject InitializationCubes(Vector3 originalPosition, Vector3 originalScale)
    {
        Vector3 newPosition = originalPosition + Random.insideUnitSphere * 0.5f;
        GameObject newCube = Instantiate(cubePrefab, newPosition, Random.rotation);

        newCube.transform.localScale = originalScale / 2;

        return newCube;
    }

    private void ChangeColor(GameObject newCube)
    {
        Renderer renderer = newCube.GetComponent<Renderer>();
        if (renderer != null)
        {
            Color randomColor = new Color(Random.value, Random.value, Random.value);
            renderer.material.color = randomColor;
        }
    }

    private void ScatteringCubes(GameObject newCube, Vector3 originalPosition, Rigidbody rigidbody)
    {
        float explosionForce = 300.0f;
        float explosionRadius = 5.0f;

        Vector3 explosionDirection = (newCube.transform.position - originalPosition).normalized;
        rigidbody.AddExplosionForce(explosionForce, originalPosition, explosionRadius);
    }
}