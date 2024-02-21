using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int healt;
    private int zPos = 250;
    private Vector3 firstPos;
    private CharacterMovementController characterMovement;
    private void OnEnable()
    {
        EventManager.PlayGame += PlayGame;
    }
    private void OnDisable()
    {
        EventManager.PlayGame -= PlayGame;
    }
    private void Start()
    {
        characterMovement = GetComponentInChildren<CharacterMovementController>();
        firstPos= transform.position;
    }
    private void PlayGame()
    {
        healt = 3;
        transform.position = firstPos;
        characterMovement.PlayGame();
    }
    private void Update()
    {
        CheckPosition();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<Obstacle>())
        {
            CheckObstaclePosition(collision.gameObject);
            CheckHealth();
        }
    }
    private void ObstacleCollisionHandler()
    {
        healt--;
        transform.localScale = Vector3.one;
        Vector3 hedefOlcek = new Vector3(0.5f, 0.5f, 0.5f);
        transform.DOScale(hedefOlcek, 0.5f).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutQuad);
        });
        
    }
    private void CheckObstaclePosition(GameObject obstacle)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1))
        {
            if (hit.transform.gameObject== obstacle)
            {
                GameManager.Instance.GameOver();
            }
        }
        else
        {
            RaycastHit hitLeft, hitRight;
            if (Physics.Raycast(transform.position + transform.right * 0.5f, transform.forward, out hitRight, 1))
            {
                if (hitRight.transform.gameObject == obstacle)
                {
                    ObstacleCollisionHandler();
                }
                
            }
            if (Physics.Raycast(transform.position - transform.right * 0.5f, transform.forward, out hitLeft, 1))
            {
                if (hitLeft.transform.gameObject == obstacle)
                {
                    ObstacleCollisionHandler();
                }
            }
        }
    }
    private void CheckHealth()
    {
        if (healt<=0)
        {
            GameManager.Instance.GameOver();
        }
    }
    private void CheckPosition()
    {
        if (transform.position.z>zPos)
        {
            EventManager.RepositionGround();
            zPos += 200;
        }
    }
}
