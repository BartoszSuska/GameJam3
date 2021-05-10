using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.BoarShroom.GameJam3
{
    public class Chicken : MonoBehaviour
    {
        public float minX;
        public float maxX;
        public float minY;
        public float maxY;
        public float speed;
        public bool spotted;

        public GameObject bonesPrefab;

        Vector2 target;
        Animator anim;
        GameObject manager;

        void Start()
        {
            anim = GetComponent<Animator>();
            manager = GameObject.FindGameObjectWithTag("Manager");
            target = GetRandomPosition();
            //target = new Vector2(0, 11);
        }

        void Update()
        {
            if(target.x < transform.position.x)
            {
                transform.rotation = new Quaternion(0, 180, 0, 1);
            }
            else
            {
                transform.rotation = new Quaternion(0, 0, 0, 1);
            }

            if ((Vector2)transform.position != target && !spotted)
            {
                transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            }
            else if((Vector2)transform.position == target && !spotted)
            {
                target = GetRandomPosition();
            }
            else if(spotted)
            {
                anim.SetTrigger("Spot");
                manager.GetComponent<Manager>().End();
            }
        }

        Vector2 GetRandomPosition()
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);

            return new Vector2(randomX, randomY);
        }

        public void Kill()
        {
            manager.GetComponent<Manager>().chickens--;
            Instantiate(bonesPrefab, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
