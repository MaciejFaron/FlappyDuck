using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocity = 1.7f;
    private PlayerSpawner spawner;
    private Rigidbody2D rigidbody;
    private Collider2D collider;
    public bool dieOnHity;
    
    void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("space")) {
            Vector2 _velocity = Vector2.up * velocity;
            rigidbody.velocity = _velocity;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        string collidedWithName = collision.collider.name;
        string collidedOtherName = collision.otherCollider.name;
        // Debug.Log($"Collision {collidedWithName}, {collidedOtherName}");
        if (collision.gameObject.CompareTag("Obstacle")) {
            if(dieOnHity) {
                playerDeath();
            }
            Obstacle obstacleHit = collision.gameObject.GetComponentInParent<Obstacle>();
            if (obstacleHit != null) {
                obstacleHit.kickBack(rigidbody);
            }
        }
    }

    void OnTriggerExit2D(Collider2D trigger) {
        if (trigger.gameObject.CompareTag("SafeZone")) {
            playerDeath();
        }
    }

    public void setSpawner(PlayerSpawner spawner) {
        Debug.Log("Player.setSpawner");
        this.spawner = spawner;
    }

    public void revive() {
        collider.enabled = true;
        rigidbody.simulated = true;
    }

    public void kill() {
        collider.enabled = false;
        rigidbody.simulated = false;
    }

    public void playerDeath() {
        kill();
        spawner.despawnPlayer();
    }

}
