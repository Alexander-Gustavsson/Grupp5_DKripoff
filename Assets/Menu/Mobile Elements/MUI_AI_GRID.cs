using UnityEngine;

public class MUI_AI_GRID : MovingUI
{
    protected override Vector3 StartPos()
    {
        return new Vector3(-cam.orthographicSize * cam.aspect, cam.orthographicSize + 0.5f, 0f);
    }

    protected override Vector3 EndPos()
    {
        return new Vector3(-5f, cam.orthographicSize, 0f);
    }
}
