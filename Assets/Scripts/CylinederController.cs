using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinederController : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] GameObject cylinders;

    private void Update()
    {
        if (GameManager.instance.isPlaying)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * speed);
        }
    }
}
