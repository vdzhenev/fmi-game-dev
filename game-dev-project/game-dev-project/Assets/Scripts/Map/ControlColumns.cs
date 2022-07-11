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
            for(int i = 0; i<MAX_COLS; ++i) 
            {
                ControlRows rows = transform.GetChild(i).GetComponent<ControlRows>();
                if(i == index)
                    rows.setStates(Node.State.Reachable);
                else
                    rows.setStates(Node.State.Locked);
            }
            ++index;
        }
        else
            Debug.Log("Reached end of map");
    }

    private void GenerateRooms()
    {
        int max = MAX_COLS-2;
        for(int i = 0; i<max; ++i) 
        {
            Debug.Log(transform.GetChild(i).name);
            ControlRows rows = transform.GetChild(i).GetComponent<ControlRows>();
            rows.SetRandomType();
        }
        transform.GetChild(max).GetComponent<ControlRows>().SetRoomsType(Node.Type.Rest);
        transform.GetChild(max+1).GetComponent<ControlRows>().SetRoomsType(Node.Type.Elite);
    }

}
