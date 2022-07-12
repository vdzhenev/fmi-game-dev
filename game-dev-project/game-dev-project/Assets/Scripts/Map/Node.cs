using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public enum State 
    {
        Locked,
        Visited,
        Reachable
    }

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

    public void OnClick()
    {
        setState(State.Visited);
        mapManager.GetComponent<ControlColumns>().goNext();
        gameManager.LoadRoom(type);
    }

    public void setState(State s) 
    {
        if(state != State.Visited)
        {    
            state = s;
            Image sr = transform.GetComponent<Image>();
            Button b = transform.GetComponent<Button>();
            switch (s)
            {
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

    public void setType(Type t)
    {
        gameObject.GetComponent<Image>().sprite = getRoomIcon(t);
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
