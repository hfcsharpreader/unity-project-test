using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneBallBehaviour : MonoBehaviour
{
    private Rigidbody rigidBody;
    private Vector3 forceAdded;
    public float Multiplier = 100f;
    public float MaxMultiplier = 3000f;

    private GameController gameController;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        gameController = GameObject.Find("Main Camera").GetComponent<GameController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Wait one second, then call the MoveMe method every 1.5seconds
        InvokeRepeating("MoveMe", 1f, 1.5f);
    }

    private void MoveMe()
    {
        forceAdded = new Vector3(Multiplier - Random.value * Multiplier * 2,
                       0, Multiplier - Random.value * Multiplier * 2);
        rigidBody.AddForce(forceAdded);
        Multiplier += 100f;
        if (Multiplier > MaxMultiplier) Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, forceAdded, Color.white);
        if (gameController.GameOver)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameController.CollidedWithBall();
            Destroy(gameObject);
        }
    }

}
