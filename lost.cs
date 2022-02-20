using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lost : MonoBehaviour
{
    public GameObject monster;

    //OnTriggerEnter関数
    //接触したオブジェクトが引数otherとして渡される
    void OnTriggerEnter(Collider other)
    {
        //接触したオブジェクトのタグ
        if (other.CompareTag("Blue"))
        {
            monster.tag = "losted";
        }
    }

}