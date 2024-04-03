using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBace : RecycleObject
{
    public float maxHP = 100.0f;
    public float moveSpeed = 10.0f;
    public float attackDamege = 25.0f;
    public float maxCoolTime = 1.5f;

    [System.Serializable]
    public struct ItemDropInfo
    {
        public ItemCode code;
        [Range(0, 1)]
        public float dropRatio;
        public uint dropCount;
    }
    public ItemDropInfo[] dropItems;

    float hp = 100.0f;
    float Hp 
    {
        get => hp;
        set 
        {
            if (hp != value) 
            {
                hp = value;
                hp = Mathf.Clamp(hp, 0, maxHP * nightAmpl);
                if (hp < 0.1) 
                {
                    Die();
                }
            }
        }
    }
    Vector3 lookRotation = Vector3.zero;
    float cooltime = 0;
    bool isNight = false;
    float nightAmpl = 1.0f;
    bool isTrace = false;

    Animator animator;
    Transform modle;
    NavMeshAgent agent;
    Transform? target;
    Player player;

    readonly int Die_Hash = Animator.StringToHash("Die");
    readonly int Run_Hash = Animator.StringToHash("Run");
    readonly int Attack_Hash = Animator.StringToHash("Attack");

    private void Awake()
    {
        modle = transform.GetChild(0);
        animator = modle.GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        OnInitialize();
    }

    protected virtual void OnInitialize() 
    {
        animator.SetBool(Die_Hash, false);
        Hp = maxHP * nightAmpl;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    public void Start()
    {
        player = GameManager.Instance.Player;
        agent.speed = moveSpeed;    
    }

    private void Update()
    {
        cooltime -= Time.deltaTime;
        if (isNight) 
        {
            nightAmpl = 1 + (Mathf.FloorToInt(Time.deltaTime)/60);
        }
        if (isTrace) 
        {
            Trace();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isTrace) 
        {
            if (other.CompareTag("Player"))
            {
                Attack();
            }
        }
    }

    public void Demage(int demage) 
    {
        Hp -= demage;
    }

    public void Die() 
    {
        target = null;
        animator.SetBool(Run_Hash, false);
        animator.SetBool(Die_Hash, true);
        ItemDrop();
        gameObject.SetActive(false);
    }

    protected virtual void Attack() 
    {
        if (cooltime < 0) 
        {
            player.Damege(attackDamege);
            Debug.Log(attackDamege);
            animator.SetTrigger(Attack_Hash);
            cooltime = maxCoolTime;
        }
    }

    protected virtual void Trace() 
    {
        agent.SetDestination(target.position);
        lookRotation.x = target.position.x;
        lookRotation.z = target.position.z;
        transform.LookAt(lookRotation);
    }

    void ItemDrop() 
    {
        foreach (var item in dropItems) 
        {
            if (item.dropRatio > Random.value) 
            {
                uint count = (uint)Random.Range(0, item.dropCount) + 1;
                //팩토리 생산
            }
        }
    }

    public void OnDetect(Transform targeting, bool on = true) //감지 델리게이트=이쪽에서 연결 섬광맞으면 타겟 풀리게할거임
    {
        target = targeting;
        isTrace = on;
        animator.SetBool(Run_Hash, on);
    }

    void OnNight(bool Night) //시간바뀔때 델리게이트 받을곳
    {
        isNight = Night;
        if (!Night)
        {
            nightAmpl = 1.0f;
        }
    }
}
