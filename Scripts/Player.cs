using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.BoarShroom.GameJam3
{
    public class Player : MonoBehaviour
    {
        public float speed;
        Animator anim;
        Rigidbody2D rb;
        [SerializeField] AudioSource audio;

        public bool nearBone;

        public float startTimeBtwAttack;
        float timeBtwAttack;

        public Transform attackPos;
        public float attackRange;
        public LayerMask whatIsEnemies;

        Vector2 moveDir;

        void Start()
        {
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            moveDir = new Vector2(horizontal, vertical);

            anim.SetFloat("speed", Mathf.Max(Mathf.Abs(horizontal), Mathf.Abs(vertical)));

            if(horizontal > 0)
            {
                transform.rotation = new Quaternion(0, 0, 0, 1);
            }
            else if(horizontal < 0)
            {
                transform.rotation = new Quaternion(0, 180, 0, 1);
            }

            //transform.Translate(moveDir * Time.deltaTime * speed, Space.World);

            if(timeBtwAttack <= 0)
            {
                if(Input.GetKey(KeyCode.Space))
                {
                    Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);

                    if(enemy.Length > 0)
                    {
                        audio.Play();
                    }

                    for(int i = 0; i < enemy.Length; i++)
                    {
                        enemy[i].gameObject.GetComponent<Chicken>().Kill();
                    }

                    timeBtwAttack = startTimeBtwAttack;
                }
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }

        private void FixedUpdate()
        {
            rb.velocity = moveDir.normalized * speed * Time.fixedDeltaTime;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPos.position, attackRange);
        }

    }
}
