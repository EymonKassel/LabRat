using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Manager {
    [SerializeField]
    private GameObject _uiSettingsPanel;

    private void Update() {
        if ( Input.GetKeyDown(KeyCode.Escape) ) {
            OpenUISettingsPanel();
        }
    }
    private void OpenUISettingsPanel() {
        if ( _uiSettingsPanel.activeInHierarchy ) {
            _uiSettingsPanel.SetActive(false);
            Time.timeScale = 1f;
        } else {
            _uiSettingsPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
