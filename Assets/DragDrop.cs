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
    private int rotateDir = 1;
    private BoxCollider2D collider;
    private ContactFilter2D shipFilter = new ContactFilter2D();

    //Animation additions:
    [SerializeField] private int shipLength = 1;
    public int ShipLength => shipLength;

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
        collider = GetComponent<BoxCollider2D>();
        shipFilter.SetLayerMask(LayerMask.GetMask("Ship", "GridBorder"));
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
            if (isValid()) feedback?.ShowValid();

            transform.position = WorldPos + offset;

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
        if (timer - begin < 0.3f)
        {
            RotateShip();
        }
    }

    public void RotateShip()
    {
        Vector3 pos = transform.position;
        transform.Rotate(0, 0, 90 * rotateDir);
        Physics2D.SyncTransforms(); // Utan denna rad använder isValid nedan den tidigare rotationen.

        if (!isValid())
        {
            // Testar att rotera till vänster eftersom höger inte fungerade
            transform.Rotate(0, 0, -180);
            Physics2D.SyncTransforms();
            if (!isValid())
            {
                transform.Rotate(0, 0, 90);
                transform.position = pos;
            }
            else
            {
                rotateDir = -rotateDir;
            }
        }
    }

    public bool isValid() //ny metod för att true false om ship är i grid
    {


        if (collider.Overlap(shipFilter, new Collider2D[1]) == 0)
        {
            return true;
        }
        

        //for (int i = 0; i < ship.shapePoints; i++)
        //{
        //    Vector3 pos = transform.GetChild(i).position;
        //    if (pos.x < 0.5f || pos.x > 8.5f || pos.y < 0.5f || pos.y > 8.5f)
        //    { 
        //        return false;
        //    }
        //}

        return false;
    }

    private void CheckIfDestroy() { 
  
        if (isValid())
        {
            transform.position = Snap(transform.position);
            GameObject.Find("GameplaySystem").GetComponent<GamePlay>().CheckAllShipsPlaced();
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
