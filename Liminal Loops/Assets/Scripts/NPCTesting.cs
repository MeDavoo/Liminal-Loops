using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class NPCTesting : MonoBehaviour
{

    private NavMeshAgent navMeshAgent;

    public NpcData npcData;

    public Vector3 goToLocation;

    public int currentTask = 0;
    private void OnEnable()
    {
        TimeManager.OnMinuteChanged += TimeCheck;
        TimeManager.OnHourChanged += TimeCheck;
        TimeManager.OnDayChanged += TimeCheck;
    }


    private void OnDisable()
    {
        TimeManager.OnMinuteChanged -= TimeCheck;
        TimeManager.OnHourChanged -= TimeCheck;
        TimeManager.OnDayChanged -= TimeCheck;
    }


    // Start is called before the first frame update
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }


    public void Update()
    {
        goToLocation = new Vector3(npcData.NpcRoutine[currentTask].coordinates.x, npcData.NpcRoutine[currentTask].coordinates.y, npcData.NpcRoutine[currentTask].coordinates.z);
    }


    private void TimeCheck()
    {
        
        if (TimeManager.Hour == npcData.NpcRoutine[currentTask].Hour && TimeManager.Minute == npcData.NpcRoutine[currentTask].Minute && TimeManager.Day == npcData.NpcRoutine[currentTask].Day)
        {
            navMeshAgent.destination = goToLocation;
            navMeshAgent.speed = npcData.NpcRoutine[currentTask].speed;
            if (npcData.NpcRoutine.Count - 1 > currentTask)
            {
                currentTask++;
            }
        }
    }
}
