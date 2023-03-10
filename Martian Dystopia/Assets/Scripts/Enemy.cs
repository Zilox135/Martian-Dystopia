using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int creditReward = 25;
    [SerializeField] int creditPenalty = 25;

    Bank bank;
    
    void Start()
    {
        bank = FindObjectOfType<Bank>();       
    }

    public void RewardCredit()
    {
        if(bank == null) { return; }
        bank.Deposit(creditReward);
    }

    public void StealCredit()
    {
        if(bank == null) { return; }
        bank.Withdraw(creditPenalty);
    }
}
