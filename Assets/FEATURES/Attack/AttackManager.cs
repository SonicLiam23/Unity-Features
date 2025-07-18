using System.Collections;
using UnityEngine;

public enum AttackType
{ 
    ATTACK, PARRY
}


public class AttackManager : MonoBehaviour
{

    
    [SerializeField] GameObject attackObj;
    [SerializeField] float attackDuration = 0.2f;
    [SerializeField] GameObject parryObj;
    [SerializeField] float parryDuration;
    Collider2D attackTrigger;
    Collider2D parryTrigger;

    GameObject currAttack;
    float duration;
    public bool Attacking { get; private set; } = false;
    [Header("In order of UP, DOWN, LEFT, RIGHT.")]
    [SerializeField] Transform[] attackPositions = new Transform[4];

    private void OnValidate()
    {
        if (attackPositions.Length != 4)
        {
            attackPositions = new Transform[4];
        }
    }

    private void Awake()
    {
        attackObj = Instantiate(attackObj, transform);
        attackTrigger = attackObj.GetComponent<Collider2D>();

        parryObj = Instantiate(parryObj, transform);
        parryTrigger = parryObj.GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        attackObj.SetActive(false);
        parryObj.SetActive(false);
    }

    public void ChangeAttackType(AttackType type)
    {
        switch (type)
        {
            case AttackType.ATTACK:
                currAttack = attackObj;
                duration = attackDuration;
                break;

            case AttackType.PARRY:
                currAttack = parryObj;
                duration = parryDuration;
                break;
        }
    }

    public void Attack(Direction dir)
    {
        Attacking = true;
        currAttack.transform.parent = attackPositions[(int)dir];
        currAttack.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        currAttack.transform.localScale = Vector3.one;

        currAttack.SetActive(true);
        // re-enable to allow "TriggerEnter" to be run
        attackTrigger.enabled = true;
        StartCoroutine(StopAttack());
    }

    IEnumerator StopAttack()
    {
        yield return new WaitForSeconds(duration);
        currAttack.SetActive(false);
        attackTrigger.enabled = false;
        Attacking = false;
    }
}
