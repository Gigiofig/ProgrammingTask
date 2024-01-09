using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private bool interactable = false;
    private Vector2 movementInput;
    private Rigidbody2D rb;
    private NPCScript npc;
    public HUDScript hud;
    private Vector2 move;
    public float speed = 1f;
    public bool pressingMoveX = false;
    public bool pressingMoveY = false;
    public bool overrideX = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnPauseGame()
    {
        if (!hud.GetIsPaused() || hud.pausePanel.activeInHierarchy)
        {
            hud.PauseGame(!hud.GetIsPaused());
        }
    }

    public void OnInteract()
    {
        if (interactable)
        {
            npc.Interact();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementInput * speed * Time.fixedDeltaTime);
    }

    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        string tag = other.GetComponent<Collider2D>().tag;
        if (tag.Equals("NPC"))
        {
            npc = other.transform.GetComponent<NPCScript>();
            interactable = true;
        }
        else if (tag.Equals("SceneChanger"))
        {
            other.transform.GetComponent<SceneChanger>().ChangeScene();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        string tag = other.GetComponent<Collider2D>().tag;
        if (tag.Equals("NPC"))
        {
            npc = null;
            interactable = false;
        }
    }
}
