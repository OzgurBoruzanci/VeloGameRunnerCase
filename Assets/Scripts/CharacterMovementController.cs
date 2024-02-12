using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using Lean.Touch;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    float horizontal = 0;
    float placeWidth = 2;
    public float speed;
    //float placeLenght = 40;
    bool speedStop;

    //public GameObject place;
    //
    private bool _swiped = false;
    private Vector2 _startSwipePosition;
    private float _minSwipeMagnitude = 50f; // Swipe i�in minimum hareket mesafesi
    //private enum SwipeDirection { None, Left, Right}
    //private SwipeDirection playerDirection = SwipeDirection.None;


    private void OnEnable()
    {
        LeanTouch.OnFingerSwipe += HandleSwipe;
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerSwipe -= HandleSwipe;
    }

    private void HandleSwipe(LeanFinger finger)
    {
        if (_swiped) return;

        // Swipe hareket vekt�r�n� al
        Vector2 swipeVector = finger.SwipeScreenDelta;

        // E�er swipe vekt�r�, belirlenen minimum swipe mesafesini ge�iyorsa
        if (swipeVector.magnitude > _minSwipeMagnitude)
        {
            // Yatay (X) y�nde bir swipe ise
            if (Mathf.Abs(swipeVector.x) > Mathf.Abs(swipeVector.y))
            {
                if (swipeVector.x > 0) // Sa�a swipe
                {
                    if (transform.position.x >=0)
                    {
                        MouseControl(-1.5f);
                        //playerDirection = SwipeDirection.Right;
                    }
                }
                else // Sola swipe
                {
                    if (transform.position.x <= 0)
                    {
                        MouseControl(1.5f);
                        //playerDirection = SwipeDirection.Right;
                    }
                }
            }
        }
    }
    void SpeedRegulation()
    {
        speedStop = true;
    }
    float SpeedStop()
    {
        if (speedStop == true)
        {
            return speed = 0;
        }
        else
        {
            return speed = 1.5f;
        }
    }

    void Update()
    {
        MControl();
    }

    void MControl()  // Hreket muhabbeti rigitbody ile yap�lmal� bu kullan��l� de�il
    {
        //horizontal = Input.GetAxis("Mouse X");
        Vector3 vec = new Vector3(transform.position.x, 0, SpeedStop());
        vec = transform.TransformDirection(vec);
        vec.Normalize();
        transform.position += vec * Time.deltaTime * 5f;
    }
    void MouseControl(float distance)
    {
        transform.position += Vector3.right * distance;
    }
}
