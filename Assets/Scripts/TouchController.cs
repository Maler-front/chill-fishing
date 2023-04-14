using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchController : MonoBehaviour
{
    public static TouchController Instance { get; private set; }

    public bool ScreenTouched { get; private set; }
    private Vector2 _firstTouch;
    private Vector2 _touchPreviousPosition;

    public EventHandler<OnFingerMoovingEventArgs> OnScreenUntouched;
    public EventHandler<OnFingerMoovingEventArgs> OnScreenTouched;
    public EventHandler<OnFingerMoovingEventArgs> OnFingerMoved;
    public class OnFingerMoovingEventArgs : EventArgs
    {
        public Vector2 firstTouch;
        public Vector2 movedFrom;
        public Vector2 movedTo;
    }

    public void CreateInstance()
    {
        if (Instance != null)
            Debug.LogError("Singleton error (in TouchController class)!");

        Instance = this;
    }

    public void AnalizeTouch()
    {
        if (Input.touchCount > 0)
        {
            foreach(Touch touch in Input.touches)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    return;
                }  
            }

            Touch currenTouch = Input.GetTouch(0);

            switch (currenTouch.phase)
            {
                case TouchPhase.Began:
                    {
                        _firstTouch = currenTouch.position;
                        _touchPreviousPosition = currenTouch.position;
                        OnScreenTouched?.Invoke(this, new OnFingerMoovingEventArgs
                        {
                            firstTouch = _firstTouch,
                            movedFrom = _firstTouch,
                            movedTo = _firstTouch
                        });
                        break;
                    }
                case TouchPhase.Moved:
                    {
                        OnFingerMoved?.Invoke(this, new OnFingerMoovingEventArgs
                        {
                            firstTouch = _firstTouch,
                            movedFrom = _touchPreviousPosition,
                            movedTo = currenTouch.position
                        });

                        _touchPreviousPosition = currenTouch.position;
                        break;
                    }
                case TouchPhase.Ended:
                    {
                        OnScreenUntouched?.Invoke(this, new OnFingerMoovingEventArgs
                        {
                            firstTouch = _firstTouch,
                            movedFrom = _touchPreviousPosition,
                            movedTo = currenTouch.position
                        });
                        break;
                    }
            }
        }
    }
}
