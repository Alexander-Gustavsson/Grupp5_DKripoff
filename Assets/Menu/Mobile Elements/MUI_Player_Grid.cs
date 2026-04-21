using UnityEngine;

public class MUI_Player_Grid : MovingUI
{
    protected override Vector3 StartPos()
    {
        return new Vector3(0, cam.orthographicSize - 0.5f, 0);
    }

    protected override Vector3 EndPos()
    {
        return new Vector3(5f, cam.orthographicSize, 0);
    }
}