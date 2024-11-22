using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(ColorChanger))]
[RequireComponent(typeof(ExplosionHandler))]

public class Cube : MonoBehaviour
{
    [SerializeField] private Cube cubePrefab;

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
        float divisionSplitChance = 2.0f;

        int newCubesCount = Random.Range(minCountCubes, maxCountCubes);
        Vector3 originalPosition = transform.position;
        Vector3 originalScale = transform.localScale;

        Debug.Log($"Split Chance: {_splitChance}");

        for (int i = 0; i < newCubesCount; i++)
        {
            Cube newCube = InitializeCube(originalPosition, originalScale);

            ColorChanger colorChanger = newCube.GetComponent<ColorChanger>();

            if (colorChanger != null)
            {
                colorChanger.ChangeColor();
            }

            ExplosionHandler explosionHandler = newCube.GetComponent<ExplosionHandler>();

            if (explosionHandler != null)
            {
                explosionHandler.ApplyExplosion(originalPosition);
            }

            newCube._splitChance = _splitChance / divisionSplitChance;
        }

        Destroy(gameObject);
    }

    private Cube InitializeCube(Vector3 originalPosition, Vector3 originalScale)
    {
        float biasVector = 0.5f;
        int biasScale = 2;

        Vector3 newPosition = originalPosition + Random.insideUnitSphere * biasVector;
        Cube newCube = Instantiate(cubePrefab, newPosition, Random.rotation);

        newCube.transform.localScale = originalScale / biasScale;

        return newCube;
    }
}
