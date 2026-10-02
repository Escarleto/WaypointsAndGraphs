using System.Collections.Generic;
using UnityEngine;

public class Nodes
{
    public List<Edge> edgeList = new List<Edge>();
    public Nodes path = null;
    GameObject id;
    //public float xPos, yPos, zPos;

    public float f, g, h;
    public Nodes cameFrom;

    public Nodes(GameObject i)
    {
        id = i;
        /*
        xPos = i.transform.position.x;
        yPos = i.transform.position.y;
        zPos = i.transform.position.z;
        */
        path = null;
    }

    public GameObject getId()
    {
        return id;
    }
}
