using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int lastScore;
    public int health;

    public float[] playerPos;

    public PlayerData(PlayerHealth player)
    {
        lastScore = ScoreManager.score;
        health = player.CurrentHealth;

        playerPos = new float[3];
        playerPos[0] = player.transform.position.x;
        playerPos[1] = player.transform.position.y;
        playerPos[2] = player.transform.position.z;
    }

}
