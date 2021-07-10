using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float _firstLine;
	[SerializeField] private float _secondLine;
	[SerializeField] private float _thirdLine;

	[SerializeField] private float _moveThreshold;
	[SerializeField] private float _speed;
	[SerializeField] private float _moveSpeed;

	private float _lastMoveTime;
	private Rigidbody _rigidbody;
	private bool _hitted;
	private Vector3 moveTo;
	public bool levelEnded;
	public GameObject endLevelScreen;

	//creating lanes. three lanes to move left and right. start at middle. 
	enum Lane
	{
		First,
		Second,
		Third
	}

	//our beginning lane.
	private Lane _lane = Lane.Second;



	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	//moving the character to right lane middle lane or left lane with touch counts to playable for simulator
	private void Update()
	{

		if (Input.touchCount > 0)
		{
			Touch touch = Input.touches[0];
			float movePow = touch.deltaPosition.normalized.x;
			if (Mathf.Abs(movePow) > _moveThreshold && Time.time - _lastMoveTime > 0.5f)
			{
				_lastMoveTime = Time.time;
				if (movePow < 0)
				{
					switch (_lane)
					{
						case Lane.First:
							break;
						case Lane.Second:
							//transform.position += new Vector3(_firstLine, 0, 0);
							moveTo = new Vector3(_firstLine, transform.position.y, transform.position.z);
							_lane = Lane.First;
							break;
						case Lane.Third:
							moveTo = new Vector3(_secondLine, transform.position.y, transform.position.z);
							_lane = Lane.Second;
							break;
					}
				}

				if (movePow > 0)
				{
					switch (_lane)
					{
						case Lane.First:
							moveTo = new Vector3(_secondLine, transform.position.y, transform.position.z);
							_lane = Lane.Second;
							break;
						case Lane.Second:
							moveTo = new Vector3(_thirdLine, transform.position.y, transform.position.z);
							_lane = Lane.Third;
							break;
						case Lane.Third:
							break;
					}
				}
			}
		}
		Move(moveTo);

	}
	//if player is alive and game started move charachter forward
	private void FixedUpdate()
	{
		if (!_hitted && PlayerManager.isGameStarted)
			_rigidbody.velocity = transform.forward * (Time.deltaTime * _moveSpeed);
	}

	//if you trigger a coin object tagged with coin destroy them and increase you coin score
	private void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.tag == "Coin")
		{
			Destroy(other.gameObject);
			PlayerManager.score += 1;
		}


	}

	//if character collides with finish lane cube end level and open next level panel to go next level. else if character hit gameobject that tagged with bariccade kill the charachter and show game over panel to start again
    private void OnCollisionEnter(Collision other)
    {
		if (other.gameObject.tag == "FinishLine")
		{
			levelEnded = true;
			endLevelScreen.SetActive(true);
		}
		if(other.gameObject.tag == "Barricade")
        {
			_hitted = true;
			PlayerManager.gameOver = true;
		}
    }

	//move character 
    private void Move(Vector3 moveTo)
	{

		moveTo = new Vector3(moveTo.x, transform.position.y, transform.position.z);
		transform.position = Vector3.MoveTowards(transform.position, moveTo, Time.deltaTime * _speed);
		_hitted = false;
	}

}