using UnityEngine;

public class ChaserSpiderAI : EnemyAI
{
    [Header("MeleeAttack")]
    [SerializeField] int damage = 1;
    [SerializeField] float meleeCooldown = 1;
    [SerializeField] float meleeRange = 3;
    [SerializeField] float meleeConeAngle = 45;

    private float meleeCooldownTimer = 0;
    private Vector3 startTargetLocation;

    void Update()
    {
        float distance;
        if (target != null && isDead == false)
        {
            distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance <= agent.stoppingDistance)
            {
                if (!hasAttacked)
                {
                    AkSoundEngine.PostEvent("arana_lanza_tela", gameObject);
                    animator.SetTrigger("Attacked");
                    hasAttacked = true;
                }
            }
            else
            {
                if (agent.isActiveAndEnabled)
                {
                    agent.SetDestination(target.transform.position);
                }
            }

            if (hasAttacked)
            {
                if (meleeCooldownTimer < meleeCooldown)
                {
                    meleeCooldownTimer += Time.deltaTime;
                }
                else
                {
                    hasAttacked = false;
                    meleeCooldownTimer = 0.0f;
                }
            }
        }
    }

    // ------------------------------------------

    public void Aim()
    {
        startTargetLocation = target.transform.position - transform.position;
    }

    public void BiteEvent()
    {

        float angle = Vector3.SignedAngle(startTargetLocation, target.transform.position - transform.position, Vector3.up);
        float distante = Vector3.Distance(transform.position, target.transform.position);

        if (angle < 0)
        {
            angle += 360;
        }

        if (angle <= meleeConeAngle && distante <= meleeRange)
        {
            IDamageable damageable = target.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }
    }
}
