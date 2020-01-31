using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -2;

    // Hierdoor kunnen de prefabs particles krijgen
    public ParticleSystem explosionParticle;

    // Hierdoor kunnen de prefabs een point value krijgen
    public int pointValue;

    // Start is called before the first frame update
    void Start()
    {
        // Dit maakt de referenties naar de components
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        targetRb.AddForce(Randomforce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPos();
    }


    // Dit zorgt ervoor dat als je op het object klikt hij verwijdert wordt, particles komen en dat er punten worden opgetelt
    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }

    // Dit zorgt ervoor dat als het object aangeraakt wordt hij verdwijnt
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad 1"))
        {
            gameManager.GameOver();
        }
    }

    // Dit is het script voor de snelheid
    Vector3 Randomforce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    // dit is het script voor de torque
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    // Dit is het script voor de random spawns
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}