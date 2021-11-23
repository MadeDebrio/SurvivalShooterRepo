using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    public class EnemyAttack : MonoBehaviour
    {
        public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
        public int attackDamage = 10;               // The amount of health taken away per attack.


        Animator anim;                              // Reference to the animator component.
        GameObject player;                          // Reference to the player GameObject.
        PlayerHealth playerHealth;                  // Reference to the player's health.
        EnemyHealth enemyHealth;                    // Reference to this enemy's health.
        bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
        float timer;                                // Timer for counting up to the next attack.


        void Awake()
        {
            //Mencari game object dengan tag "Player"
            player = GameObject.FindGameObjectWithTag("Player");

            //mendapatkan komponen player health
            playerHealth = player.GetComponent<PlayerHealth>();

            //mendapatkan komponen Animator
            anim = GetComponent<Animator>();

            //Mendapatkan Enemy health
            enemyHealth = GetComponent<EnemyHealth>();
        }


        //Callback jika ada suatu object masuk kedalam trigger


        void OnTriggerEnter(Collider other)
        {
            //Set player in range
            if (other.gameObject == player && other.isTrigger == false)
            {
                playerInRange = true;
            }
        }


        //Callback jika ada object yang keluar dari trigger
        void OnTriggerExit(Collider other)
        {
            //Set player jika tidak dalam range
            if (other.gameObject == player)
            {
                playerInRange = false;
            }
        }


        void Update()
        {
            timer += Time.deltaTime;

            if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
            {
                Attack();
            }

            //mentrigger animasi PlayerDead jika darah player kurang dari sama dengan 0
            if (playerHealth.currentHealth <= 0)
            {
                anim.SetTrigger("PlayerDead");
            }
        }


        void Attack()
        {
            //Reset timer
            timer = 0f;

            //Taking Damage
            if (playerHealth.currentHealth > 0)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }
}