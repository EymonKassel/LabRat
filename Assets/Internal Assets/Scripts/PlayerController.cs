using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private float _movementSpeed = 5f;

    private Vector2 _position;
    private Rigidbody2D _rb;

    private void Awake() {
        _rb = GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<Rigidbody2D>();
    }
    
    private void Update() {
        _position.x = Input.GetAxisRaw("Horizontal");
        _position.y = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate() {
        Move();
    }
    private void Move() {
        // Make better later
        _rb.velocity = _movementSpeed * Time.deltaTime * _position;
    }
}
