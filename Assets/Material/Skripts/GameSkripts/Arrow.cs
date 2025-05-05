using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Explosion explosion;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Box"))
        {
            //����� ��� ����������� �����
            explosion.TriggerExplosion(transform.position);
        }
    }
}
