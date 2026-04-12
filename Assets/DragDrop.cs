using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragDrop : MonoBehaviour
{
    [SerializeField] private InputAction press, screenPos;

    [SerializeField] private ArrayList shape;
    // * * *
    // *   *
    private ShipShape ship;
    private Vector3 startPos;
    private Camera mainCamera;
    public bool dragging;
    private float timer;
    private float begin;

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

        ship = GetComponent<ShipShape>();

        //anim
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
        timer = 0;
        begin = Time.time;
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
            timer = Time.time;
            //dragging the game objekt
            transform.position = WorldPos + offset;


            //animation additions:
            bool insideOfGrid = isValid();

            if (feedback != null)
            {
                if (insideOfGrid)
                    feedback.ShowValid();
                else
                    feedback.ShowInvalid();
            }

            yield return null;
        }
        if (timer - begin < 0.5f)
        {
            RotateShip();
        }
    }

    public void RotateShip()
    {
        Vector3 pos = transform.position;
        transform.Rotate(0, 0, 90);
        if (!isValid())
        {
            transform.Rotate(0, 0, -90);
            transform.position = pos;
        }
    }

    private bool isValid() //ny metod för att true false om ship är i grid
    {
        for (int i = 0; i < ship.shapePoints; i++)
        {
            Vector3 pos = transform.GetChild(i).position;
            if (pos.x < 0.5f || pos.x > 8.5f || pos.y < 0.5f || pos.y > 8.5f)
            {
                return false;
            }
        }
        return true;
    }

    private void CheckIfDestroy() { 
  
        if (isValid())
        {
            transform.position = Snap(transform.position);
        }

        else
        {
            transform.position = startPos;
        }



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
