using System.Collections;
using UnityEngine;

public class MovingUI : MonoBehaviour
{
    private void Start()
    {
        transform.position = StartPos();
    }

    protected virtual Vector3 StartPos()
    {
        return Vector3.zero;
    }

    public IEnumerator Move()
    {
        yield return null;
    }
}
