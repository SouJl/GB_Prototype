using UnityEngine;

public class EnemyObserver : MonoBehaviour
{
    public GameObject parent;

    private void Awake()
    {
        parent = transform.parent.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (HealthBarUIManager.instance.healthCount > 0)
            {
                HealthBarUIManager.instance.MinusHealth();
                Destroy(parent);
                Main.instance.EnemyCount--;
            }
            else
            {
                Destroy(other.gameObject);
                Main.instance.GameOver(false);
            }
        }
    }
}
