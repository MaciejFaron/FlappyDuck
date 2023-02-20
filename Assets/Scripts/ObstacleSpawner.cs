using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private float time = 0f;
    public float spawnTimer = 1.5f;
    public float height;
    public GameObject obstacle;
    public bool allowSpawn = false;
    public long obstacleCount = 0;
    public GameManager gameManager;
    public float pushForce = 1f;
    // Start is called before the first frame update
    void Start()
    {
        // obstacleCount = 0;
        // allowSpawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (allowSpawn & time > spawnTimer)
        {
            spawnObstacle();
            time = 0f;
        }
        time += Time.deltaTime * gameManager.gameSpeed;
    }

    private void spawnObstacle()
    {
        GameObject newObstacle = createNewGameObstacle();
        setObstacleParams(newObstacle);
        gameManager.onObstacleSpawn(newObstacle);
        obstacleCount += 1;
        Destroy(newObstacle, 10);
    }

    private GameObject createNewGameObstacle()
    {
        Vector3 heightOffset = new Vector3(0, Random.Range(-height, height), 0);
        Vector3 obstaclePosition = transform.position + heightOffset;
        GameObject newObstacle = Instantiate(obstacle, obstaclePosition, Quaternion.identity);
        return newObstacle;
    }

    private void setObstacleParams(GameObject newObstacle)
    {
        Obstacle obstacle = newObstacle.GetComponent<Obstacle>();
        obstacle.setGameManager(gameManager); // TODO(mf) for updating game speed, change to event
        obstacle.pushForce = pushForce;
    }
}
