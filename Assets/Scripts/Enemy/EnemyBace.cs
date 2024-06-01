using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyBace : RecycleObject
{
    public float maxHP = 100.0f;
    public float moveSpeed = 3.0f;     // �ٱ� 5 �ȱ� 3
    public float runIncrease = 2.0f;
    public float attackDamege = 10.0f;
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
                if (State != EnemyState.Dead && hp < 0.1)    // �ѹ��� �ױ�뵵
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
    int wanderCount = 0;
    float nightAmpl = 1.0f;
    bool IsAlive => hp > 0;

    Animator animator;
    Transform modle;
    NavMeshAgent agent;
    Transform target;
    Vector3 wanderDestination;

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
                        wanderCount = 0;
                        onStateUpdate = Update_Wander;
                        break;
                    case EnemyState.Attack:
                        agent.isStopped = true;
                        agent.velocity = Vector3.zero;
                        animator.SetBool(Run_Hash, false);
                        animator.SetBool(Move_Hash, false);
                        CurrentMoveSpeed = 0.0f;
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
    const float BaceChaseTime = 7.0f;
    const float BaceChaseDistance = 10.0f;
    const int BaceWanderCount = 3;
    const float WanderRange = 30.0f;

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
        if (GameManager.Instance.TimeSystem != null) 
        {
            GameManager.Instance.TimeSystem.onNightchange += OnNight;
        }
    }

    protected virtual void OnInitialize() 
    {
        animator.SetBool(Die_Hash, false);
        Hp = maxHP * nightAmpl;
        CurrentMoveSpeed = moveSpeed;
        State = EnemyState.Wait;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    public void Start()
    {
        CurrentMoveSpeed = moveSpeed;
        GameManager.Instance.TimeSystem.onNightchange += OnNight;
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
            State = EnemyState.Wait;
        }
    }

    void Update_Chase()
    {
        chaseTime += Time.deltaTime;
        Trace();
        if ((chaseTime > chaseAmpl *BaceChaseTime) || agent.remainingDistance > BaceChaseDistance * chaseAmpl) 
        {
            State = EnemyState.Alert;
        }
    }

    void Update_Wander()
    {
        if (wanderCount < chaseAmpl * BaceWanderCount)
        {
            Wandering();
        }
        else if ((wanderCount >= chaseAmpl * BaceWanderCount) && (agent.remainingDistance <= agent.stoppingDistance)) 
        {
            State = EnemyState.Wait;
        }
    }

    void Update_Attack()
    {
        cooltime -= Time.deltaTime;
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
            target = other.transform;
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
        Player player = target.GetComponent<Player>();
        if (player != null)
        {
            player.Damege(attackDamege);
            animator.SetTrigger(Attack_Hash);
            cooltime = maxCoolTime;
            Debug.Log(attackDamege);
        }
    }

    protected virtual void Trace() 
    {
        agent.SetDestination(target.position);
        lookRotation.x = target.position.x;
        lookRotation.y = transform.position.y;
        lookRotation.z = target.position.z;
        transform.LookAt(lookRotation);
    }

    protected virtual void Wandering() 
    {
        if (agent.remainingDistance <= agent.stoppingDistance) 
        {
            if (RandomPoint(transform.position, WanderRange, out wanderDestination))
            {
                agent.SetDestination(wanderDestination);
                wanderCount++;
            }
        }
    }

    void ItemDrop() 
    {
        foreach (var item in dropItems) 
        {
            if (item.dropRatio > Random.value) 
            {
                uint count = (uint)Random.Range(0, item.dropCount) + 1;
                Factory.Instance.MakeItems(item.code, count, transform.position);
            }
        }
    }

    public void OnDetect(Transform targeting, float radi)
    {
        Vector3 dir = targeting.position - transform.position;
        float distance = dir.magnitude;
        float findDistance = radi * findDistanceRange;                                                            //chase���·� �Ѿ�� �Ÿ� �Ѱ�
        if ( State == EnemyState.Chase || State == EnemyState.Wander)
        {
            State = EnemyState.Chase;
            target = targeting;             //�����ð� �ʱ�ȭ
            chaseTime = 0.0f;
        }
        else if(State == EnemyState.Wait || State == EnemyState.Alert)
        {
            if (distance < findDistance)
            {
                State = EnemyState.Chase;
                target = targeting;             //�����ð� �ʱ�ȭ
                chaseTime = 0.0f;
                chaseAmpl = ((radi - distance) / radi) * (MaxChaseAmpl - 1) + 1;                                              //(�ִ�-����) / �ִ� : (0~1���̰� ����) -> +1(�ּҰ� ����)
            }
            else
            {
                State = EnemyState.Alert;
                agent.SetDestination(targeting.position);
                chaseAmpl = ((radi - distance) / radi) * (MaxChaseAmpl - 1) + 1;                                              //(�ִ�-����) / �ִ� : (0~1���̰� ����) -> +1(�ּҰ� ����)
            }
        }
    }

    public void OnDetect(Vector3 targetPos)
    {
        State = EnemyState.Alert;
        chaseAmpl = 3.0f;
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

    bool RandomPoint(Vector3 center, float range, out Vector3 result) 
    {
        bool isOk = false;
        result = Vector3.zero;
        for(int i = 0; i < 30; i++) 
        {
            Vector2 random = Random.insideUnitCircle * range;                               //�����̴ٺ��ϱ�(insideUnitSphere) ������ 60�̶� ���� ����� ������ �� ������ �ݰ� 1�ȿ� ���� ������,
            Vector3 randomPoint = center;                                                   //�������� ������� �ٲٰ�, ���� �ٸ�������� ����Ἥ ���� ����� �ݰ��� ���ϰ�
            randomPoint.x += random.x;
            randomPoint.z += random.y;
            NavMeshHit hit;                                                                 
            if (NavMesh.SamplePosition(randomPoint, out hit, 10.0f, NavMesh.AllAreas))      //��������Ʈ���� 3����(10.0f) �ݰ�ȿ��� ���� ����� �� ���� ������ flase
            {
                result = hit.position; 
                isOk = true;
                break;          //return true
            }
        }
        return isOk;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, transform.up, 4.0f);   //���ݹ���
        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, transform.up, 1.4f);   //��ž���Ͻ�
    }
#endif
}
