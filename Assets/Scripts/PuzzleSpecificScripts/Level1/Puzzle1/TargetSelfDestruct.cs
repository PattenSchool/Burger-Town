using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelfDestruct : MonoBehaviour, IHitable
{

    public void IHit()
    {
        this.gameObject.SetActive(false);
    }

    private void SubtractNumber()
    {
        GateTrigger.targetNum -= 1;
        BridgeRiseTrigger.targetNum -= 1;
    }
}
