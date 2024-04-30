using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
using System.Linq;

public class AmalgamCentralAI : MonoBehaviour
{
    private float stunHealth;

    public bool playerInSight;

    public Transform interuptedEvent;

    public GameObject playerTracker;

    public Transform[] leavingAreas;

    private int tensionMeter;

    public AmalgamFSM FSMLogic;

    [SerializeField]
    public List<EmptyState> states;

    


    private void Start()
    {
        interuptedEvent = null;
        FSMLogic = GetComponent<AmalgamFSM>();
        /*states = new Dictionary<Type, EmptyState>()
        {
            {typeof(RoamingState), new RoamingState() },
            {typeof(IdleState), new IdleState() },
            {typeof(StalkingState), new StalkingState() },
            {typeof(HuntingState), new HuntingState() },
            {typeof(HauntingState), new HauntingState() },
            {typeof(LeavingState), new LeavingState() }
        };*/
        Debug.Log(states.ToList());
        FSMLogic.GrabAllStates(states);
    }

    private void Update()
    {
        trackPlayerVisability();

        if(playerTracker.GetComponent<charactercontroller>().ActiveState == charactercontroller.PlayerState.Paused)
        {
            gameObject.GetComponent<NavMeshAgent>().SetDestination(this.transform.position);
            gameObject.GetComponent<AmalgamFSM>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<AmalgamFSM>().enabled = true;
        }
    }

    private void trackPlayerVisability()
    {
        RaycastHit hit;
        Vector3 dir = -this.transform.position + playerTracker.transform.position; 
        if (Physics.Raycast(transform.position, dir * 10, out hit, 1000.0f))
        {
            Debug.Log(hit.transform.tag);
            float temp = Vector3.Dot(dir.normalized, transform.forward);
            temp = (Mathf.Acos(temp) * Mathf.Rad2Deg);
            if(hit.transform.tag == "Player" && temp < 70)
            {
                
                playerInSight = true;
            }
            else
            {
                playerInSight = false;
            }
        }
    }

    public float playerDistance()
    {
        return Vector3.Distance(this.transform.position, playerTracker.transform.position);
    }
    
}
