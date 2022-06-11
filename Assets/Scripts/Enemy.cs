using UnityEngine;


public class Enemy : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float speed = 1f;


    void Start()
    {

    }

    
    void Update()
    {
        if (EnemySpawner.IsPlayerIn) 
        {
            Rigidbody playerPos = GameManager.instance.PlayerPostion;
            transform.position = Vector3.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, playerPos.position, 20 * Time.deltaTime, 0f));
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Player") 
        {
            GameManager.instance.Restart();
        }
    }
}
