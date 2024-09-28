using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LaserEnemy : ShootingEnemy
{
    [SerializeField] private float defDistanceRay = 100;
    [SerializeField] private float duration = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] LayerMask lineIgnoreLayer;
    private float timer;
    private float radian;

    protected override void GetDirectionToPlayer()
    {
        Direction = Quaternion.Euler(0, 0, radian) * (PlayerPosition.position - transform.position).normalized;
        radian += Time.deltaTime * rotationSpeed;
    }

    protected override void Shoot()    {
        
        RaycastHit2D _hit = Physics2D.Raycast(transform.position, Direction, Mathf.Infinity, ~lineIgnoreLayer);
        if (_hit)
        {
            Draw2DRay(transform.position, _hit.point);
            if(_hit.collider.gameObject.GetComponent<PlayerController>() != null)
            {
                print("����� �����");
            }
        }
        else
            Draw2DRay(transform.position, transform.transform.right * defDistanceRay);
        if(timer < duration)
            timer += Time.deltaTime;
        else
        {
            timer = 0;
            lastShotTime = Time.time;
            Draw2DRay(transform.position, transform.position);
        }
    }

    protected override void Follow()
    {
        base.Follow();
        Draw2DRay(transform.position, transform.position);
    }

    private void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
}
