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
            if (HealtBarUIManager.instance.healthCount > 0)
            {
                HealtBarUIManager.instance.MinusHealth();
                // other.gameObject.GetComponent<Rigidbody>().AddForce(other.gameObject.transform.position - new Vector3(1f, 0, 1f), ForceMode.Impulse);
                Destroy(parent);
            }
            else
            {
                Destroy(other.gameObject);
                Main.instance.GameOver(false);
            }
        }
    }
}
