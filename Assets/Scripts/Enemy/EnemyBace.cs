using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyBace : RecycleObject
{
    public float maxHP = 100.0f;
    public float moveSpeed = 3.0f;     // �ٱ� 5 �ȱ� 3
    public float runIncrease = 2.0f;
    public float attackDamege = 25.0f;
    public float maxCoolTime = 1.5f;
    public float findDistanceRange = 0.6f;
    public float alatWaitTime = 5.0f;

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
                if (State != EnemyState.Dead && hp <= 0)    // �ѹ��� �ױ�뵵
                {
                    Die();
                }
                hp = Mathf.Clamp(hp, 0, maxHP * nightAmpl);
            }
        }
    }

    Vector3 lookRotation = Vector3.zero;

    float currentMoveSpeed = 3.0f;
    float CurrentMoveSpeed 
    {
        get => currentMoveSpeed;
        set
        {
            currentMoveSpeed = value;
            agent.speed = currentMoveSpeed;
        }
    }

    float cooltime = 0;
    float chaseTime = 0.0f;
    float chaseAmpl = 1.0f;     //n
    float nightAmpl = 1.0f;
    bool IsAlive => hp > 0;

    Animator animator;
    Transform modle;
    NavMeshAgent agent;
    Transform target;
    Player player;

    protected enum EnemyState 
    {
        Wait = 0,   //���
        Alert,      //���
        Chase,      //�߰�
        Wander,     //��ȸ
        Attack,     //����
        Dead        //���
    }
    EnemyState state = EnemyState.Alert;
    protected EnemyState State 
    {
        get => state;
        set 
        {
            if (state != value)
            {
                state = value;
                switch (state)
                {
                    case EnemyState.Wait:
                        agent.isStopped = true;         // agent ����
                        agent.velocity = Vector3.zero;  // agent�� �����ִ� ��� ����
                        animator.SetBool(Move_Hash, false);
                        animator.SetBool(Run_Hash, false);
                        onStateUpdate = Update_Wait;
                        break;
                    case EnemyState.Alert:
                        agent.isStopped = false;
                        animator.SetBool(Run_Hash, false);
                        animator.SetBool(Move_Hash, true);
                        CurrentMoveSpeed = moveSpeed;
                        onStateUpdate = Update_Alert;
                        break;
                    case EnemyState.Chase:
                        agent.isStopped = false;
                        animator.SetBool(Move_Hash, false);
                        animator.SetBool(Run_Hash, true);
                        CurrentMoveSpeed += runIncrease;
                        onStateUpdate = Update_Chase;
                        break;
                    case EnemyState.Wander:
                        agent.isStopped = false;
                        animator.SetBool(Run_Hash, false);
                        animator.SetBool(Move_Hash, true);
                        onStateUpdate = Update_Wander;
                        break;
                    case EnemyState.Attack:
                        agent.isStopped = false;
                        animator.SetBool(Run_Hash, false);
                        animator.SetBool(Move_Hash, true);
                        CurrentMoveSpeed = moveSpeed;
                        onStateUpdate = Update_Attack;
                        break;
                    case EnemyState.Dead:
                        agent.isStopped = true;
                        agent.velocity = Vector3.zero;
                        animator.SetBool(Move_Hash, false);
                        animator.SetBool(Run_Hash, false);
                        animator.SetBool(Die_Hash, true);
                        onStateUpdate = Update_Dead;
                        break;
                }
            }
        }
    }
    Action onStateUpdate;

    readonly int Die_Hash = Animator.StringToHash("Die");
    readonly int Run_Hash = Animator.StringToHash("Run");
    readonly int Move_Hash = Animator.StringToHash("Move");
    readonly int Attack_Hash = Animator.StringToHash("Attack");

    const float MaxChaseAmpl = 5.0f;

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
        State = EnemyState.Wait;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    public void Start()
    {
        GameManager.Instance.TimeSystem.onNightchange += OnNight;
        player = GameManager.Instance.Player;
        CurrentMoveSpeed = moveSpeed;  
    }

    private void Update()
    {
        onStateUpdate();
    }
    void Update_Wait()
    {
        
    }
    
    void Update_Alert()
    {
        if (agent.remainingDistance < agent.stoppingDistance) 
        {
            StartCoroutine(AlatWait());
        }
    }

    void Update_Chase()
    {
        chaseTime += Time.deltaTime;
        Trace();//�Ÿ����� ���, ���� �������� ���� -> ����
    }

    void Update_Wander()
    {
        //n�ʰ� ��ȸ�� �����·�
    }

    void Update_Attack()
    {
        cooltime -= Time.deltaTime;
        Trace();
        if (agent.remainingDistance > agent.stoppingDistance)
        {
            CurrentMoveSpeed = moveSpeed;
        }
        else 
        {
            CurrentMoveSpeed = 0.0f;
        }
        if (cooltime < 0)
        {
            Attack();
        }
    }

    void Update_Dead()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (State == EnemyState.Chase) 
            {
                State = EnemyState.Attack;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            State = EnemyState.Chase;
        }
    }

    public void Demage(float demage) 
    {
        if (IsAlive) 
        {
            Hp -= demage;
        }
    }

    public void Die() 
    {
        target = null;
        State = EnemyState.Dead;
        ItemDrop();
        GameManager.Instance.TimeSystem.onNightchange -= OnNight;
        gameObject.SetActive(false);
    }

    protected virtual void Attack() 
    {
        player.Damege(attackDamege);
        animator.SetTrigger(Attack_Hash);
        cooltime = maxCoolTime;
    }

    protected virtual void Trace() 
    {
        agent.SetDestination(target.position);
        lookRotation.x = target.position.x;
        lookRotation.y = transform.position.y;
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
                //���丮 ����
            }
        }
    }

    public void OnDetect(Transform targeting, float radi)       //�Ÿ��� 0�� �������� n�� Ŀ����
    {
        Vector3 dir = targeting.position - transform.position;
        float findDistance = radi * findDistanceRange;          
        chaseAmpl += MaxChaseAmpl - (dir.sqrMagnitude);         //�ִ� 5 �ּ� 1
        if ((dir.sqrMagnitude > findDistance * findDistance) && (State != EnemyState.Alert || State != EnemyState.Chase))
        {
            State = EnemyState.Alert;
            agent.SetDestination(targeting.position);
        }
        else 
        {
            State = EnemyState.Chase;
            target = targeting;             //�����ð� �ʱ�ȭ
            chaseTime = 0.0f;
        }
    }

    public void OnDetect(Vector3 targetPos)
    {
        State = EnemyState.Alert;
        agent.SetDestination(targetPos);
    }

    void OnNight(float Ampl) //�ð��ٲ� ��������Ʈ ������
    {
        nightAmpl = Ampl;
    }

    IEnumerator AlatWait() 
    {
        yield return new WaitForSeconds(alatWaitTime);
        if (target == null) 
        {
            State = EnemyState.Wait;
        }
        else 
        {
            State = EnemyState.Wander;
            target = null;                              //�������� Ÿ���� ���� �ٲ���
        }
    }
}
