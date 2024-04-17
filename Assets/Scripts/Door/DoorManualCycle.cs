using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManualCycle : DoorManual
{
    [Range(600, 1200)]  //(10��-20��)
    public float cycle = 1200.0f;
    public float openPeriod = 300.0f; //5��

    float cycleTime = 0.0f;
    float periodTime = 0.0f;
    DoorState state = DoorState.Close;

    enum DoorState 
    {
        Open,
        Close
    }

    void Update()
    {
        switch (state) 
        {
            case DoorState.Open:
                cycleTime = 0.0f;
                periodTime += Time.time;
                if (periodTime > openPeriod) 
                {
                    state = DoorState.Close;
                }
                break;
            case DoorState.Close:
                periodTime = 0.0f;
                cycleTime += Time.time;
                if (cycleTime > cycle)
                {
                    state = DoorState.Open;
                }
                break;
        }
    }

    public override void Interect()
    {
        Open();
    }

    protected override bool preOpen()
    {
        bool result = (state == DoorState.Open);
        return result;
    }

    protected override void Open()
    {
        if (preOpen())
        {
            Debug.Log("Ż��");
            //ȭ����ȯ(���Ӹ޴�)
        }
    }
}
