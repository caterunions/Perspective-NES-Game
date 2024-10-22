using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    private CastingHandler _castingHandler;

    [SerializeField]
    private SpellSelection _spellSelection;

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

        _playerInput.actions.FindAction("Cast").performed += StartCast;
        _playerInput.actions.FindAction("Cast").canceled += StopCast;

        _playerInput.actions.FindAction("Scroll").performed += HandleScroll;

        _playerInput.actions.FindAction("Quit").performed += HandleQuit;
    }

    private void OnDisable()
    {
        _playerInput.actions.FindAction("Move").performed -= HandleMove;
        _playerInput.actions.FindAction("Move").canceled -= StopMove;

        _playerInput.actions.FindAction("Aim").performed -= HandleAim;

        _playerInput.actions.FindAction("Fire").performed -= BeginFire;
        _playerInput.actions.FindAction("Fire").canceled -= StopFire;

        _playerInput.actions.FindAction("Cast").performed -= StartCast;
        _playerInput.actions.FindAction("Cast").canceled -= StopCast;

        _playerInput.actions.FindAction("Scroll").performed += HandleScroll;
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
        _playerAim.HandleAim(ctx.ReadValue<Vector2>());
    }

    private void BeginFire(InputAction.CallbackContext ctx) 
    {
        _playerFire.StartFiring();
    }

    private void StopFire(InputAction.CallbackContext ctx)
    {
        _playerFire.StopFiring();
    }

    private void StartCast(InputAction.CallbackContext ctx)
    {
        _castingHandler.StartCast();
    }

    private void StopCast(InputAction.CallbackContext ctx)
    {
        _castingHandler.StopCast();
    }

    private void HandleScroll(InputAction.CallbackContext ctx)
    {
        _spellSelection.HandleScroll(ctx.ReadValue<Vector2>().y);
    }

    private void HandleQuit(InputAction.CallbackContext ctx)
    {
        SceneManager.LoadScene("MainMenu");
    }
}
