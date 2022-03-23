using UnityEngine;

public class ButtonTest : MonoBehaviour
{
	public GameObject cubesPrefab;
	public float waitTime = 10f;

	private float _elapsedTime = 0f;
	private MeshRenderer _meshRenderer;
	private bool _ready = true;

	private void Awake()
	{
		_meshRenderer = GetComponent<MeshRenderer>();
		_meshRenderer.material.color = Color.green;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(_ready && other.CompareTag("Player"))
		{
			GameObject.Instantiate(cubesPrefab, cubesPrefab.transform.position, cubesPrefab.transform.rotation);
			_meshRenderer.material.color = Color.red;
			_elapsedTime = 0f;
			_ready = false;
		}
	}

	private void Update()
	{
		if(!_ready)
		{
			_elapsedTime += Time.deltaTime;
			if (_elapsedTime >= waitTime)
			{
				_meshRenderer.material.color = Color.green;
				_ready = true;
			}
		}
	}
}
