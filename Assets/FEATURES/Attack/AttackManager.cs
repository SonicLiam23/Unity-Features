using System.Collections;
using UnityEngine;

public class AttackManager : MonoBehaviour
{

    [SerializeField] float attackDuration = 0.2f;
    [SerializeField] GameObject attackObj;
    Collider2D attackTrigger;
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
    }

    // Start is called before the first frame update
    void Start()
    {
        attackObj.SetActive(false);
    }

    public void Attack(Direction dir)
    {
        Attacking = true;
        attackObj.transform.parent = attackPositions[(int)dir];
        attackObj.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        attackObj.transform.localScale = Vector3.one;

        attackObj.SetActive(true);
        // re-enable to allow "TriggerEnter" to be run
        attackTrigger.enabled = true;
        StartCoroutine(StopAttack());
    }

    IEnumerator StopAttack()
    {
        yield return new WaitForSeconds(attackDuration);
        attackObj.SetActive(false);
        attackTrigger.enabled = false;
        Attacking = false;
    }
}
