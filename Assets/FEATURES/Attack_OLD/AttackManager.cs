using System.Collections;
using UnityEngine;

public enum AttackType
{ 
    ATTACK, PARRY
}


public class AttackManager : MonoBehaviour
{

    
    [SerializeField] GameObject attackObj;
    [SerializeField] GameObject parryObj;
    [SerializeField] float parryDuration;
    Collider2D attackTrigger;
    Collider2D parryTrigger;

    GameObject currAttack;
    public Collider2D currTrigger;
    float attackDuration;
    float duration;
    public bool Attacking = false;
    bool flipAttack = false;

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

        Animator anim = attackObj.GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("No animator found on attack object.");
            return;
        } 

        attackDuration = anim.runtimeAnimatorController.animationClips[0].length;
        Debug.Log(duration);

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
                currTrigger = attackTrigger;
                duration = attackDuration;
                break;

            case AttackType.PARRY:
                currAttack = parryObj;
                currTrigger = parryTrigger;
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


        
        if (flipAttack)
        {        
            Vector3 currScale = currAttack.transform.localScale;
            currScale.y *= -1;
            currAttack.transform.localScale = currScale;
        }
        flipAttack = !flipAttack;


        currAttack.SetActive(true);
        // re-enable to allow "TriggerEnter" to be run
        currTrigger.enabled = true;
    }

}
