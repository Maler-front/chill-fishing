using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }

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


    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("Singleton error (in PlayerInput class)!");
        
        Instance = this;
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    {
                        _firstTouch = touch.position;
                        _touchPreviousPosition = touch.position;
                        OnScreenTouched?.Invoke(this, new OnFingerMoovingEventArgs{ 
                            firstTouch = _firstTouch,
                            movedFrom = _firstTouch,
                            movedTo = _firstTouch
                        });
                        break;
                    }
                case TouchPhase.Moved:
                    {
                        OnFingerMoved?.Invoke(this, new OnFingerMoovingEventArgs {
                            firstTouch = _firstTouch,
                            movedFrom = _touchPreviousPosition,
                            movedTo = touch.position 
                        });

                        _touchPreviousPosition = touch.position;
                        break;
                    }
                case TouchPhase.Ended:
                    {
                        OnScreenUntouched?.Invoke(this, new OnFingerMoovingEventArgs
                        {
                            firstTouch = _firstTouch,
                            movedFrom = _touchPreviousPosition,
                            movedTo = touch.position
                        });
                        break;
                    }
            }
        }
    }
}
