using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager gameManager;
    public long gamePoints { get; private set; }
    public bool shouldCountPoints;
    private float nextStep;
    public float pointTickDelay;
    void Start()
    {
        nextStep = 0.0f;
        gamePoints = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldCountPoints && Time.time > nextStep) {
            onPointTick(gamePoints++);
            nextStep += pointTickDelay / gameManager.gameSpeed;
        }
    }

    private void onPointTick(float points) {
        //TODO(mf) redesign to publisher/subscriber with events,
        // i.e. https://www.youtube.com/watch?v=k4JlFxPcqlg
        Debug.Log($"You've scored points");
    }

    public void setGameManager(GameManager gameManager) {
        this.gameManager = gameManager;
    }

    public bool recordPlayerPoints() {
        Debug.Log($"Player has achieved {gamePoints} points!");
        return true;
    }

    public void resetPoints() {
        gamePoints = 0;
    }

}
