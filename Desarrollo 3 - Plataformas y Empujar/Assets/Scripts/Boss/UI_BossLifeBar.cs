using UnityEngine;
using UnityEngine.UI;

public class UI_BossLifeBar : MonoBehaviour
{
    [SerializeField] private Image lifeBar;

    private void OnEnable()
    {
        BossStats.SetLifeBar += SetLifeBar;
    }

    private void OnDisable()
    {
        BossStats.SetLifeBar -= SetLifeBar;
    }
    
    public void SetLifeBar(int max, int actual)
    {
        lifeBar.fillAmount = (float)actual / (float)max;
    }
}
