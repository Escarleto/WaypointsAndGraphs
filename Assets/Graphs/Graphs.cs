using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

public class Graphs
{
    private List<Edge> edges = new List<Edge>();
    private List<Nodes> nodes = new List<Nodes>();
    private List<Nodes> pathList = new List<Nodes>();

    public Graphs()
    {
        
    }

    public void AddNode(GameObject id)
    {
        Nodes node = new Nodes(id);
        nodes.Add(node);
    }

    public void AddEdge(GameObject fromNode, GameObject toNode)
    {
        Nodes from = FindNode(fromNode);
        Nodes to = FindNode(toNode);

        if (from != null && to != null)
        {
            Edge e = new Edge(from, to);
            edges.Add(e);
            from.edgeList.Add(e);
        }
    }

    private Nodes FindNode(GameObject id)
    {
        foreach (Nodes n in nodes)
        {
            if (n.getId() == id)
                return n;
        }
        return null;
    }

    public bool AStar(GameObject startId, GameObject endId)
    {
        Nodes start = FindNode(startId);
        Nodes end = FindNode(endId);

        if (start == null || end == null)
            return false;
        
        List<Nodes> open = new List<Nodes>();
        List<Nodes> closed = new List<Nodes>();
        float tentativeGscore = 0;
        bool tentativeIsBetter;

        start.g = 0;
        start.h = Distance(start, end);
        start.f = start.h;

        open.Add(start);
        while (open.Count > 0)
        {
            int i = LowestF(open);
            Nodes thisNode = open[i];
            if (thisNode.getId() == endId)
            {
                ReconstructPath(start, end);
                return true;
            }
                
            open.RemoveAt(i);
            closed.Add(thisNode);
            Nodes neighbour;
            foreach (Edge e in thisNode.edgeList)
            {
                neighbour = e.endNode;

                if (closed.IndexOf(neighbour) > -1)
                    continue;
                
                tentativeGscore = thisNode.g + Distance(thisNode, neighbour);
                if (open.IndexOf(neighbour) == -1)
                {
                    open.Add(neighbour);
                    tentativeIsBetter = true;
                }
                else if (tentativeGscore < neighbour.g)
                    tentativeIsBetter = true;
                else
                    tentativeIsBetter = false;
                
                if (tentativeIsBetter)
                {
                    neighbour.cameFrom = thisNode;
                    neighbour.g = tentativeGscore;
                    neighbour.h = Distance(thisNode, end);
                    neighbour.f = neighbour.g + neighbour.h;
                }
            }
        }
        return false;
    }

    public void ReconstructPath(Nodes startId, Nodes endId)
    {
        pathList.Clear();
        pathList.Add(endId);

        var p = endId.cameFrom;
        while (p != startId && p == null)
        {
            pathList.Insert(0, p);
            p = p.cameFrom;
        }
        pathList.Insert(0, startId);
    }

    private float Distance(Nodes a, Nodes b)
    {
        return Vector3.SqrMagnitude(a.getId().transform.position - b.getId().transform.position);
    }

    private int LowestF(List<Nodes> l)
    {
        float lowestf = 0;
        int count = 0,  iteratorCount = 0;

        lowestf = l[0].f;

        for (int i = 1; i < l.Count; i++)
        {
            if (l[i].f < lowestf)
            {
                lowestf = l[i].f;
                iteratorCount = count;
            }
            count++;
        }
        return iteratorCount;
    }
}
