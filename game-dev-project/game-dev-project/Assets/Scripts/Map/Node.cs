using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    //Nodes have states:
    //  Locked      Can no longer be accessed
    //  Visited     Has been visited
    //  Reachable   Can be chosen by the player
    public enum State 
    {
        Locked,
        Visited,
        Reachable
    }

    //Nodes have types:
    //  Battle      A battle with common enemies
    //  Elite       A battle with more difficult enemies
    //  Rest        Allows the player to heal their characters
    //  Mystery     One of the other room types or A special event.
    //  Shop        TODO: Player can buy equipment to make characters stronger
    //  Empty       Placeholder
    public enum Type
    {
        Battle,
        Elite,
        Rest,
        Mystery,
        Shop,
        Empty
    }

    private State state = State.Locked;
    private Type type = Type.Empty;

    private static GameManager gameManager;
    private static GameObject mapManager;
 
    void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
        if (mapManager == null)
        {
            mapManager = GameObject.FindGameObjectWithTag("Map Manager");
        }
    }


    //When chosing a node, the node becomes visited and the player moves to the next column of the map
    //The game manager loads the scene corresponding to room type
    public void OnClick()
    {
        setState(State.Visited);
        mapManager.GetComponent<ControlColumns>().goNext();
        gameManager.LoadRoom(type);
    }

    //Sets the state of the node
    public void setState(State s) 
    {
        //If the node is visited, the state doesn't change
        if(state != State.Visited)
        {    
            state = s;
            Image sr = transform.GetComponent<Image>();
            Button b = transform.GetComponent<Button>();
            switch (s)
            {
                //Changes the node color and button.interactable accordingly
                case State.Locked:
                    sr.color = Color.gray;
                    b.interactable = false;
                    break;
                case State.Visited:
                    sr.color = Color.cyan;
                    b.interactable = false;
                    break;
                case State.Reachable:
                    sr.color = Color.white;
                    b.interactable = true;
                    break;
                default:
                    break;
            }
        }
    }

    //Sets the room type
    public void setType(Type t)
    {
        //Changes the room icon accordingly
        gameObject.GetComponent<Image>().sprite = getRoomIcon(t);
        //If the room has been assigned a mystery type the following can occur:
        //The room is a Shop            25%
        //The room is a rest            25%
        //The room is a common battle   25%
        //The room is a special event   25%
        //The player doesn't know the room type until they enter the room (the icon remains a '?')
        if(t == Type.Mystery)
        {
            int rng = Random.Range(0,100);
            if(rng < 25)
                type = Type.Shop;
            else if(rng < 50)
                type = Type.Rest;
            else if(rng < 75)
                type = Type.Battle;
            else
                type = t;
        }
        else
            type = t;          
    }

    //Returns the room icon corresponding to the given type
    private static Sprite getRoomIcon(Type type)
    {
        foreach(GameAssets.RoomIcon roomIcon in GameAssets.i.roomIconArray)
        {
            if(roomIcon.type == type)
            {
                return roomIcon.icon;
            }
        }
        //Debug.Log("Buff type " + type + " not found!");
        return null;
    }
}
