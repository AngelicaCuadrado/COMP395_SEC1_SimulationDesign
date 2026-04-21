using UnityEngine;

public class LevelPositionManager : MonoBehaviour
{
    public static LevelPositionManager instance;

    public Transform[] playerSpawns;
    public Transform[] cameraPositions;

    private void Awake()
    {
        instance = this;
    }

    public Transform GetPlayerSpawn(int level)
    {
        return playerSpawns[Mathf.Clamp(level - 1, 0, playerSpawns.Length - 1)];
    }

    public Transform GetCameraPosition(int level)
    {
        return cameraPositions[Mathf.Clamp(level - 1, 0, cameraPositions.Length - 1)];
    }
}
