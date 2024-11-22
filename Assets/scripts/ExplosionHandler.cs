using UnityEngine;

public class ExplosionHandler : MonoBehaviour
{
    [SerializeField] private float explosionForce = 300.0f;
    [SerializeField] private float explosionRadius = 5.0f;

    private void ApplyExplosion(Vector3 origin)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (rigidbody != null)
        {
            Vector3 explosionDirection = (transform.position - origin).normalized;
            rigidbody.AddExplosionForce(explosionForce, origin, explosionRadius);
        }
        else
        {
            Debug.LogWarning("Rigidbody not found on object: " + gameObject.name);
        }
    }
}
