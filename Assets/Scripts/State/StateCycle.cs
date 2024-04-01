using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Reference: https://www.youtube.com/watch?v=dWrRb9pO8aQ&t=932s

public class StateCycle : StateMachine
{
    public StartState startState { get; private set; }
    public MoveState moveState { get; private set; }
    public AttackState attackState { get; private set; }

    UnitClick unitClick;
    NavMeshAgent agent;
    LineRenderer lineRenderer;

    public Vector3 targetPosition { get; set; }

    public LayerMask player2;
    public int attackDamage = 5;
    public float bulletAttackTime;
    public float swordAttackTime;
    public int attackRange = 10;
    public bool alreadyAttacked = false;
    public GameObject projectile;
    public GameObject projectilePosition;
    public float forward = 32f;
    public float upward = 1f;
    public float right = 1f;
    public int priority = 0;
    public float radius = 5.0f;

    public bool isSelected = false;

    private void Awake()
    {
        unitClick = GameObject.Find("Game Manager").GetComponent<UnitClick>();
        agent = GetComponent<NavMeshAgent>();
        lineRenderer = GetComponent<LineRenderer>();

        startState = new StartState(this, unitClick);
        moveState = new MoveState(this, agent, lineRenderer);
        attackState = new AttackState(this, agent);
    }

    private void OnEnable()
    {
        unitClick.NewTargetAcquired += OnNewTargetAcquired;
    }

    private void OnDisable()
    {
        unitClick.NewTargetAcquired -= OnNewTargetAcquired;
    }

    private void Start()
    {
        ChangeState(startState);
    }

    public void OnNewTargetAcquired(Vector3 newTarget)
    {
        targetPosition = newTarget;
    }
}
