using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlRows : MonoBehaviour
{
    [SerializeField] private List<Transform> nodes;

    public void setStates(Node.State state)
    {
        foreach(Transform n in nodes) 
        {
            n.GetComponent<Node>().setState(state);
        }
    }

    public void SetRoomsType(Node.Type t)
    {
        foreach(Transform n in nodes) 
        {
            n.GetComponent<Node>().setType(t);
        }
    }

    public void SetRandomType()
    {
        Node.Type t = Node.Type.Empty;
        foreach(Transform n in nodes) 
        {
            int rand = Random.Range(0, 100);
            switch (rand)
            {
                case int r when r <10:
                    t = Node.Type.Shop;
                    break;
                case int r when r <20:
                    t = Node.Type.Mystery;
                    break;
                case int r when r <40:
                    t = Node.Type.Rest;
                    break;
                case int r when r <90:
                    t = Node.Type.Battle;
                    break;
                case int r when r <100:
                    t = Node.Type.Elite;
                    break;
                default:
                    break;
            }
            n.GetComponent<Node>().setType(t);
        }
    }
}
