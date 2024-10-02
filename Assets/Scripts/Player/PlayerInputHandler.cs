using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerInput _playerInput;

    [SerializeField]
    private PlayerMove _playerMove;

    [SerializeField]
    private PlayerAim _playerAim;

    [SerializeField]
    private PlayerFire _playerFire;

    [SerializeField]
    private Camera _camera;

    private void OnEnable()
    {
        if (_camera == null) _camera = Camera.main;

        _playerInput.actions.FindAction("Move").performed += HandleMove;
        _playerInput.actions.FindAction("Move").canceled += StopMove;

        _playerInput.actions.FindAction("Aim").performed += HandleAim;

        _playerInput.actions.FindAction("Fire").performed += BeginFire;
        _playerInput.actions.FindAction("Fire").canceled += StopFire;
    }

    private void OnDisable()
    {
        _playerInput.actions.FindAction("Move").performed -= HandleMove;
        _playerInput.actions.FindAction("Move").canceled -= StopMove;

        _playerInput.actions.FindAction("Aim").performed -= HandleAim;

        _playerInput.actions.FindAction("Fire").performed -= BeginFire;
        _playerInput.actions.FindAction("Fire").canceled -= StopFire;
    }

    private void HandleMove(InputAction.CallbackContext ctx)
    {
        _playerMove.HandleMove(ctx.ReadValue<Vector2>());
    }

    private void StopMove(InputAction.CallbackContext ctx)
    {
        _playerMove.HandleMove(Vector2.zero);
    }

    private void HandleAim(InputAction.CallbackContext ctx)
    {
        _playerAim.HandleAim(_camera.ScreenToWorldPoint(ctx.ReadValue<Vector2>()));
    }

    private void BeginFire(InputAction.CallbackContext ctx) 
    {
        _playerFire.StartFiring();
    }

    private void StopFire(InputAction.CallbackContext ctx)
    {
        _playerFire.StopFiring();
    }
}
