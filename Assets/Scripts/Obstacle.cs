using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed;
    private GameManager gameManager;
    public float pushForce = 1f;
    private Vector2 pushBack;

    // Start is called before the first frame update
    void Start()
    {
        pushBack = (Vector2.left + Vector2.up);
        pushBack.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += ((Vector3.left * speed * gameManager.gameSpeed) * Time.deltaTime);
    }

    public void kickBack(Rigidbody2D rigidbody) {
        rigidbody.AddForce(pushBack * pushForce, ForceMode2D.Impulse);
    }

    public void setGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
