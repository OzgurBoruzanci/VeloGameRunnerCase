using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GroundController : MonoBehaviour
{
    private ObstaclesController obstaclesController;
    private void OnEnable()
    {
        EventManager.RepositionGround += Reposition;
    }
    private void OnDisable()
    {
        EventManager.RepositionGround -= Reposition;
    }
    void Start()
    {
        obstaclesController=GetComponentInChildren<ObstaclesController>();
    }

    private void Reposition()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 200);
        transform.position=pos;
        obstaclesController.Reposition();
    }
}
