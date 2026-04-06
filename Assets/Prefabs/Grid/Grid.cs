using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Grid : MonoBehaviour
{
    [SerializeField] GameObject missSprite;
    public enum InputState
    {
        FIRE,
        LAYOUT,
        DISABLED
    }
    public InputState state;
    private Vector2 size;
    private float tileScale;
    private Vector2 offset
    {
        get
        {
            return (Vector2) (transform.position + new Vector3(-size.x, size.y, 0)/2);
        }
    }

    List<Vector2> missedPos = new List<Vector2>();

    Vector2 inputPos;

    private void Start()
    {
        size = GetComponent<BoxCollider2D>().size;
        tileScale = size.x / 8;
    }

    public void pressed(Vector2 pressPos)
    {
        Vector2 input = findInputPos(pressPos);
        print("Coordinate: " + input);
        switch (state)
        {
            case InputState.FIRE:
                spawnMissSprite(input);
                break;
            default:
                break;
        }
        
    }

    private Vector2 findInputPos(Vector2 pressPos)
    {
        print(offset);
        Vector2 normalized = pressPos - offset;
        normalized /= tileScale;

        return new Vector2((float) Math.Floor(normalized.x), (float) Math.Floor(normalized.y));

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

    private void spawnMissSprite (Vector2 gridPosition)
    {
        if (missedPos.Contains(gridPosition))
        {
            return;
        }
        missedPos.Add(gridPosition);

        Instantiate(missSprite, (Vector3) (gridPosition + offset + new Vector2(1, 1) * tileScale/2), Quaternion.identity).transform.parent = transform;
    }
}
