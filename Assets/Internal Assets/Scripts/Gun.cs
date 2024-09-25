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

    AudioManager _audioManager;
    float lastShotTime = float.MinValue;

    private void Awake() {
        _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    private void Update() {
        RotateGunAroundAnchor();

        if (Input.GetMouseButton(0) && Time.time - lastShotTime > cooldown) {
            Shoot();
        }
    }

    private void Shoot() {
        lastShotTime = Time.time;

        switch ( _currentShootingType ) {
            case ShootingType.Basic:
                GameObject basicBullet = Instantiate(_basicBulletPrefab, _firePoint.position, _firePoint.rotation);
                Rigidbody2D basicBulletRB = basicBullet.GetComponentInChildren<Rigidbody2D>();
                basicBulletRB.AddForce(_firePoint.right * _basicBulletForce, ForceMode2D.Impulse);
                _audioManager.PlaySFX(_audioManager.PlayerShoot);
                break;
            case ShootingType.Ricochet:

                break;
            case ShootingType.Laser:

                break;
            case ShootingType.Splash:

                break;
            default:
                Debug.Log("Unknown shooting type");
                break;
        }
    }

    // To change later
    private void RotateGunAroundAnchor() {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _gunAnchor.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        _gunAnchor.rotation = rotation;
    }
}
