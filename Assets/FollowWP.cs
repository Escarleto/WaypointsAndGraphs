using UnityEngine;

public class FollowWP : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWP = 0;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotSpeed = 10f;

    private void Update()
    {
        if (Vector3.Distance(transform.position, waypoints[currentWP].transform.position) < 10)
            currentWP++;
        
        if (currentWP >= waypoints.Length)
            currentWP = 0;
        
        //transform.LookAt(waypoints[currentWP].transform);
        Quaternion lookatWP = Quaternion.LookRotation(waypoints[currentWP].transform.position - transform.position);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, lookatWP, rotSpeed * Time.deltaTime);
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
