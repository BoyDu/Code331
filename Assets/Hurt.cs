using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt : MonoBehaviour
{
    public int hp = 3;
    private bool isDead = false;
    public void DoHurt()
    {
        hp -= 1;
        Debug.Log("DoHurt.............");
        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        GetComponent<BoxCollider>().isTrigger = true;
        gameObject.SetActive(false);
    }

    public bool IsDead()
        { return isDead; }
}
