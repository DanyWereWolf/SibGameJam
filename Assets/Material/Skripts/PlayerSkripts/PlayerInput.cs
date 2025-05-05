using System.Collections;
using UnityEngine;
[RequireComponent(typeof(PlayerMovment))]
public class PlayerInput : MonoBehaviour
{
    public FollowPlayer follow;
    public AngerCast angerCast;
    private Animator animator;
    private PlayerMovment playerMovment;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovment = GetComponent<PlayerMovment>();
    }
    private void Update()
    {
        float horizontalDirection = Input.GetAxis(GlobalStringVars.HORIZONTAL_AXIS);
        bool isJampButtonPressed = Input.GetButtonDown(GlobalStringVars.JUMP_BUTTON);

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Нажата клавиша DownArrow");
            Physics2D.IgnoreLayerCollision(14, 15, true);
            Invoke("IgnoreLayerOff", 0.2f);
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Нажата клавиша S");
            Physics2D.IgnoreLayerCollision(14, 15, true);
            Invoke("IgnoreLayerOff", 0.2f);
        }

        if (Input.GetKey(KeyCode.K) && angerCast._trigger == true)
        {
            angerCast.playableDirector.Play();
            Debug.Log("Гнев");
            angerCast.Cast.SetActive(true);
            Invoke("CastAnger", 2f);
        }
    }
    public void IgnoreLayerOff()
    {
        Physics2D.IgnoreLayerCollision(14, 15, false);
    }
    private void CastAnger()
    {
        if (angerCast._isPlayerInside && angerCast._player != null)
        {
            follow.destroy();
            Destroy(angerCast.ZonaTrigAnger);
            angerCast.HelpAngerText.SetActive(false);
            angerCast.Cast.SetActive(false);
        }
    }
}
