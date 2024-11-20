using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform waypointA;
    public Transform waypointB;
    public float movementSpeed = 2f;
    private Animator animator;
    private bool isWalking = false;

    private Transform currentTarget;
    private Rigidbody2D rb;
    private Vector3 scale;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentTarget = waypointA;
        scale = transform.localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            animator.SetTrigger("Attack");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CombatZone"))
        {
            animator.SetTrigger("Death");
            Destroy(gameObject, 1f);
        }
    }

    void Update()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        Vector3 curTargetHorizontal = new Vector2(currentTarget.position.x, transform.position.y);
        Vector2 direction = (curTargetHorizontal - transform.position).normalized;

        transform.position += (Vector3)direction * movementSpeed * Time.deltaTime;

        if(Vector2.Distance(curTargetHorizontal, transform.position) <= 0.2f)
        {
            SwitchTarget();
        }
        UpdateAnimation();
    }

    private void SwitchTarget()
    {
        if(currentTarget == waypointA)
        {
            currentTarget = waypointB;
            Flip();
        }
        else
        {
            currentTarget = waypointA;
            transform.localScale = scale;
        }
    }

    private void UpdateAnimation()
    {
        isWalking = (Vector2.Distance(transform.position, currentTarget.position) <= 0.2f);
        animator.SetBool("isWalking", true);
    }

    private void Flip()
    {
        Vector3 flippedScale = scale;
        flippedScale.x *= -1;
        transform.localScale = flippedScale;
    }

}
