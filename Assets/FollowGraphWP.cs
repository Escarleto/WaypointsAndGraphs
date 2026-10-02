using UnityEngine;

public class FollowGraphWP : MonoBehaviour
{
    private Transform goal;
    private float speed = 5f, accuracy = 1f, rotSpeed = 2f;

    [SerializeField] private GameObject wpManager;
    private GameObject[] wps;
    private GameObject currenteNode;
    private int currentWp = 0;
    private Graphs g;

    private void Start()
    {
        wps = wpManager.GetComponent<WPManager>().wayPoints;
        g = wpManager.GetComponent<WPManager>().graph;
        currenteNode = wps[0];
        
        //Invoke("GoToRuin", 2);
    }

    private void LateUpdate()
    {
        if (g.pathList.Count == 0 || currentWp == g.pathList.Count)
            return;
        
        if (Vector3.Distance(g.pathList[currentWp].getId().transform.position, transform.position) < accuracy)
        {
            currenteNode = g.pathList[currentWp].getId();
            currentWp++;
        }

        if (currentWp < g.pathList.Count)
        {
            goal = g.pathList[currentWp].getId().transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x, transform.position.y, goal.position.z);
            Vector3 direction = lookAtGoal - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }

    public void GoToHeli()
    {
        g.AStar(currenteNode, wps[13]);
        currentWp = 0;
    }

    public void GoToRuin()
    {
        g.AStar(currenteNode, wps[3]);
        currentWp = 0;
    }

    public void GoToOil()
    {
        g.AStar(currenteNode, wps[5]);
        currentWp = 0;
    }

    public void GoToFactory()
    {
        g.AStar(currenteNode, wps[6]);
        currentWp = 0;
    }
}
