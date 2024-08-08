using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CameraController : MonoBehaviour
{
    [SerializeField] private InputActionAsset _action;
    private InputAction _moving;
    private InputAction _zoom;

    private Vector2 _movement;

    private void Start()
    {
        _moving = _action.FindAction("Move");
        _moving.performed += moving =>
        {
            _movement = _moving.ReadValue<Vector2>();
        };

        _zoom = _action.FindAction("Zoom");
    }

    private void FixedUpdate()
    {
        if(_moving.IsInProgress())
        {
            Camera.main.transform.position = (Vector2)Camera.main.transform.position + _movement;
        }
    }

    private void Update()
    {
        Camera.main.orthographicSize += -Mouse.current.scroll.ReadValue().normalized.y * 3;
    }
}
