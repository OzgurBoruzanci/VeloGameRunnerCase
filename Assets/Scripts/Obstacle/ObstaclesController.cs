using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesController : MonoBehaviour
{
    [SerializeField] private List<GameObject> obstacles;
    [SerializeField] private int Zpos;
    
    private void Start()
    {
        CreateLevel();
    }
    private void CreateLevel()
    {
        for (int i = 0; i < 10; i++)
        {
            System.Random random = new System.Random();
            GameObject obstacle = obstacles[random.Next(obstacles.Count)];
            Instantiate(obstacle, new Vector3(transform.position.x, transform.position.y, Zpos), Quaternion.identity, transform);
            Zpos += UnityEngine.Random.Range(15, 25);
        }

    }
    public void Reposition()
    {
        List<Vector3> childsPos = new List<Vector3>();
        List<GameObject> childs = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            childsPos.Add(transform.GetChild(i).transform.localPosition);
        }

        foreach (var child in childs)
        {
            int listElement = UnityEngine.Random.Range(0, childsPos.Count);
            child.transform.localPosition = childsPos[listElement];
            childsPos.RemoveAt(listElement);
        }
    }
}
