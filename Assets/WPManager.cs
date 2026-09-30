using UnityEngine;

[System.Serializable]
public struct Link
{
    public enum direction { UNI, BI }
    public GameObject node1;
    public GameObject node2;
    public direction dir;
}

public class WPManager : MonoBehaviour
{
    [SerializeField] GameObject[] wayPoints;
    [SerializeField] Link[] Links;
    public Graphs graph = new Graphs();

    private void Start()
    {
        if (wayPoints.Length > 0)
        {
            foreach (GameObject wp in wayPoints)
            {
                graph.AddNode(wp);
            }
            foreach (Link l in Links)
            {
                graph.AddEdge(l.node1, l.node2);
                if (l.dir == Link.direction.BI)
                    graph.AddEdge(l.node2, l.node1);
            }            
        }
        //wayPoints = GameObject.FindGameObjectsWithTag("wp");
    }
}
