using UnityEngine;

public class UI_ArrowSettings : MonoBehaviour
{
    public Transform arrow;

    public void SetArrowPosition(int posY)
    {
        AkSoundEngine.PostEvent("menu_click_otro", gameObject);

        arrow.transform.localPosition = new Vector2(arrow.localPosition.x, posY);
    }
}
