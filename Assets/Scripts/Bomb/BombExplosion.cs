using UnityEngine;
using UnityEngine.AI;

public class BombExplosion : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float power = 10f;
    public float radius = 10f;

    private void FixedUpdate()
    {
        if (gameObject.GetComponent<BombController>().IsDetonate)
        {
            Detonate();
        }
    }

    private void Detonate()
    {
        var colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null) 
            {
                rb.AddExplosionForce(power, transform.position, radius);
            }
               
        }
        Destroy(gameObject);
    }
}
