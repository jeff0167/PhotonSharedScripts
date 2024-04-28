using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMove : NetworkBehaviour
{
    float h, v;
    bool isDead = false;

    [Networked, OnChangedRender(nameof(HealthChanged))]
    public float Networked_hp { get; set; }

    public TextMeshProUGUI youWon { get; set; }

    void Start()
    {
        youWon = GameObject.FindGameObjectWithTag("Win").GetComponent<TextMeshProUGUI>();
        if (!HasStateAuthority) return;
    }

    void HealthChanged()
    {
        Debug.Log($"Health changed to: {Networked_hp}");
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

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void OnAttackRpc()
    {
        if(isDead) return;
        Networked_hp--;
        if (Networked_hp <= 0)
        {
            Networked_hp = 0;
            isDead = true;
            ShowWinTextRpc();
        }
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    void ShowWinTextRpc() // not ideal
    {
        if (HasStateAuthority)
        {
            youWon.text = "You Lose"; // if I call myself it means I lost
        }
        else
        {
            youWon.text = "You won";
        }
    }
}
