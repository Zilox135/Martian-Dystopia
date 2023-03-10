using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] int cost = 75;
    [SerializeField] float buildDelay = 1f;
    
    void Start()
    {
        StartCoroutine(Build());
    }
    
    public bool CreateTurret(Turret turret, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if(bank == null)
        {
            return false;
        }

        if(bank.CurrentBalance >= cost)
        {
           Instantiate(turret, position, Quaternion.identity);
           bank.Withdraw(cost);
           return true;           
        }

        return false;
    }

    IEnumerator Build()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false); 
            foreach(Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(false);
            }        
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);
            foreach(Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(true);
            }        
        }
    }
}
