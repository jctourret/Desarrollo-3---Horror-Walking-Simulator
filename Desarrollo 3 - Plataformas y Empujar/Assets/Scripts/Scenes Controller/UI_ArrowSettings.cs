using UnityEngine;

public class UI_ArrowSettings : MonoBehaviour
{
    public Transform arrow;

    public void SetArrowPosition(int posY)
    {
        arrow.transform.localPosition = new Vector2(arrow.localPosition.x, posY);
    }
}
