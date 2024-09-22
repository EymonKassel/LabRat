using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    [SerializeField]
    private Transform _gunAchor;

    private void Update() {
        RotateGunAroundAnchor();
    }

    // To change later
    private void RotateGunAroundAnchor() {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _gunAchor.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        _gunAchor.rotation = rotation;
    }
}
