using UnityEngine;

public class Manager : MonoBehaviour {
    private void Awake() {
        gameObject.transform.parent = null;
        gameObject.transform.position = Vector3.zero;
        DontDestroyOnLoad(gameObject);
    }
}
