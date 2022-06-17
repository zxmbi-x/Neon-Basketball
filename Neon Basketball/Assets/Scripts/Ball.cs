using UnityEngine;

public class Ball : MonoBehaviour {
	// camera is used to get position in world instead of screen
	Camera cam;

	public float force = 4f;
	public Trajectory trajectory;

	private Rigidbody2D ball;
	private Vector2 start;
	private Vector2 end;
	private Vector2 direction;
	private Vector2 power;
	private float distance;
	private bool dragging;

	private Vector3 ballPosition { 
		get { return transform.position; } 
	}

	private float ballScorePosition;
	private Vector3 initial;

	void Awake() {
		ball = GetComponent<Rigidbody2D>();
	}

	void Start() {
		cam = Camera.main;
		initial = transform.position;
	}

	void Update() {
		if(Input.GetMouseButtonDown(0) && !ball.isKinematic) {
			dragging = true;

			start = cam.ScreenToWorldPoint(Input.mousePosition);
			trajectory.ShowTrajectory();
		}

		if(Input.GetMouseButtonUp (0)) {
			dragging = false;

			ball.AddForce(power, ForceMode2D.Impulse);
			trajectory.HideTrajectory();
		}

		if(dragging) {
			end = cam.ScreenToWorldPoint(Input.mousePosition);

			distance = Vector2.Distance(start, end);
			direction = start - end;
			power = force * distance * direction;

			trajectory.UpdateDots(this.ballPosition, power);
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		ballScorePosition = transform.position.y;
	}

	void OnTriggerExit2D(Collider2D collider) {
		if(transform.position.y < ballScorePosition) {
			GameManager.instance.AddTime();
			GameManager.instance.AddScore();
			GameManager.instance.MakeFaster();

			transform.position = initial;
		}
	}
	
}
