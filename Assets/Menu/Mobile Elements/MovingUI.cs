using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MovingUI : MonoBehaviour
{
    [SerializeField] protected float MovementDuration;
    protected Camera cam;
    protected void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        transform.position = StartPos();
    }

    protected virtual Vector3 EndPos()
    {
        return Vector3.zero;
    }

    protected virtual Vector3 StartPos()
    {
        return Vector3.zero;
    }

    public IEnumerator Move()
    {
        Vector3 startPos = StartPos();
        Vector3 endPos = EndPos();

        float currentTime = 0f;

        Vector3 movementToDo = endPos - transform.position;


        while (currentTime < MovementDuration)
        {
            currentTime += Time.deltaTime;

            transform.position = startPos + movementToDo * (currentTime / MovementDuration);

            yield return null;
        }

        transform.position = endPos;
    }
}
