using UnityEngine;
using UnityEngine.Playables;

public class AngerCast : MonoBehaviour
{
    public FollowPlayer followPlayer;
    public GameObject HelpAngerText;
    public GameObject ZonaTrigAnger;
    public GameObject Cast;

    public PlayableDirector playableDirector;

    public GameObject _player;
    public bool _trigger;
    public bool _isPlayerInside;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player") && followPlayer.isFollowing == true)
        {
            _trigger = true;
            _player = collision.gameObject;
            _isPlayerInside = true;
            HelpAngerText.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
       
        if (other.CompareTag("Player") && followPlayer.isFollowing == true)
        {
            _trigger = false;
            _isPlayerInside = false;
            HelpAngerText.SetActive(false);
        }
    }
}