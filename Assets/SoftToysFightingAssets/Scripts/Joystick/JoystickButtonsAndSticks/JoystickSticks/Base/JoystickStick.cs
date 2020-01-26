using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.SoftToysFighting.Joysticks
{
    public class JoystickStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {

        public Vector2 Input = Vector2.zero;

        [SerializeField] private float handleRange = 1;
        [SerializeField] private float deadZone = 0;

        [SerializeField] protected RectTransform background = null;
        [SerializeField] private RectTransform handle = null;
        private RectTransform baseRect = null;

        private Canvas canvas;
        private Camera cam;

        private void Awake()
        {
            baseRect = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
            Vector2 center = new Vector2(0.5f, 0.5f);
            background.pivot = center;
            handle.anchorMin = center;
            handle.anchorMax = center;
            handle.pivot = center;
            handle.anchoredPosition = Vector2.zero;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            cam = null;
            if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
                cam = canvas.worldCamera;

            Vector2 position = RectTransformUtility.WorldToScreenPoint(cam, background.position);
            Vector2 radius = background.sizeDelta / 2;
            Input = (eventData.position - position) / (radius * canvas.scaleFactor);
            HandleInput(Input.magnitude, Input.normalized, radius, cam);
            handle.anchoredPosition = Input * radius * handleRange;
        }

        protected virtual void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
        {
            if (magnitude > deadZone)
            {
                if (magnitude > 1)
                    Input = normalised;
            }
            else
                Input = Vector2.zero;
        }



        public void OnPointerUp(PointerEventData eventData)
        {
            Input = Vector2.zero;
            handle.anchoredPosition = Vector2.zero;
        }

    }
}

