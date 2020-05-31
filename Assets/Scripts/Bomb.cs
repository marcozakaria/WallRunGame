using UnityEngine;

public class Bomb : MonoBehaviour
{
    //[SerializeField] Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           // animator.SetTrigger("explode");
            MyObjectPool.instance.SpawnParticleFromPool(5, transform.position);
            MyObjectPool.instance.SpawnParticleFromPool(2, transform.position);
            GameManager.instance.OnGameOver();
            AudioManager.instance.Play("explosion");
            gameObject.SetActive(false);
        }
        
    }
}
