using UnityEngine;
using UnityEngine.InputSystem;

public class InputClick : MonoBehaviour
{

    [SerializeField] private InputAction press, screenPos;
    [SerializeField] private GamePlay gamePlay;

    public bool canDrag = false;
    public bool canClick = false;

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
/*
            if (hit.CompareTag("Grid"))
            {
                print("GRID");
                gridScipt.pressed(new Vector2(worldPoint.x, worldPoint.y));
            }
            else if (hit.gameObject == gameObject)
            {
                Destroy(gameObject);
            }*/

            DragDrop ship = hit.GetComponent<DragDrop>();

            if (ship != null && canDrag) 
            {
                ship.StartDragging();
            }

            else if (hit.CompareTag("AIGrid") && canClick)
            {
                gamePlay.AIGridPressed(new Vector2(worldPoint.x, worldPoint.y));
            }
        }
    }
}
