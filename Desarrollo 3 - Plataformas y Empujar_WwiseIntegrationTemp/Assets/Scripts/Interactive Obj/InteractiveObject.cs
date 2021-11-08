using UnityEngine;

abstract class InteractiveObject : MonoBehaviour
{
    public enum StateObject
    {
        Closed,
        Available,
        Open
    };

    [Header("Interactive Obj")]
    [SerializeField] protected StateObject stateObject;
    [SerializeField] protected GameObject UIposter;

    //========================================

    protected Animator animator;
    protected bool inRange = false;

    //========================================

    private void Start()
    {
        UIposter.SetActive(false);

        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (stateObject == StateObject.Available)
        {
            UIposter.SetActive(true);

            inRange = true;
        }
    }

    public virtual void Update()
    {
        if (stateObject == StateObject.Available && inRange == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                stateObject = StateObject.Open;
                UIposter.SetActive(false);
                animator.SetTrigger("Open");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (stateObject == StateObject.Available)
        {
            UIposter.SetActive(false);

            inRange = false;
        }
    }
}
