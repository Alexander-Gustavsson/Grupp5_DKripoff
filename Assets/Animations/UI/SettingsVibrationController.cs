using UnityEngine;
using UnityEngine.UI;

public class SettingsVibrationController : MonoBehaviour
{
    [SerializeField] private Toggle vibrationToggle;

    private void Start()
    {
        if (vibrationToggle != null)
        {
            vibrationToggle.isOn = GameSettings.VibrationEnabled;
            vibrationToggle.onValueChanged.AddListener(SetVibrationEnabled);
        }
    }

    public void SetVibrationEnabled(bool value)
    {
        GameSettings.VibrationEnabled = value;
    }
}