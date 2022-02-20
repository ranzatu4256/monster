using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;
using UnityEngine.UI;

// RollerAgent
public class hunter_move : Agent
{
    Rigidbody rBody;
    public GameObject monster;
    public GameObject score;

    // 初期化時に呼ばれる
    public override void Initialize()
    {
        this.rBody = GetComponent<Rigidbody>();
    }

    // エピソード開始時に呼ばれる
    public override void OnEpisodeBegin()
    {
    }

    // 観察取得時に呼ばれる
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(this.transform.localRotation);

        sensor.AddObservation(monster.transform.localPosition);
        sensor.AddObservation(monster.transform.localRotation);
    }

    // 行動実行時に呼ばれる
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // RollerAgentに力を加える
        Vector3 controlSignal = Vector3.zero;
        Vector3 roteSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.y = actionBuffers.ContinuousActions[1];
        rBody.AddForce(controlSignal * 400f);
        rBody.velocity = Vector3.zero; // 3Dの場合

        
    }

    void Update()
    {

        transform.LookAt(monster.transform, Vector3.forward);

        transform.Rotate(new Vector3(-180f, -180f, +90f));

    }

    //OnTriggerEnter関数
    //接触したオブジェクトが引数otherとして渡される
    void OnTriggerEnter(Collider other)
    {
            //接触したオブジェクトのタグ
        if (other.CompareTag("red_attack"))
        {
            this.transform.localPosition = new Vector3(
                -8f, 8f, 0.0f);
            monster.transform.localPosition = new Vector3(
                8f, -8f, 0.0f);
            monster.tag = "losted";
            score.GetComponent<Renderer>().material.color = Color.red;
            this.AddReward(-1f);
            EndEpisode();
        }

        if (other.CompareTag("searched"))
        {
            this.transform.localPosition = new Vector3(
                -8f, 8f, 0.0f);
            monster.transform.localPosition = new Vector3(
                8f, -8f, 0.0f);
            score.GetComponent<Renderer>().material.color = Color.blue;
            this.AddReward(1f);
            EndEpisode();
        }

        if (other.CompareTag("losted"))
        {
            this.transform.localPosition = new Vector3(
                -8f, 8f, 0.0f);
            monster.transform.localPosition = new Vector3(
                8f, -8f, 0.0f);
            score.GetComponent<Renderer>().material.color = Color.blue;
            this.AddReward(1f);
            EndEpisode();
        }
    }

    // ヒューリスティックモードの行動決定時に呼ばれる
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }
}

//>mlagents-learn config/taisen.yaml --run-id=monster --env=apps/renewal --force