using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMove : NetworkBehaviour
{
    float h, v;

    public int hp;

    [Networked, OnChangedRender(nameof(OnAttack))]
    public string NetWorked_youWon { get; set; }
    public TextMeshProUGUI youWon { get; set; }

    void Start()
    {
        youWon = GameObject.FindGameObjectWithTag("Win").GetComponent<TextMeshProUGUI>();
        if (!HasStateAuthority) return;
    }

    public override void FixedUpdateNetwork()
    {
        if (!HasStateAuthority) return;

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        if(h != 0 || v != 0)
        {
            transform.position += new Vector3(h, v, 0);
        }
    }

    public void OnAttack()
    {
        hp--;
        Debug.Log("Hit opponent");
        if (hp <= 0)
        {
            hp = 0;
            youWon.text = "You won";
            Debug.Log("You won");
        }
    }

    void ShowWinText()
    {
        if(NetWorked_youWon == "You won")
        {
            youWon.text = "You Lose";
        }
    }

    //private void OnMouseDown()
    //{
    //    if (!HasStateAuthority)
    //    {
    //        OnAttack();
    //    }
    //}
}
