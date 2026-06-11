using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Main : MonoBehaviour
{
    [Header("Input references")]
    [SerializeField] InputActionReference moveAction;
    [SerializeField] InputActionReference fireAction;

    [Header("References")]
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject bulletObject;
    [SerializeField] GameObject weaponObject;
    [SerializeField] GameObject enemyObject;

    [Header("Game settings")]
    [SerializeField] float playerSpeed = 0.1f;

    // Variables
    private Player player;
    private IUsable playerItem;
    private Enemy targetDummy;
    private EventManager eventManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventManager = new EventManager();

        // input handling
        moveAction.action.Enable();
        fireAction.action.started += triggerFire;

        InitializeGame();
    }

    private void triggerFire(InputAction.CallbackContext context)
    {
        playerItem.Use();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        // Update game
        EventManager.instance.SendUpdateTick();
        EventManager.instance.TickTimers(Time.fixedDeltaTime);

        var moveDirection = moveAction.action.ReadValue<Vector3>();
        player.Move(moveDirection);
    }

    private void InitializeGame()
    {
        player = new Player(
            playerObject, 
            playerSpeed, 
            new Vector3(0f, 0.5f, 0f), 
            100f, 
            new Vector3(0f,0f,0f));
        targetDummy = new Enemy(
            enemyObject, 
            0f, 
            new Vector3(0f, 0f, 0f), 
            999f,
            new Vector3(0f, 3f, 0f));
        var gun = new Gun(
            weaponObject, 
            bulletObject, 
            player, 
            player.heldItemPos);
        // decorate gun

        playerItem = gun;
    }
}
