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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {

    }

    public void setState(State s) 
    {
        if(state != State.Visited)
        {    
            state = s;
            SpriteRenderer sr = transform.GetComponent<SpriteRenderer>();
            Button b = transform.GetComponent<Button>();
            switch (s)
            {
                case State.Locked:
                    sr.color = Color.gray;
                    b.interactable = false;
                    break;
                case State.Visited:
                    sr.color = Color.black;
                    b.interactable = false;
                    break;
                case State.Reachable:
                    sr.color = Color.black;
                    b.interactable = true;
                    break;
                default:
                    break;
            }
        }
    }

    public void setType(Type t)
    {
        type = t;
        gameObject.GetComponent<Image>().sprite = getRoomIcon(t);
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
