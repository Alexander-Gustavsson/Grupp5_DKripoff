using UnityEngine;

public class ToggleVisualCorrectorUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<UnityEngine.UI.Toggle>().isOn = BoardShake.shakeOn;
    }
}
