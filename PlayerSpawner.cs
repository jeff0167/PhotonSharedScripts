using Fusion;
using UnityEngine;
public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;

    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            Runner.Spawn(PlayerPrefab, GetRandomPos(), Quaternion.identity);
        }
    }

    Vector3 GetRandomPos()
    {
        return new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
    }
}