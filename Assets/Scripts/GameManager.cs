using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// This class controls whole game and acts as a bridge between other components.
    /// </summary>
    public ObstacleSpawner obstacleSpawner;
    public PlayerSpawner playerSpawner;
    public PointsManager pointsManager;
    public float gameSpeed = 1.0f;
    public uint obstacleDifficultyStep = 3;
    private uint obstacleDifficultyThreshold;
    public bool playerDieOnHit; // if player dies afer colliding with obstacle

    // Start is called before the first frame update
    void Start()
    {
        // obstacleSpawner.subscribeToSpawnAction(this);
        playerSpawner.subscribeToSpawnAction(this);
        playerSpawner.allowSpawn = true;
        pointsManager.setGameManager(this);  // TODO(mf): unify the interface
        obstacleDifficultyThreshold = obstacleDifficultyStep;
        Debug.Log("Press SPACE to start the game");
    }

    public void onObstacleSpawn(GameObject newObstacle) { 
        if(obstacleSpawner.obstacleCount > obstacleDifficultyThreshold) {
            gameSpeed += 0.4f;
            obstacleDifficultyThreshold += obstacleDifficultyStep;
        }
    }

    //called when new player character is spawned
    public void onPlayerSpawn(Player playerInstance) {
        Debug.Log("Press SPACE to keep alive!");
        Debug.Log("obstacleSpawner.allowSpawn:: Spawner allowed to spawn obstacles");
        playerInstance.dieOnHity = playerDieOnHit;
        obstacleSpawner.allowSpawn = true;
        pointsManager.shouldCountPoints = true;
    }

    public void onPlayerDespawn() {
        resetObstacleSpawn();
        handleGamePoints();
        playerSpawner.allowSpawn = true;
        gameSpeed = 1.0f;
        obstacleDifficultyThreshold = obstacleDifficultyStep;
        Debug.Log("Press SPACE to start the game");
    }

    private void resetObstacleSpawn() {
        obstacleSpawner.allowSpawn = false;
        obstacleSpawner.obstacleCount = 0;
    }
    
    private void handleGamePoints() {
        pointsManager.shouldCountPoints = false;
        if(pointsManager.recordPlayerPoints()) {
            pointsManager.resetPoints();
        } else {
            Debug.LogError("ERROR: Player points has not been recorded");
        }
    }

    void Update() {}
}
