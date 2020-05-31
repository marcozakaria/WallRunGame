using UnityEngine;

public enum PlatformState
{
    empty,coin,obstacle,startingPoint
}

public class Platform : MonoBehaviour
{
    [SerializeField] PlatformState state;
    [Range(0f,1f)]
    [SerializeField] float coinToTrapPercent = 0.75f;
    [SerializeField] Transform spawnPoint;

    private void Start()
    {
        RefreshPlatform();
    }

    public void RefreshPlatform()
    {
        if (state == PlatformState.coin || state == PlatformState.obstacle)
        {
            state = Random.Range(0f, 1f) > coinToTrapPercent ? PlatformState.obstacle : PlatformState.coin;
        }

        switch (state)
        {
            case PlatformState.empty:
                break;
            case PlatformState.coin:
                MyObjectPool.instance.SpawnFromPool(0, spawnPoint.position, spawnPoint);
                break;
            case PlatformState.obstacle:
                MyObjectPool.instance.SpawnFromPool(1, spawnPoint.position, spawnPoint);
                break;
            case PlatformState.startingPoint:
                break;
            default:
                break;
        }
    }
}
