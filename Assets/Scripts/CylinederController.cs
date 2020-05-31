using UnityEngine;

public class CylinederController : MonoBehaviour
{
    [SerializeField] float speed = 1;

    [SerializeField] float distanceToRecycle = 15;
    [SerializeField] Platform[] platforms;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.isPlaying)
        {
            transform.Rotate(Vector3.up * Time.fixedDeltaTime * speed);
           
        }
    }

    private void Recycle()
    {
        
    }
}
