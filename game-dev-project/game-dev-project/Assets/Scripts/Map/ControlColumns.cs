using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlColumns : MonoBehaviour
{
    private int index = 0;
    private int MAX_COLS = 6;

    private void Awake() 
    {
        GenerateRooms();
    }

    public void goNext()
    {
        if(index < MAX_COLS)
        {
            ++index;
            for(int i = 0; i<MAX_COLS; ++i) 
            {
                ControlRows rows = transform.GetChild(i).GetComponent<ControlRows>();
                if(i == index)
                    rows.setStates(Node.State.Reachable);
                else
                    rows.setStates(Node.State.Locked);
            }
            Debug.Log("Opening column " + index);
        }
        else
            Debug.Log("Reached end of map");
    }

    private void GenerateRooms()
    {
        int max = MAX_COLS-2;
        ControlRows CR_0 = transform.GetChild(0).GetComponent<ControlRows>();
        CR_0.SetRoomsType(Node.Type.Battle);
        CR_0.setStates(Node.State.Reachable);
        for(int i = 1; i<max; ++i) 
        {
            ControlRows rows = transform.GetChild(i).GetComponent<ControlRows>();
            rows.SetRandomType();
            rows.setStates(Node.State.Locked);
        }
        transform.GetChild(max).GetComponent<ControlRows>().SetRoomsType(Node.Type.Rest);
        transform.GetChild(max).GetComponent<ControlRows>().setStates(Node.State.Locked);
        transform.GetChild(max+1).GetComponent<ControlRows>().SetRoomsType(Node.Type.Elite);
        transform.GetChild(max+1).GetComponent<ControlRows>().setStates(Node.State.Locked);
    }

}
