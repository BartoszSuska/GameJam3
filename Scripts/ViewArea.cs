using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.BoarShroom.GameJam3
{
    public class ViewArea : MonoBehaviour
    {
        public Chicken chicken;

        void OnTriggerStay2D(Collider2D col)
        {
            if(col.gameObject.tag == "Player")
            {
                if(col.GetComponent<Player>().nearBone == true)
                {
                    chicken.spotted = true;
                }
            }
        }
    }
}
