using UnityEngine;

namespace Whatever // Since Unity 4.x
{
	// Execution order of methods : https://docs.unity3d.com/Manual/ExecutionOrder.html
	public class MonoBehaviourMethods : MonoBehaviour
	{
		void Awake()
		{
			Debug.Log("Awake");
        }

		void Start()
		{
			Debug.Log("Start");
		}

		void OnEnable()
		{
			Debug.Log("OnEnable");
		}

		void OnDisable()
		{
			Debug.Log("OnDisable");
		}

		void OnDestroy()
		{
			Debug.Log("OnDestroy");
		}

		// Main Loop
		void Update()
		{
			Debug.Log("Update : " + Time.deltaTime);

			if(Input.GetKeyUp(KeyCode.Space))
			{
				Debug.LogWarning("Space pressed !");
			}
        }

		void LateUpdate()
		{
			Debug.Log("LateUpdate : " + Time.deltaTime);
		}

		// Physics Loop
		void FixedUpdate()
		{
			Debug.Log("FixedUpdate : " + Time.fixedDeltaTime);
		}
	}
}