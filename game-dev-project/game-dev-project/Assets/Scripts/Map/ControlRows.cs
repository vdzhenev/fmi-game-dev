using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlRows : MonoBehaviour
{
    //Nodes in a column
    [SerializeField] private List<Transform> nodes;

    //Sets the states of all nodes in the column to a given state
    public void setStates(Node.State state)
    {
        foreach(Transform n in nodes) 
        {
            n.GetComponent<Node>().setState(state);
        }
    }

    //Sets the types of all nodes in the column to a given type
    public void SetRoomsType(Node.Type t)
    {
        foreach(Transform n in nodes) 
        {
            n.GetComponent<Node>().setType(t);
        }
    }

    //Generates a random room type for each of the nodes in the column
    public void SetRandomType()
    {
        Node.Type t = Node.Type.Empty;
        foreach(Transform n in nodes) 
        {
            int rand = Random.Range(0, 100);
            switch (rand)
            {
                //Each room type has a % chance of occuring:
                //  Shop        10%
                //  Mystery     15%
                //  Rest        15%
                //  Battle      55%
                //  Elite       5%
                case int r when r <10:
                    t = Node.Type.Shop;
                    break;
                case int r when r <25:
                    t = Node.Type.Mystery;
                    break;
                case int r when r <40:
                    t = Node.Type.Rest;
                    break;
                case int r when r <95:
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
