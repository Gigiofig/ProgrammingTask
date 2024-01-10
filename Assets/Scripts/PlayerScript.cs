using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private bool interactable = false;
    private Vector2 movementInput;
    private Vector2 lastInput;
    private Rigidbody2D rb;
    private NPCScript npc;
    private ColorNPCScript colorNPC;
    public HUDScript hud;
    private Vector2 move;
    public float speed = 1f;
    public SpriteRenderer HairComponent;
    public SpriteRenderer HatComponent;
    public SpriteRenderer ClothesComponent;
    public CharacterCustomization customClothes;
    public CharacterCustomization customHair;
    public CharacterCustomization customHat;
    public Sprite[] sprites;
    private Animator playerAnimator;
    string currentState;
    const string PLAYER_IDLE_UP = "IdlePlayerAnimUp";
    const string PLAYER_IDLE_DOWN = "IdlePlayerAnimDown";
    const string PLAYER_IDLE_LEFT = "IdlePlayerAnimLeft";
    const string PLAYER_IDLE_RIGHT = "IdlePlayerAnimRight";
    const string PLAYER_WALK_UP = "WalkPlayerAnimUp";
    const string PLAYER_WALK_DOWN = "WalkPlayerAnimDown";
    const string PLAYER_WALK_LEFT = "WalkPlayerAnimLeft";
    const string PLAYER_WALK_RIGHT = "WalkPlayerAnimRight";
    [SerializeField] private SceneInfo sceneInfo;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        if (sceneInfo.sceneNum == 1 && sceneInfo.isNextScene)
        {
            transform.position = new Vector3(-3.5f, 17.3f, 0f);
        }
        if (sceneInfo.hasClothes)
        {
            ClothesComponent.enabled = true;
        }
        if (sceneInfo.hasHat)
        {
            HatComponent.enabled = true;
        }
        if (sceneInfo.hasHair)
        {
            HairComponent.enabled = true;
        }
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
            if (npc != null) npc.OpenShop();
            else if (colorNPC != null) colorNPC.OpenShop();
        }
    }

    private void FixedUpdate()
    {
        if (movementInput.x > 0)
        {
            ChangeAnimationState(PLAYER_WALK_RIGHT);
            lastInput = new Vector2(1, 0);
        }
        else if (movementInput.x < 0)
        {
            ChangeAnimationState(PLAYER_WALK_LEFT);
            lastInput = new Vector2(-1, 0);
        }
        else if (movementInput.y > 0)
        {
            ChangeAnimationState(PLAYER_WALK_UP);
            lastInput = new Vector2(0, 1);
        }
        else if (movementInput.y < 0)
        {
            ChangeAnimationState(PLAYER_WALK_DOWN);
            lastInput = new Vector2(0, -1);
        }
        rb.MovePosition(rb.position + movementInput * speed * Time.fixedDeltaTime);
        if (movementInput == Vector2.zero)
        {
            if (lastInput.x > 0)
            {
                ChangeAnimationState(PLAYER_IDLE_RIGHT);
            }
            else if (lastInput.x < 0)
            {
                ChangeAnimationState(PLAYER_IDLE_LEFT);
            }
            else if (lastInput.y > 0)
            {
                ChangeAnimationState(PLAYER_IDLE_UP);
            }
            else
            {
                ChangeAnimationState(PLAYER_IDLE_DOWN);
            }
        }
        string spriteName = GetComponent<SpriteRenderer>().sprite.name;
        int pos = spriteName.LastIndexOf('_');
        spriteName = spriteName.Remove(0, pos + 1);
        int spriteNum = int.Parse(spriteName);
        customClothes.SetSkin(spriteNum);
        customHat.SetSkin(spriteNum);
        customHair.SetSkin(spriteNum);
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
            npc.inAreaNotification.enabled = true;
            interactable = true;
        }
        else if (tag.Equals("ColorNPC"))
        {
            colorNPC = other.transform.GetComponent<ColorNPCScript>();
            colorNPC.inAreaNotification.enabled = true;
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
        else if (tag.Equals("ColorNPC"))
        {
            colorNPC.inAreaNotification.enabled = false;
            colorNPC = null;
            interactable = false;
        }
    }

    void ChangeAnimationState(string newState)
    {
        if (newState == currentState) return;
        playerAnimator.Play(newState);
        currentState = newState;
    }
}
