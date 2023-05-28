using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondSpawner : MonoBehaviour
{
    public GameObject diamondPrefab;
    public GameObject playerHurtBox;
    public Vector2[] spawnPositions;
    private bool canSpawnDiamond = true;

    private void Update()
    {
        if (canSpawnDiamond)
        {
            SpawnDiamond();
        }
    }

    private void SpawnDiamond()
    {
        int randomIndex = Random.Range(0, spawnPositions.Length);
        Vector2 randomPosition = spawnPositions[randomIndex];

        Instantiate(diamondPrefab, randomPosition, Quaternion.identity, this.transform);
        canSpawnDiamond = false;
        playerHurtBox.SetActive(true);
    }

    public void DiamondCollected()
    {
        canSpawnDiamond = true;
    }
}

