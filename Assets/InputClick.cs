using UnityEngine;
using UnityEngine.InputSystem;

public class InputClick : MonoBehaviour
{

    [SerializeField] private InputAction press, screenPos;
    [SerializeField] private GamePlay gamePlay;
    private ContactFilter2D clickable = new ContactFilter2D();

    public bool canDrag = false;
    public bool canClick = false;

    private void OnEnable()
    {
        clickable.SetLayerMask(LayerMask.GetMask("Ship", "Grid", "Guide")); // Om du behöver skapa ett nytt layer måste det ligga här för att vara klickbart. 
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

        Collider2D[] clicked = new Collider2D[1];

        Physics2D.OverlapPoint(worldPoint, clickable, clicked);

        Collider2D hit = clicked[0];

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
