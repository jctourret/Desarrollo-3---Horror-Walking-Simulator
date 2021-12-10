using UnityEngine;

public class NeedleBehaviour : MonoBehaviour
{
    public int damage = 1;

    [SerializeField] float rotationSpeed = 0.1f;
    [SerializeField] float destroyTime = 6f;

    private Rigidbody rig;

    //==================================

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();

        Destroy(this.gameObject, destroyTime);
    }

    private void Update()
    {
        if(rig.isKinematic == false)
        {
            float rotX = rig.velocity.y * rotationSpeed * Time.deltaTime;

            this.transform.Rotate(rotX, 0, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        this.transform.parent = collision.transform;
        BoxCollider needleCol = transform.GetComponent<BoxCollider>();
        needleCol.enabled = false;

        rig.isKinematic = true;

        IDamageable damaged = collision.transform.GetComponentInChildren<IDamageable>();

        if (damaged != null)
        {
            damaged.TakeDamage(damage);
        }
    }
}
