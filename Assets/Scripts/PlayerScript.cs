using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private bool hasHat = false;
    private bool hasHair = true;
    private bool hasClothes = true;
    private bool interactable = false;
    private Vector2 movementInput;
    private Rigidbody2D rb;
    private NPCScript npc;
    public HUDScript hud;
    private Vector2 move;
    public float speed = 1f;
    public SpriteRenderer HairComponent;
    public SpriteRenderer HatComponent;
    public SpriteRenderer ClothesComponent;
    private Animator playerAnimator;


    void Start()
    {
        if (hasClothes)
        {
            ClothesComponent.enabled = true;
        }
        if (hasHat)
        {
            HatComponent.enabled = true;
        }
        if (hasHair)
        {
            HairComponent.enabled = true;
        }
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
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
            npc.OpenShop();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementInput * speed * Time.fixedDeltaTime);
    }

    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
        playerAnimator.SetFloat("Horizontal", movementInput.x);
        playerAnimator.SetFloat("Vertical", movementInput.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        string tag = other.GetComponent<Collider2D>().tag;
        if (tag.Equals("NPC"))
        {
            npc = other.transform.GetComponent<NPCScript>();
            npc.inAreaNotification.enabled = true;
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
            npc.inAreaNotification.enabled = false;
            npc = null;
            interactable = false;
        }
    }
}
