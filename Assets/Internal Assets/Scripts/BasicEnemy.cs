using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour {
    [Header("Attributes")]
    private float _movementSpeed = 5f;
    
    public int Health = 1;


    //[Header("References")]
    private Rigidbody2D _rb;

    private void Awake() {
        _rb = GetComponentInChildren<Rigidbody2D>();
    }

    private void Update() {
        
    }


    private void Spawn() {

    }

    private void Patrol() {

    }

    private void Follow() {

    }
    private void Die() {

    }
}
