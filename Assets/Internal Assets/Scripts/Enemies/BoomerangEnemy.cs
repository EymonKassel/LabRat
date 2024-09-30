using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoomerangEnemy : ShootingEnemy {

    protected virtual IEnumerator Attack() {
        //_animator.SetBool("IsAttacking", true);

        GameObject bulletPrefab = Instantiate(BulletPrefab, transform.position, transform.rotation);
        Rigidbody2D bulletPrefabRB = bulletPrefab.GetComponent<Rigidbody2D>();
        
        // To do

        yield return new WaitForSeconds(_cooldown);
        //_animator.SetBool("IsAttacking", false);
    }
}
