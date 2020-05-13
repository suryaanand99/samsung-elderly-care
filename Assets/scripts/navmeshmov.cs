using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class navmeshmov : MonoBehaviour
{
    [SerializeField] List<Transform> _patrols;
    [SerializeField] Text timer;

    int currentPatrolIndex;
    bool travelling,waiting;
    float waitTimer;
    float totalWaitTime;
    int currentHour;
    int eat = 1;
    NavMeshAgent _navMeshAgent;

    public void Start()
    {
        currentHour = int.Parse(timer.text.Split(':')[0]);
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        totalWaitTime = UnityEngine.Random.Range(10f, 30f);
        if(_patrols != null && _patrols.Count >= 2)
        {
            currentPatrolIndex = 0;
            Vector3 taget = _patrols[0].transform.position;
            _navMeshAgent.SetDestination(taget);
            travelling = true;
        }
    }

    private void FixedUpdate()
    {
        currentHour = int.Parse(timer.text.Split(':')[0]);
    }

    public void Update()
    {
        if (currentHour >= 21)
        {
            Vector3 taget = _patrols[3].transform.position;
            _navMeshAgent.SetDestination(taget);
        }
        if((currentHour >= 9 && currentHour <= 10) || (currentHour >= 13 && currentHour <= 14) || (currentHour >= 18 && currentHour <= 19))
        {
            if(eat == 1 && travelling && _navMeshAgent.remainingDistance <= 1f)
            {
                Vector3 taget = _patrols[1].transform.position;
                _navMeshAgent.SetDestination(taget);
                travelling = true;
                eat = 0;
            }
        }
        if (currentHour > 10 || currentHour > 14 || currentHour > 19)
        {
            eat = 1;
        }
        if(travelling && _navMeshAgent.remainingDistance <= 1f && currentHour >= 7)
        {
            if(currentHour <= 7)
            {
                totalWaitTime = 0f;
            }
            waitTimer += Time.deltaTime;
            if (waitTimer >= totalWaitTime)
            {
                changePatrolPoint();
                waitTimer = 0f;
                totalWaitTime = UnityEngine.Random.Range(15f,30f);
                setDestination();
            }
        }
    }

    public void changePatrolPoint()
    {
        currentPatrolIndex = (int)UnityEngine.Random.Range(0, _patrols.Count);
    }

    public void setDestination()
    {
        if(currentPatrolIndex == 1)
        {
            eat = 0;
        }
        Vector3 taget = _patrols[currentPatrolIndex].transform.position;
        _navMeshAgent.SetDestination(taget);
        travelling = true;
    }
}
