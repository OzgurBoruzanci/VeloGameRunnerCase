using DG.Tweening;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

public class CharacterMovementController : MonoBehaviour
{
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    private bool isSwipe = false;

    [SerializeField]
    private float minDistanceForSwipe = 20f;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float moveDistance = 1f;
    public bool IsMovement { get; set; }
    private void Update()
    {
        Movement();
        MouseController();
        TouchedController();
    }
    private void Movement()
    {
        if (IsMovement)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.1f);
        }
    }
    private void MouseController()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fingerUpPosition = Input.mousePosition;
            fingerDownPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            fingerDownPosition = Input.mousePosition;
            DetectSwipe();
        }
    }
    private void TouchedController()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                fingerUpPosition = touch.position;
                fingerDownPosition = touch.position;
                isSwipe = true;
            }

            if (touch.phase == TouchPhase.Ended && isSwipe)
            {
                fingerDownPosition = touch.position;
                DetectSwipe();
            }
        }
    }
    private void DetectSwipe()
    {
        if (SwipeDistanceCheck() && SwipeDirectionCheck() == Direction.Right)
        {
            MoveHorizontal(1.5f);
        }
        else if (SwipeDistanceCheck() && SwipeDirectionCheck() == Direction.Left)
        {
            MoveHorizontal(-1.5f);
        }
        else if (SwipeDistanceCheck() && SwipeDirectionCheck() == Direction.Up)
        {
            Jump();
        }
        else if (SwipeDistanceCheck() && SwipeDirectionCheck() == Direction.Down)
        {
            animator.SetTrigger("Slide");
        }
    }

    private bool SwipeDistanceCheck()
    {
        return Vector2.Distance(fingerDownPosition, fingerUpPosition) > minDistanceForSwipe;
    }

    private Direction SwipeDirectionCheck()
    {
        Vector2 distance = fingerDownPosition - fingerUpPosition;
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;

        if (angle < 0)
            angle += 360;

        if (angle >= 45 && angle < 135)
            return Direction.Up;
        else if (angle >= 135 && angle < 225)
            return Direction.Left;
        else if (angle >= 225 && angle < 315)
            return Direction.Down;
        else
            return Direction.Right;
    }
    private void Jump()
    {
        Vector3 originalPosition = transform.position;
        Sequence jump = DOTween.Sequence();
        jump.Append(transform.DOMoveY(originalPosition.y + 2f, 0.43f).SetEase(Ease.OutQuad));
        jump.Append(transform.DOMoveY(originalPosition.y, 0.43f).SetEase(Ease.InQuad));
    }
    private void MoveHorizontal(float distance)
    {
        float newX = Mathf.Clamp(transform.position.x + distance, -1.5f, 1.5f); // Yeni X pozisyonunu -1.5 ile 1.5 arasında sınırla
        Vector3 vec = new Vector3(newX, transform.position.y, transform.position.z); // Yeni konum vektörünü oluştur
        Vector3 originalPosition = transform.position;
        Sequence jumpSequence = DOTween.Sequence();
        jumpSequence.Append(transform.DOMoveY(originalPosition.y + 0.2f, 0.1f).SetEase(Ease.OutQuad));
        jumpSequence.Append(transform.DOMove(vec, 0.1f));
        jumpSequence.Append(transform.DOMoveY(originalPosition.y, 0.1f).SetEase(Ease.InQuad));
    }
    public void PlayGame()
    {
        IsMovement=true;
        animator.SetBool("Idle", false);
    }
    public void GameOver()
    {
        IsMovement = false;
        animator.SetBool("Idle", true);
    }
}
public enum Direction
{
    Up,
    Down,
    Left,
    Right
}