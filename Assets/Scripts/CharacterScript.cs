using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody playerRb;

    void Start()
    {
        player = GameObject.Find("CharacterPlayer");
        playerRb = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        this.transform.position = player.transform.position;
        player.transform.localPosition = Vector3.zero;
        /*
        if (playerRb.linearVelocity.magnitude > 0.1f)
        {
            float v = Vector3.SignedAngle(
                playerRb.linearVelocity,
                Vector3.forward,
                Vector3.up);
            if (v != 0)
            {
                Debug.Log(v);
            }
            this.transform.eulerAngles = new Vector3(0, v, 0);
            player.transform.eulerAngles = new Vector3(
                player.transform.eulerAngles.x,
                player.transform.eulerAngles.y - v,
                player.transform.eulerAngles.z);
        }*/
    }
}