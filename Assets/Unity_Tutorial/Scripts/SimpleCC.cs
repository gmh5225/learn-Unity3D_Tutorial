using UnityEngine;

public class SimpleCC : MonoBehaviour
{
    public float translationSpeed = 5f; // in m/s
    public float rotationSpeed = 120f; // in deg/s
    public float runFactor = 2f;

    Vector3 _move = Vector3.zero;
    float _angle = 0f;
    private CharacterController _cc;

	private void Awake()
	{
        _cc = GetComponent<CharacterController>();
	}


	// Update is called once per frame
	void Update()
    {
        // Get Translation
        // V1
        //_move.z = 0f;
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //	_move.z = translationSpeed * Time.deltaTime;
        //}
        //else if (Input.GetKey(KeyCode.DownArrow))
        //{
        //	_move.z = -translationSpeed * Time.deltaTime;
        //}
        _move.z = Input.GetAxisRaw("Vertical") * translationSpeed * Time.deltaTime;

        if (Input.GetButton("Run"))
		{
            _move *= runFactor;
		}

        // Get Rotation
        _angle = 0f;
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    _angle = -rotationSpeed * Time.deltaTime;
        //}
        //else if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    _angle = rotationSpeed * Time.deltaTime;
        //}
        _angle = Input.GetAxisRaw("Horizontal") * rotationSpeed * Time.deltaTime;

        // Apply Rotation
        // V1
        transform.Rotate(0f, _angle, 0f);
        // V2
        //Vector3 euler = transform.eulerAngles;
        //euler.y += _angle;
        //transform.eulerAngles = euler;

        // Apply Translation
        // V1
        //transform.Translate(_move);
        // V2
        //Vector3 pos = transform.position;
        //pos += transform.rotation * _move;
        //transform.position = pos;
        // V3
        _cc.Move(transform.rotation * _move);
    }
}
