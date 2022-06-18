using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset;
    
    Transform target;

    private void Start()
    {
        target = Main.instance.playerPosition;
    }

    private void LateUpdate()
    {
        if (target != null) 
        {
            transform.position = target.position + offset;
        }  
    }
}
