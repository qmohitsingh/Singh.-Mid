using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float moveSpeed, lifeTime;
    public Rigidbody theRB;


    public GameManager gameManager;

    void Start() {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        theRB.velocity = transform.forward * moveSpeed;
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);

            gameManager.aliensCounter = gameManager.aliensCounter - 1;
        }

        if (gameManager.aliensCounter <=0 ) {
            gameManager.GameOver();
        }
    }

}
