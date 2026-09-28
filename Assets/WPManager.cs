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

    private void Start()
    {
        wayPoints = GameObject.FindGameObjectsWithTag("wp");
    }
}
