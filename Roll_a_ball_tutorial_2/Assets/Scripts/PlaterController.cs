using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlaterController : MonoBehaviour {

	public float speed;
    public Text countText;
    public Text winText;

	private Rigidbody rb;
    private int count;

	public int jumpForce;

	bool canJump = true;

    Vector3 currentCheckpoint = new Vector3(0, 0.5f, 0);

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
    }

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);

		if (Input.GetKeyDown (KeyCode.Space)) {
			Jump ();
		}
	}

	void Jump()
	{
		if (canJump) {
			rb.AddForce (Vector3.up * jumpForce);
			canJump = false;
		}
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("Killzone"))
        {
            transform.position = currentCheckpoint;
        }
        
    }

	void OnCollisionEnter(Collision other)
	{
		canJump = true;
	}

    void SetCountText ()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winText.text = "You Win!";
        }
    }

}
