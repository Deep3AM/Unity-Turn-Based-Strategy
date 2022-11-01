using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    delegate void MoveCompleteDelegate();
    [SerializeField] private Animator unitAnimator;
    [SerializeField] int maxMoveDistance;
    private Vector3 targetPosition;
    protected override void Awake()
    {
        base.Awake();
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            return;
        }
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        float stoppingDistance = .1f;

        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {

            float moveSpeed = 4f;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            unitAnimator.SetBool("IsWalking", true);
        }
        else
        {
            unitAnimator.SetBool("IsWalking", false);
            isActive = false;
            onActionComplete();
        }

        float rotateSpped = 10f;
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpped);
    }

    public override void TakeAction(GridPosition gridPosition, Action onMoveComplete)
    {
        this.onActionComplete = onMoveComplete;
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        isActive = true;
    }


    public override List<GridPosition> GetValidGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();
        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }
                if (unitGridPosition == testGridPosition)
                {
                    continue;
                }
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    continue;
                }
                validGridPositionList.Add(testGridPosition);
            }
        }
        return validGridPositionList;
    }

    public override string GetActionName()
    {
        return "Move";
    }
}