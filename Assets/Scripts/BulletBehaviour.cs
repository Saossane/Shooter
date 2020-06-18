﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D myRigidbody;

    
    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        var playerGameObject = GameObject.FindWithTag("Player");
        var playerBehaviour = playerGameObject.GetComponent<PlayerBehaviour>();
        playerBehaviour.onDeath.AddListener(Die);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        myRigidbody.velocity = new Vector3
        {
            x = speed * Time.fixedDeltaTime,
            y = 0,
            z = 0
        };
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
