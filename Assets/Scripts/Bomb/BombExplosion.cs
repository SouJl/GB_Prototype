using UnityEngine;
using System.Collections;

public class BombExplosion : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float power = 10f;
    public float radius = 10f;
    public float damage = 20f;

    private new AudioSource audio;

    private void Awake()
    {
        audio = gameObject.GetComponent<AudioSource>();
        StartCoroutine(Explosion());
    }

    IEnumerator Explosion()
    {
        //SoundManager.instance.Play("BombExplosion");
        //audio.Play();
        yield return new WaitForSeconds(0.5f);
        
        var colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                var p = hit.GetComponent<BaseEnemy>();

                if (p != null)
                {
                    if (Vector3.Distance(rb.position, transform.position) < radius / 2f) 
                    {
                        p.OnHitBomb(damage);
                    }
                    else
                        p.ApplyPhisics();
                }
                rb.AddExplosionForce(power, transform.position, radius);

                var bomb = rb.GetComponent<BombController>();
                if (bomb && Vector3.Distance(rb.position, transform.position) < radius / 2f)
                {
                    yield return new WaitForSeconds(0.2f);
                    bomb.BombDestroy();
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

