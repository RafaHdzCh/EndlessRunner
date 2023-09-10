using System;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private readonly float speed = 10f;
    private readonly float leftBound = -15;
    private PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        if (playerController.gameOver == false)
        {
            transform.Translate(Vector3.left * (Time.fixedDeltaTime * speed));
        }

        if (transform.position.x < leftBound && transform.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
