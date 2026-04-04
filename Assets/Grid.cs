using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Grid : MonoBehaviour
{
    [SerializeField] GameObject missSprite;

    List<Vector2> missedPos = new List<Vector2>();

    Vector2 inputPos;

    public void pressed(Vector2 pressPos)
    {
        findInputPos(pressPos);
        spawnMissSprite();
    }

    private void findInputPos(Vector2 pressPos)
    {
        for (int i = 0; i <= 8; i++)
        {
            for (int j = 0; j <= 8; j++)
            {
                if (pressPos.x > i - 0.5 && pressPos.x < i + 1.5 && pressPos.y > j - 0.5 && pressPos.y < j + 1.5)
                {
                    inputPos = new Vector2(i, j);
                }
            }
        }
    }

    private void spawnMissSprite()
    {
        if (missedPos.Contains(inputPos))
        {
            return;
        }
        missedPos.Add(inputPos);
        Instantiate(missSprite, missSprite.transform.position = inputPos, Quaternion.identity);
    }

}
