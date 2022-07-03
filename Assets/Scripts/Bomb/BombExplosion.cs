using UnityEngine;
using UnityEngine.AI;

public class BombExplosion : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float power = 10f;
    public float radius = 10f;

    private bool exxpsionDone = false;

    private void FixedUpdate()
    {
        if (gameObject.GetComponent<BombController>().IsDetonate)
        {
            Detonate();
        }
    }

    private void Detonate()
    {
        if (exxpsionDone) return;
        exxpsionDone = true;

        var colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null) 
            {
                var p = hit.GetComponent<BaseEnemy>();
                if (p != null) p.ApplyPhisics();
                rb.AddExplosionForce(power, transform.position, radius);
                
                var bomb = rb.GetComponent<BombExplosion>();
                if (bomb && Vector3.Distance(rb.position, transform.position) < radius/ 2f ) bomb.Detonate();
            }

        }
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius/2f);
    }
}

