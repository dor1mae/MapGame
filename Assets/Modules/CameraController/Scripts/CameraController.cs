using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;


public class CameraController : MonoBehaviour
{
    [SerializeField] private InputActionAsset _action;
    private InputAction _moving;
    private InputAction _zoom;
    private InputAction _swap;
    private InputAction _pointer;

    [SerializeField] private int _powerOfSwapping = 10;

    private Vector2 _mouseStartPos;
    private Vector2 _cameraStartPos;
    private Vector2 _offset;
    private Vector2 _movement;

    private void Start()
    {
        _moving = _action.FindAction("Move");
        _moving.performed += moving =>
        {
            _movement = _moving.ReadValue<Vector2>();
        };

        _pointer = _action.FindAction("Pointer");
        _swap = _action.FindAction("Swap");
        _swap.started += swaping =>
        {
            _mouseStartPos = Mouse.current.position.value;
            _cameraStartPos = Camera.main.transform.position;
        };
    }

    private void FixedUpdate()
    {
        if(_moving.IsInProgress())
        {
            Camera.main.transform.position = (Vector2)Camera.main.transform.position + _movement;
        }
        if (_swap.IsInProgress())
        {
            _offset = (_mouseStartPos - Mouse.current.position.value);
            Camera.main.transform.position = _cameraStartPos + _offset / _powerOfSwapping;
        }
    }

    private void Update()
    {
        Camera.main.orthographicSize += -Mouse.current.scroll.ReadValue().normalized.y * 3;
    }
}
