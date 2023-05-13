using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCTesting : MonoBehaviour
{

    [SerializeField] private Transform movePosTransform;

    private NavMeshAgent navMeshAgent;

    public float hours;
    public float minutes;

    private void OnEnable()
    {
        TimeManager.OnMinuteChanged += TimeCheck;
    }


    private void OnDisable()
    {
        TimeManager.OnMinuteChanged -= TimeCheck;
    }


    // Start is called before the first frame update
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void TimeCheck()
    {
        if (TimeManager.Hour == hours && TimeManager.Minute == minutes)
        {
            navMeshAgent.destination = movePosTransform.position;
        }
    }
}
