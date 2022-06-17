using UnityEngine;

public class Trajectory : MonoBehaviour {

	public GameObject dotParent;
	public GameObject dotPrefab;
	public SpriteRenderer dotOpacity;

	public int dotNumber;
	public float dotSpacing;

	private Transform[] dotList;
	private Vector2 pos;
	private float time;

	void Start() {
		HideTrajectory();

		float opacity = 1f;
		dotOpacity.color =  new Color(1f, 1f, 1f, opacity);
		dotList = new Transform[dotNumber];

		for(int i = 0; i < dotNumber; i++) {
			dotList [i] = Instantiate (dotPrefab, null).transform;
			dotList [i].parent = dotParent.transform;

			dotOpacity.color =  new Color(1f, 1f, 1f, opacity);
			opacity = opacity - 0.1f;	
		}
	}

	public void UpdateDots(Vector3 ballPosition, Vector2 power) {
		time = dotSpacing;
		
		for (int i = 0; i < dotNumber; i++) {
			pos.x = (ballPosition.x + power.x * time);
			pos.y = (ballPosition.y + power.y * time) - (Physics2D.gravity.magnitude * time * time) / 2f;
			dotList [i].position = pos;
			time += dotSpacing;
		}
	}

	public void ShowTrajectory() {
		dotParent.SetActive (true);
	}

	public void HideTrajectory() {
		dotParent.SetActive (false);
	}
}
