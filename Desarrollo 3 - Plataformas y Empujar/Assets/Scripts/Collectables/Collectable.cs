using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] protected Vector3 jumpDirection;
    [SerializeField] protected float jumpForce = 5f;

    [Range(-50, 0)]
    [SerializeField] protected float y_fallLimit = -30f;

    //===================================

    protected Rigidbody rig;

    //===================================

    public virtual void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    public virtual void Start()
    {
        rig.AddForce(jumpDirection * jumpForce, ForceMode.Force);
    }

    public virtual void Update()
    {
        DeleteAfterFall();
    }

    //=======================================

    void DeleteAfterFall()
    {
        if (this.transform.position.y < y_fallLimit)
        {
            Destroy(this.gameObject);
        }
    }
}
