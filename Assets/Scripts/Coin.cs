using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float speed = 50;
    [SerializeField] int value = 5;

    private void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * speed, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.Play("coin");
            GameManager.instance.CoinCollected(value);
            MyObjectPool.instance.SpawnParticleFromPool(4, transform.position);

            gameObject.SetActive(false);
        }
    }
}
