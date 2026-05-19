using System.Collections;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    private static float height = 5.75f;
    private static float width = 10.5f;

    private static Vector3 combatPosition = new Vector3(0f, 4f, -10f);

    private Camera cam;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GetComponent<Camera>();

        cam.orthographicSize = Mathf.Max(height, width / cam.aspect);
    }

    public void EnterCombat ()
    {
        StartCoroutine("MoveToCombatPosition", transform.position);
    }

    IEnumerator MoveToCombatPosition(Vector3 startPos)
    {
        float duration = 1f;
        float time = 0f;

        while (true)
        {
            time += Time.deltaTime;
            if (time > duration)
            {
                transform.position = combatPosition;
                break;
            }

            transform.Translate((combatPosition - startPos) * Time.deltaTime);

            yield return null;
        }
    }
}
