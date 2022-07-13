using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlColumns : MonoBehaviour
{
    //Current column
    private int index = 0;
    private int MAX_COLS = 6;

    private void Awake() 
    {
        GenerateRooms();
    }

    //Moves the "player" one column to the right, making nodes behind him locked, and those in front of him - reachable
    public void goNext()
    {
        if(index < MAX_COLS)
        {
            ++index;
            for(int i = index-1; i<MAX_COLS; ++i) 
            {
                //Sets the states of the nodes of each column
                ControlRows rows = transform.GetChild(i).GetComponent<ControlRows>();
                if(i == index)
                    rows.setStates(Node.State.Reachable);
                else
                    rows.setStates(Node.State.Locked);
            }
            //Debug.Log("Opening column " + index);
        }
        else
            Debug.Log("Reached end of map");
    }

    private void GenerateRooms()
    {
        //Last two rooms are excluded from the random generation
        int max = MAX_COLS-2;

        //First room is always a battle
        ControlRows CR_0 = transform.GetChild(0).GetComponent<ControlRows>();
        CR_0.SetRoomsType(Node.Type.Battle);
        CR_0.setStates(Node.State.Reachable);

        //Other rooms get setassigned a random type
        for(int i = 1; i<max; ++i) 
        {
            ControlRows rows = transform.GetChild(i).GetComponent<ControlRows>();
            rows.SetRandomType();
            rows.setStates(Node.State.Locked);
        }

        //Last two rooms are always a rest followed by an elite fight
        transform.GetChild(max).GetComponent<ControlRows>().SetRoomsType(Node.Type.Rest);
        transform.GetChild(max).GetComponent<ControlRows>().setStates(Node.State.Locked);
        transform.GetChild(max+1).GetComponent<ControlRows>().SetRoomsType(Node.Type.Elite);
        transform.GetChild(max+1).GetComponent<ControlRows>().setStates(Node.State.Locked);
    }

}
