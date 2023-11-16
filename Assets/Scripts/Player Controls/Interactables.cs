using UnityEngine;

public class Interactables : MonoBehaviour
{
    public float radius = 1f;

    bool isFocus = false;
    Transform player;

    public void onFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
    }

    public void onDefocus()
    {
        isFocus = false;
        player = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void Update()
    {
        if (isFocus)
        {
            float distance = Vector2.Distance(player.position, transform.position);

            if (distance <= radius)
            {
                Debug.Log("Interacting");
            }
        }
    }

}
