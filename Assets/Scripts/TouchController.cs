using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchController : EntryLeaf
{
    public static TouchController Instance { get; private set; }

    public bool ScreenTouched { get; private set; }
    private Vector2 _firstTouch;
    private Vector2 _touchPreviousPosition;

    public Action<OnFingerMoovingEventArgs> OnScreenUntouched;
    public Action<OnFingerMoovingEventArgs> OnScreenTouched;
    public Action<OnFingerMoovingEventArgs> OnFingerMoved;
    public class OnFingerMoovingEventArgs
    {
        public Vector2 firstTouch;
        public Vector2 movedFrom;
        public Vector2 movedTo;
    }

    protected override void AwakeComponent()
    {
        if (Instance == null)
            Instance = this;

        base.AwakeComponent();
    }

    protected override void UpdateComponent()
    {
        AnalizeTouch();
        base.UpdateComponent();
    }

    public void AnalizeTouch()
    {
        if (Input.touchCount > 0 && !Pause.Instance.Paused)
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
                        OnScreenTouched?.Invoke(new OnFingerMoovingEventArgs
                        {
                            firstTouch = _firstTouch,
                            movedFrom = _firstTouch,
                            movedTo = _firstTouch
                        });
                        break;
                    }
                case TouchPhase.Moved:
                    {
                        OnFingerMoved?.Invoke(new OnFingerMoovingEventArgs
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
                        OnScreenUntouched?.Invoke(new OnFingerMoovingEventArgs
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

    private void OnDestroy()
    {
        Instance = null;
        base.DisableComponent();
    }
}
