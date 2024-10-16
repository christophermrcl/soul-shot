using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 5f; // Movement speed of the enemy
    public bool playerInAttackArea = false; // Bool to check if the player is in the attack area
    public float attackRange = 5f; // Range within which the player is considered in the attack area

    private Transform player; // Reference to the player's transform
    private EnemyHP hp;
    public Animator anim;

    void Start()
    {
        // Find the player object by its tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
        hp = GetComponent<EnemyHP>();
    }

    void Update()
    {
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Get the current state info from the animator
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        // If the player is not in the attack area, and the attack animation is not playing, move towards the player
        if (!playerInAttackArea && player != null && hp.amountHP > 0 && !stateInfo.IsName("Attack"))
        {
            FacePlayer(); // Face the player
            MoveTowardsPlayer(); // Move toward the player
            anim.SetBool("isWalking", true);
        }
        else if (stateInfo.IsName("Attack"))
        {
            // If the attack animation is playing, stop movement
            StopMovement();
            anim.SetBool("isWalking", false);
        }

        // If the player is within the attack range, trigger the attack animation
        if (playerInAttackArea)
        {
            anim.SetBool("isAttacking", true);
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }

        // Update the playerInAttackArea based on distance
        playerInAttackArea = distanceToPlayer <= attackRange;
    }

    void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // Ensure the enemy only rotates on the Y-axis

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    void StopMovement()
    {
        // Logic to stop movement
    }
}
