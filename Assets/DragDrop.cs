using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragDrop : MonoBehaviour
{
    [SerializeField] private InputAction press, screenPos;

    private Vector2 currentPos;
    private Camera mainCamera;
    private bool dragging;

    private Vector3 WorldPos
    {
        get
        {
            Vector3 pos = mainCamera.ScreenToWorldPoint(currentPos);
            pos.z = transform.position.z;
            return pos;
    
        }
    }
    private bool isClickedOn
    {
        get
        {
            Collider2D hit = Physics2D.OverlapPoint(WorldPos);
            return hit != null && hit.transform == transform;
        }
    }
    private void Awake()
    {
     mainCamera = Camera.main;
     screenPos.Enable();
     press.Enable();

        screenPos.performed += context => { currentPos = context.ReadValue<Vector2>(); };
        press.performed += _ => { if (isClickedOn) StartCoroutine(Drag()); };
        press.canceled += _ => { dragging = false; };



    }

    private IEnumerator Drag()
    {
        dragging = true;
        Vector3 offset = transform.position - WorldPos;
        //grabbing the game object
        while (dragging)
        {
            //dragging the game objekt
            transform.position = WorldPos + offset;
            yield return null;
        }
        //
    }
}
