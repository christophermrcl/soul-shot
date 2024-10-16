using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public float amountHP;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(amountHP <= 0f)
        {
            anim.SetBool("isDead", true);
        }
    }

    public void TakeDamage(float amount)
    {
        amountHP -= amount;
    }
}
