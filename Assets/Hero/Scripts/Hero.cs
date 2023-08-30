using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Hero : MonoBehaviour
{
    public int _HP, _HP_Max;
    public int _atk;
    public int _atk_range;
    public float _speed;

    private NavMeshAgent agent;
    private Transform targetEnemy;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        _HP = _HP_Max;
        _atk = 10;
        _atk_range = 2;
        _speed = 3;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            MoveToClickedPoint(mouse);
        }
        //else if (Input.GetMouseButtonDown(0)) // Left-click
        //{
          //  AttackEnemy();
        //}
        UpdateAnimator();

    }

    public void UpdateAnimator()
    {
        _speed = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("Speed", _speed);
    }

    public void AttackEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                targetEnemy = hit.collider.transform;
                agent.SetDestination(targetEnemy.position);
            }
        }
    }

    public void MoveToClickedPoint(Mouse Input_mouse)
    {
        Vector3 mousePosition = Input_mouse.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            print(hit.point);
            if (!hit.collider.CompareTag("Enemy"))
            {
                targetEnemy = null;
                agent.SetDestination(hit.point);
            }
        }
    }
}
