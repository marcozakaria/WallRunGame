using System.Collections.Generic;
using UnityEngine;

public class MyObjectPool : MonoBehaviour
{
    [System.Serializable]
    public struct Pool
    {
        public int indexTag;
        //public string tag;
        public GameObject prefap;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<int, Queue<GameObject>> poolDictionary;

    public static MyObjectPool instance = null;

    public bool creatingObjectsDone = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        // DontDestroyOnLoad(gameObject);

        MakePools();
    }

    private void MakePools()
    {
        poolDictionary = new Dictionary<int, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefap);
                obj.SetActive(false);
                obj.transform.parent = this.transform;
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.indexTag, objectPool);
        }

        creatingObjectsDone = true;
    }

    public void ResetPool()
    {
        foreach (Queue<GameObject> queue in poolDictionary.Values)
        {
            for (int i = 0; i < queue.Count; i++)
            {
                GameObject obj = queue.Dequeue();
                obj.SetActive(false);
                obj.transform.parent = this.transform;
                queue.Enqueue(obj);
            }
        }

        // Debug.Log(poolDictionary.Values.Count);
    }

    public void SpawnFromPool(int id, Vector3 position)
    {
        GameObject objectToSpawn = poolDictionary[id].Dequeue();
        //Debug.Log("Spawn");
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        //objectToSpawn.transform.parent = obsteclesHolder;

        poolDictionary[id].Enqueue(objectToSpawn);

        //return objectToSpawn.transform;
    }

    public void ChangeSpriteFromPool(int id,int spriteID)
    {
        for (int i = 0; i < poolDictionary[id].Count; i++)
        {
            GameObject objectToSpawn = poolDictionary[id].Dequeue();
            //objectToSpawn.GetComponent<SpriteRenderer>().sprite = shopManager.visualItems[spriteID].snakeSprite;
            poolDictionary[id].Enqueue(objectToSpawn);
        }
        
    }

    public void SpawnParticleFromPool(int id , Vector3 position)
    {
        GameObject objectToSpawn = poolDictionary[id].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.GetComponent<ParticleSystem>().Play();
            
        poolDictionary[id].Enqueue(objectToSpawn);
    }

    public GameObject ReturnFromPool(int id)
    {
        GameObject objectToSpawn = poolDictionary[id].Dequeue();

        objectToSpawn.SetActive(true);
        //objectToSpawn.transform.parent = obsteclesHolder;

        poolDictionary[id].Enqueue(objectToSpawn);
        return objectToSpawn;
    }

    /*public void SpawnParticleWithColor(string tag, Vector3 position, Color color)
    {
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;      

        poolDictionary[tag].Enqueue(objectToSpawn);
    }*/
}

