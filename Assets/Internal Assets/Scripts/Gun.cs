using System;
using UnityEngine;

public class Gun : MonoBehaviour {
    [SerializeField]
    private Transform _gunAnchor;
    [SerializeField]
    private Transform _firePoint;
    [SerializeField]
    private GameObject _basicBulletPrefab;
    [SerializeField]
    private float _basicBulletForce = 20f;

    [SerializeField]
    private ShootingType _currentShootingType;

    [SerializeField] float cooldown = 0.5f;
    float lastShotTime = float.MinValue;

    AudioManager _audioManager;

    [SerializeField] private float defDistanceRay = 100;
    public LineRenderer m_lineRenderer;
    public LayerMask lineIgnoreLayer;

    private void Awake() {
        _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    private void Update() {
        RotateGunAroundAnchor();

        if (Input.GetMouseButton(0) && Time.time - lastShotTime > cooldown) 
        {
            Shoot();
        }
        else
        {
            Draw2DRay(_firePoint.position, _firePoint.position);
        }
    }

    private void Shoot() {

        switch ( _currentShootingType ) {
            case ShootingType.Basic:
                GameObject basicBullet = Instantiate(_basicBulletPrefab, _firePoint.position, _firePoint.rotation);
                Rigidbody2D basicBulletRB = basicBullet.GetComponentInChildren<Rigidbody2D>();
                basicBulletRB.AddForce(_firePoint.right * _basicBulletForce, ForceMode2D.Impulse);
                _audioManager.PlaySFX(_audioManager.PlayerBasicShoot);
                lastShotTime = Time.time;
                break;
            case ShootingType.Ricochet:

                break;
            case ShootingType.Laser:
                RaycastHit2D _hit = Physics2D.Raycast(_firePoint.position, transform.right, Mathf.Infinity, ~lineIgnoreLayer);
                if (_hit)
                {
                    Draw2DRay(_firePoint.position, _hit.point);
                }
                else
                {
                    Draw2DRay(_firePoint.position, _firePoint.transform.right * defDistanceRay);
                }
                break;
            case ShootingType.Splash:

                break;
            default:
                Debug.LogError("Unknown shooting type");
                break;
        }
    }

    private void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        m_lineRenderer.SetPosition(0, startPos);
        m_lineRenderer.SetPosition(1, endPos);
    }

    private void RotateGunAroundAnchor() {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _gunAnchor.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        _gunAnchor.rotation = rotation;
    }
}
