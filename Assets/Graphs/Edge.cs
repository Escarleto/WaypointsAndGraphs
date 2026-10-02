using UnityEngine;

public class Edge {

    public Nodes startNode;
    public Nodes endNode;

    public Edge(Nodes from, Nodes to) { 
    
        startNode = from;
        endNode = to;
    }
}