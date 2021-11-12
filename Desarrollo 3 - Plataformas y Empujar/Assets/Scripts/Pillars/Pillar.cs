using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    [SerializeField] Animator animator;

    [Header("Delay Animation")]
    [Range(0, 2)]
    [SerializeField] protected float delayTime = 1.5f;

    [Header("Diegetic Lights")]
    [SerializeField] protected List<GameObject> nightLights = new List<GameObject>();

    [Header("Destroy Timer")]
    [SerializeField] protected float waitTime = 30f;
    [SerializeField] protected float destroyTime = 5f;

    [Header("Pillar Scale")]
    [SerializeField] protected float pillarScale = 1f;

    //===============================================

    protected enum PillarState
    {
        MoveUp,
        waiting,
        MoveDown
    };
    protected PillarState pillarState;
    protected float timer = 0f;

    //===============================================

    //public virtual void Awake()
    //{
    //    animator = GetComponent<Animator>();
    //}

    public virtual void Start()
    {
        pillarState = PillarState.MoveUp;

        AkSoundEngine.PostEvent("aparece_torre", gameObject);
    }

    //===============================================

    public float GetPillarScale()
    {
        return pillarScale;
    }

    public virtual void StartCollapse()
    {
        StartCoroutine(WaitTime());
    }

    public virtual IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(delayTime);
        AkSoundEngine.PostEvent("alarma_caida_torre", gameObject);
        timer = destroyTime;
        pillarState = PillarState.waiting;
    }

    public virtual void UpdateTimer()
    {
        
    }

    public virtual IEnumerator MoveDownPillar()
    {
        animator.SetTrigger("Change");
        AkSoundEngine.PostEvent("desaparece_torre", gameObject);
        yield return new WaitForSeconds(delayTime);
        Destroy(this.gameObject);
    }

}
