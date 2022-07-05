using UnityEngine;
using System.Collections;

public class BombExplosion : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float power = 10f;
    public float radius = 10f;
    public GameObject explosionEffect;

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

        StartCoroutine(Explosion());
    }

    IEnumerator Explosion()
    {
        Instantiate(explosionEffect, transform.position + new Vector3(0, 0.15f, 0), Quaternion.identity);
        SoundManager.instance.Play("BombExplosion");

        var colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                var p = hit.GetComponent<BaseEnemy>();
               
                if (p != null)
                {
                    yield return new WaitForSeconds(0.2f);
                    if (Vector3.Distance(rb.position, transform.position) < radius / 2f) 
                    {
                        p.OnHitBomb(gameObject);
                    }
                    else
                        p.ApplyPhisics();
                }
                rb.AddExplosionForce(power, transform.position, radius);

                var bomb = rb.GetComponent<BombExplosion>();
                if (bomb && Vector3.Distance(rb.position, transform.position) < radius / 2f)
                {
                    yield return new WaitForSeconds(0.2f);
                    bomb.Detonate();
                }
            }

        }
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius/2f);
    }
}

