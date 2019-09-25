using UnityEngine;

public class Flag : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Trigger");
        if (col.gameObject.tag == "Player")
        {
            GetComponent<Collider2D>().enabled = false;
            var entity = col.gameObject.GetComponent<Entity>();
            Game.I.Messages.SendEvent(new TakeFlagMsg{EntityId = entity.Id});
        }
    }
}