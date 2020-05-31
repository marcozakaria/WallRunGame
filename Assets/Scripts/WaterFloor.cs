using UnityEngine;

public class WaterFloor : MonoBehaviour
{
    [SerializeField] float speed = 0.05f;
    [SerializeField] AudioClip water;

    private void Update()
    {
        if (GameManager.instance.isPlaying)
        {
            transform.position += Vector3.up * Time.deltaTime * speed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.PlaySingle(water);
            MyObjectPool.instance.SpawnParticleFromPool(3, collision.contacts[0].point);
            MyObjectPool.instance.SpawnParticleFromPool(2, collision.contacts[0].point);
            GameManager.instance.OnGameOver();
        }
    }
}
