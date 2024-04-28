using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveObjectToMousePos2D : NetworkBehaviour
{
    public float Movement;
    [Range(0,100)]
    public float MovementSpeed;
    bool isMoving = false;
    Vector3 targetPos;

    private void Start()
    {
        gameObject.tag = !HasStateAuthority ? "Enemy" : "Player"; // if not client then they have tag enemy, when client we have the tag player
    }

    public override void FixedUpdateNetwork()
    {
        if (!HasStateAuthority) return;

        if (Movement <= 0) return;

        if (Input.GetKey(KeyCode.Mouse0)) // don't move if clicking on enemy
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var d = Vector3.Distance(pos, transform.position);

            if (Movement - d < 0) return;
            Movement -= d;

            //transform.position = Vector3.MoveTowards(transform.position, pos, 1 * Time.deltaTime);
            targetPos = pos;
            if (!IsClickingEnemy(targetPos))
            {
                isMoving = true;
            }
            //transform.position = new Vector3(pos.x, pos.y, 0);
        }

        if (isMoving) CheckIfShouldMove();
    }

    void CheckIfShouldMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, MovementSpeed * Time.deltaTime);

        // Check if reached target or close enough (adjust epsilon based on movement precision)
        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            isMoving = false;
        }
    }

    private bool IsClickingEnemy(Vector3 targetPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        bool hitEnemy = hit.collider != null && hit.collider.gameObject.tag == "Enemy";
        if (hitEnemy) hit.collider.gameObject.GetComponent<PlayerMove>().OnAttack();

        return hitEnemy;
    }
}
