using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting;

public class TaskPatrol : Node
{
    // 当前npc位置
    private Transform _transform;
    private Rigidbody2D _rigidbody2D;
    // 巡逻点位
    private Transform[] _wayPoints;
    private Animator _animator;

    //巡逻属性配置
    private float _waitTime = 1f; // in seconds
    private float _waitCounter = 0f;
    private bool _waiting = false;
    private int _currentWayPointIndex = 0;


    public TaskPatrol(Transform transform, Transform[] wayPoints)
    {
        _transform = transform;
        _wayPoints = wayPoints;
        _animator = transform.GetComponent<Animator>();
        _rigidbody2D = transform.GetComponent<Rigidbody2D>();
    }

    public override NodeState Evaluate()
    {
        if(_waiting)
        {
            _waitCounter += Time.deltaTime;
            if(_waitCounter >= _waitTime)
            {
                _waiting = false;
                // 变更动画parameters
                // _animator.SetBool("Walking", true);
            }
        }
        else 
        {
            Transform wp = _wayPoints[_currentWayPointIndex];
            if(Vector3.Distance(_transform.position, wp.position) < 0.01f)
            {
                _waiting = true;
                _transform.position = wp.position;
                _waitCounter = 0f;

                _currentWayPointIndex = (_currentWayPointIndex + 1) % _wayPoints.Length;
                // 变更动画 parameters
                // _animator.SetBool("Walking", false);
                // 停下
                _animator.SetFloat ("Vertical", 0);
                _animator.SetFloat ("Horizontal", 0);
                _animator.SetFloat("Speed", 0);
            }
            else 
            {
                Transform lastWp = _wayPoints[(_currentWayPointIndex + _wayPoints.Length - 1) % _wayPoints.Length];
                Vector3 dir = wp.position - lastWp.position;
                float distance = dir.magnitude; 
                // 移动
                // _transform.position = Vector2.MoveTowards(_transform.position, wp.position, 0.5f);
                if(dir.x != 0) {
                    _animator.SetFloat ("Vertical", 0);
                    _animator.SetFloat ("Horizontal", dir.x);
                }

                if(dir.y != 0) {
                    _animator.SetFloat ("Vertical", dir.y);
                    _animator.SetFloat ("Horizontal", 0);
                }

                _animator.SetFloat("Speed", 0.5f);
                Vector2 newPosition = Vector2.MoveTowards(_transform.position, wp.position, Time.deltaTime * 0.5f);
                _rigidbody2D.MovePosition(newPosition);
            }
        }

        state = NodeState.RUNNING;
        return state;
    }
}
