using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwipeController : MonoBehaviour {

    private static SwipeController instance;
    public static SwipeController Instance { get { return instance; } }

    public SwipeDirection Direction { set; get; }

    public Vector3 firstTouch; // First touch position
    public Vector3 lastTouch;  // Last touch position
    private float dragDistance;
    public Vector3 deltaDirection;


    void Start() {
        instance = this;
        dragDistance = Screen.height * 15 / 100;
    }

    void Update()
    {
        Direction = SwipeDirection.None;

#if UNITY_EDITOR

        if (Input.GetMouseButtonDown(0))
        {
            firstTouch = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            lastTouch = Input.mousePosition;

            deltaDirection = firstTouch - lastTouch;
            CheckSwipe();
        }

#elif UNITY_ANDROID || UNITY_IOS

        if (Input.touchCount == 1) {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase) {
                case TouchPhase.Began:
                    firstTouch = touch.position;
                    break;

                case TouchPhase.Ended:
                    lastTouch = touch.position;

                    deltaDirection = firstTouch - lastTouch;
                    CheckSwipe();

                    break;
            }
        }

#endif
    }

    public bool IsSwiping(SwipeDirection dir) {
        return (Direction & dir) == dir;
    }

    private void CheckSwipe() {
        if (Mathf.Abs(lastTouch.x - firstTouch.x) > dragDistance ||
                        Mathf.Abs(lastTouch.y - firstTouch.y) > dragDistance) {
            if (Mathf.Abs(lastTouch.x - firstTouch.x) > Mathf.Abs(lastTouch.y - firstTouch.y)) {
                if (lastTouch.x > firstTouch.x) {
                    // Right swipe
                    Direction |= SwipeDirection.Right;
                } else {
                    // Left swipe
                    Direction |= SwipeDirection.Left;
                }
            } else {
                if (lastTouch.y > firstTouch.y) {
                    // Up swipe
                    Direction |= SwipeDirection.Up;
                }
                else {
                    // Down swipe
                    Direction |= SwipeDirection.Down;
                }
            }
        } else {
            Debug.Log("Tap");
        }
    }
}

public enum SwipeDirection {
    None = 0,
    Left = 1,
    Right = 2,
    Up = 4,
    Down = 8
} 