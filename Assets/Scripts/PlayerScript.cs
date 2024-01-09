using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private bool interactable = false
    private Vector2 movementInput;
    private Rigidbody2D rb;
    public HUDScript hud;
    private Vector2 move;
    private bool canMove = true;
    public float speed = 1f;
    public bool pressingMoveX = false;
    public bool pressingMoveY = false;
    public bool overrideX = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void PauseGame(InputAction.CallbackContext context)
    {
        if (context.started && (!hud.GetIsPaused() || hud.pausePanel.activeInHierarchy))
        {
            hud.PauseGame(!hud.GetIsPaused());
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementInput * speed * Time.fixedDeltaTime);
    }

    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        string tag = other.collider.tag;
        if tag.Equals("NPC")
        {
            interactable = true;
        }
        else if tag.Equals("SceneChanger")
        {
            other.ChangeScene();
        }
    }
}
