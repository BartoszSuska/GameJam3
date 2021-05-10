using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.BoarShroom.GameJam3
{
    public class Bones : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D col)
        {
            if(col.gameObject.tag == "Player")
            {
                col.GetComponent<Player>().nearBone = true;
            }
        }

        void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                col.GetComponent<Player>().nearBone = false;
            }
        }
    }
}
