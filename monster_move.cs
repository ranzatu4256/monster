using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// RollerAgent
public class monster_move : MonoBehaviour
{
    public GameObject targetObject; // 注視したいオブジェクトをInspectorから入れておく

    // Update is called once per frame
    void Update()
    {
        Rigidbody rBody = GetComponent<Rigidbody>();
        // ターゲット方向のベクトルを取得
        Vector3 relativePos = targetObject.transform.position - this.transform.position;
        // 方向を、回転情報に変換
        Quaternion rotation = Quaternion.LookRotation(relativePos);

        if (this.gameObject.CompareTag("searched"))
        {
            float speed = 0.001f;
            transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, speed);
            transform.Rotate(new Vector3(180f, 180f, 0f));
            GetComponent<Rigidbody>().AddForce(transform.forward * 200f, ForceMode.Force);
            rBody.velocity = Vector3.zero;
        }

        if (this.gameObject.CompareTag("losted"))
        {
            float speed = 0.01f;
            transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, speed);
            transform.Rotate(new Vector3(180f, 180f, 0f));
            rBody.velocity = Vector3.zero;
        }

        rBody.velocity = Vector3.zero;
    }

}