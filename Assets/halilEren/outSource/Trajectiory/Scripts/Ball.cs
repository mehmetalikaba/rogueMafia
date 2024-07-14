
using UnityEngine;

public class Ball : MonoBehaviour
{
	bool firladi;
	public oyuncuHareket oyuncuKonum;
	[HideInInspector] public Rigidbody2D rb;
	[HideInInspector] public CircleCollider2D col;

	[HideInInspector] public Vector3 pos { get { return transform.position; } }

	void Awake ()
	{
		rb = GetComponent<Rigidbody2D> ();
		col = GetComponent<CircleCollider2D> ();
        oyuncuKonum = FindObjectOfType<oyuncuHareket>();
	}
    private void LateUpdate()
    {
		if(!firladi)
		{
            transform.position = oyuncuKonum.transform.position;

        }
    }
    public void Push (Vector2 force)
	{
		rb.AddForce (force, ForceMode2D.Impulse);
	}

	public void ActivateRb ()
	{
		rb.isKinematic = false;
		firladi = true;
	}

	public void DesactivateRb ()
	{
		rb.velocity = Vector3.zero;
		rb.angularVelocity = 0f;
		rb.isKinematic = true;
	}
}
