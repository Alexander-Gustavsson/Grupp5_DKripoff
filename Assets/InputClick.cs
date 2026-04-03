using UnityEngine;
using UnityEngine.InputSystem;

public class InputClick : MonoBehaviour
{

    [SerializeField] private InputAction press, screenPos;

    private void OnEnable()
    {
        press.Enable();        
        screenPos.Enable();
        press.performed += IsClicked;
    }


    private void OnDisable()
    {
        press.Disable();
        screenPos.Disable();
        press.performed -= IsClicked;
    }

    private void IsClicked(InputAction.CallbackContext context)
    {
        Vector2 mousePosition = screenPos.ReadValue<Vector2>();
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPoint.z = 0;

        Collider2D hit = Physics2D.OverlapPoint(worldPoint);

        if (hit != null)
        {
            if (hit.gameObject == gameObject)
            {
                Destroy(gameObject);
            }
        }
    }
}
