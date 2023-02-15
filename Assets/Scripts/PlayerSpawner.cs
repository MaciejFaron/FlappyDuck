using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Vector3 spawningPosition;
    public GameObject prefabReference;
    private GameManager gameManager;
    private GameObject playerInstance; 
    private Player playerController;
    public bool allowSpawn;

    // Start is called before the first frame update
    void Start() {}

    public void subscribeToSpawnAction(GameManager subscriber) {
        gameManager = subscriber;
    }

    private void onPlayerSpawn() {
        gameManager.onPlayerSpawn(playerInstance.GetComponent<Player>());
    }

    private void onPlayerDespawn() {
        gameManager.onPlayerDespawn();
    }

    private void spawnPlayer() {
        allowSpawn = false;
        if (playerInstance == null) {
            playerInstance = Instantiate(prefabReference);
            playerController = playerInstance.GetComponent<Player>();
            playerController.setSpawner(this);
        }
        playerInstance.transform.position = spawningPosition;
        playerInstance.transform.rotation = Quaternion.identity;
        playerInstance.SetActive(true);
        playerController.revive();
        onPlayerSpawn();
    }

    public void despawnPlayer() {
        if (playerInstance != null && playerInstance.activeSelf)
            playerInstance.SetActive(false);
        onPlayerDespawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (allowSpawn && Input.GetKeyDown(KeyCode.Space)) {
            spawnPlayer();
        }
    }
}
