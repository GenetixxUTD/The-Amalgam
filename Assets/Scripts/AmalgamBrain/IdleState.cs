using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EmptyState
{

    public bool idledEnough;

    public override void stateStart(AmalgamCentralAI amalgamBrain)
    {
        base.stateStart(amalgamBrain);
        idledEnough = false;
        amalgamBrain.interuptedEvent = false;
        StartCoroutine(IdleTimer());
    }

    public override Type stateUpdate(AmalgamCentralAI amalgamBrain)
    {
        base.stateUpdate(amalgamBrain);
        if (idledEnough && !amalgamBrain.interuptedEvent)
        {
            return typeof(RoamingState);
        }
        else if(amalgamBrain.interuptedEvent)
        {
            return typeof(HuntingState);
        }
        else
        {
            return typeof(IdleState);
        }
    }

    public override void stateExit(AmalgamCentralAI amalgamBrain)
    {
        base.stateExit(amalgamBrain);
    }

    public IEnumerator IdleTimer()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(3f, 5f));
        idledEnough = true;
    }
}
