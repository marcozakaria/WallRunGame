using UnityEngine;

public class CylinederController : MonoBehaviour
{

    [SerializeField] Platform[] platforms;

    public void Recylce()
    {
        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i].RefreshPlatform();
        }
    }
}
