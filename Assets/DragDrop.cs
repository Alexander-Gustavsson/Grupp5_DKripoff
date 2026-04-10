using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragDrop : MonoBehaviour
{
    [SerializeField] private InputAction press, screenPos;

    [SerializeField] private ArrayList shape;
    // * * *
    // *   *
    
    private Vector3 startPos;
    private Camera mainCamera;
    private bool dragging;
    //Animation additions:
    private ShipPlacementFeedback feedback;

    private Vector3 WorldPos
    {
        get
        {
            Vector3 pos = mainCamera.ScreenToWorldPoint(screenPos.ReadValue<Vector2>());
            pos.z = transform.position.z;
            return pos;
    
        }
    }
   /* private bool isClickedOn
    {
        get
        {
            Collider2D hit = Physics2D.OverlapPoint(WorldPos);
            return hit != null && hit.transform == transform;
        }
    }*/
    private void Awake()
    {
     mainCamera = Camera.main;
     screenPos.Enable();
     press.Enable();
     press.canceled += Active;

        //Animation additions:
        feedback = GetComponent<ShipPlacementFeedback>();
    }

    private void Active(InputAction.CallbackContext context)
    {
        if (dragging)
        {
            dragging = false;
            CheckIfDestroy();
        }
    }

    private void OnDestroy()
    {
        press.canceled -= Active;
    }

    public void StartDragging()
    {
        startPos = transform.position;

        //Animation additions:
        //if (feedback != null)
        //    feedback.ShowSelected();

        StartCoroutine(Drag());
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

            //animation additions:
            bool insideOfGrid =
            transform.position.x >= -0.5f && transform.position.x <= 8.5f
            && transform.position.y >= -0.5f && transform.position.y <= 8.5f;

            if (feedback != null)
            {
                if (insideOfGrid)
                    feedback.ShowValid();
                else
                    feedback.ShowInvalid();
            }

            yield return null;
        }
       
    }


    private void CheckIfDestroy() { 
    float distanceMove = Vector3.Distance(startPos, transform.position);

        bool insideOfGrid =
            transform.position.x >= -0.5f && transform.position.x <= 8.5f
            && transform.position.y >= -0.5f && transform.position.y <= 8.5f;
        /*
        if (distanceMove < 0.1f)
        {
            Destroy(gameObject);
            return;
        }
        */

        if (insideOfGrid)
        {
            transform.position = Snap(transform.position);
        }

        // om vi vill att den ska tas sönder om den släpps utanför griden
        /* else
         {
             Destroy(gameObject);
         }
        */


        //animation additions:
        if (feedback != null)
            feedback.ResetFeedback();
    }


    //snapping
    private Vector3 Snap(Vector3 pos)
    {
        float x = Mathf.Round(pos.x);
        float y = Mathf.Round(pos.y);

        return new Vector3(x, y, pos.z);
    }
}
