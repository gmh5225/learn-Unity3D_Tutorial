using UnityEngine;

namespace CeQueJeVeux // Since Unity 4.x
{
	public class MonoBehaviourMethods : MonoBehaviour
	{
		//private int _count;

		void Awake()
		{
			Debug.Log("Awake");
			//_count = 0;
        }

		// Use this for initialization
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

		// Update is called once per frame
		void Update()
		{
			//Debug.Log("Update");
			//++_count;
			//Debug.Log(_count);
			Debug.Log("Dt : " + Time.deltaTime);
        }

		//void LateUpdate()
		//{
		//	//Debug.Log("LateUpdate");
		//	--_count;
		//	Debug.Log(_count);
		//}

		void FixedUpdate()
		{
			//Debug.Log("FixedUpdate");
			Debug.Log("Fixed Dt : " + Time.fixedDeltaTime);
		}
	}
}