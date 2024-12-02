using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsMenu : MonoBehaviour {
    [SerializeField] private Button backButton;
    private void OnEnable() {
        backButton.Select();
    }
}
