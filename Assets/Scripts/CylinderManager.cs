using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderManager : MonoBehaviour
{
    public static CylinderManager instance = null;

    //[SerializeField] CylinederController[] cylinederControllers;

    Queue<GameObject> wallsQueue = new Queue<GameObject>();

    public Vector3 offset = new Vector3(0, 7.85f, 0);

    private Transform lastOneSpawned;
    private Transform playerTransform;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        for (int i = transform.childCount -1; i >= 0; i--)
        {
            wallsQueue.Enqueue(transform.GetChild(i).gameObject);
        }
    }

    private void Start()
    {
        lastOneSpawned = transform.GetChild(0);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (GameManager.instance.isPlaying)
        {
            if (Vector3.Distance( playerTransform.position , lastOneSpawned.position) < 8)
            {
                DequeueWallFromQueue();
            }
        }
    }


    private void RandomizeChilds()
    {
        List<GameObject> tempList = new List<GameObject>();
        //Debug.Log(wallsQueue.Count);
        for (int i = 0; i < wallsQueue.Count; i++)
        {
            tempList.Add(wallsQueue.Dequeue());
        }
        Debug.Log("List " + tempList.Count + ", Queue :" + wallsQueue.Count);
        wallsQueue.Clear();

        for (int i = 0; i < tempList.Count; i++)
        {
            GameObject temp = tempList[i];
            int randomIndex = Random.Range(i, tempList.Count);
            tempList[i] = tempList[randomIndex];
            tempList[randomIndex] = temp;
        }

        for (int i = 0; i < tempList.Count; i++)
        {
            wallsQueue.Enqueue(tempList[i]);
        }
        //Debug.Log("List2 " + tempList.Count + ", Queue2 :" + wallsQueue.Count);
        tempList.Clear();
    }

    public void DequeueWallFromQueue()
    {
        GameObject obj = wallsQueue.Dequeue();

        obj.transform.localPosition = lastOneSpawned.localPosition + offset;
        obj.transform.Rotate(lastOneSpawned.localRotation.eulerAngles - new Vector3(0, 45f, 0));
        obj.SetActive(true);
        lastOneSpawned = obj.transform;
        obj.GetComponent<CylinederController>().Recylce();

        wallsQueue.Enqueue(obj);
    }
}
