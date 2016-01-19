using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooling : MonoBehaviour {

	public static ObjectPooling instance = null;

	public PoolingObj[] poolingTarget;
	private Dictionary<string, List<GameObject>> _dictPooled = new Dictionary<string, List<GameObject>>();
	private List<GameObject> _tempPooled = new List<GameObject>();

	[System.Serializable]
	public class PoolingObj{
		public GameObject objectTarget;
		public string key;
	}

	// Use this for initialization
	void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
	}

	void Start(){
		SetupDict ();
	}

	void SetupDict(){
		foreach (PoolingObj po in poolingTarget) {
			_dictPooled.Add (po.key, new List<GameObject> ());
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}

	public void CreateNewKey(string key){
		_dictPooled.Add (key, new List<GameObject> ());
	}

	public void AddObject(string key, GameObject gameObj, bool setActive){
		if (_dictPooled.ContainsKey (key)) {
			_dictPooled.TryGetValue (key, out _tempPooled);
			_tempPooled.Add (gameObj);
			_dictPooled [key] = _tempPooled;
			gameObj.SetActive(setActive);
		} else
			Debug.LogError ("Can't Add to Pooling System : '"+key+"' Not found");
	}
	public void AddObject(string key, GameObject gameObj){
		AddObject (key, gameObj, true);
	}
	public void AddObject(GameObject gameObj){
		AddObject (gameObj.name, gameObj);
	}

	public GameObject GetObject(string key){
		if(_dictPooled.TryGetValue(key, out _tempPooled)){
			if (_tempPooled.Count != 0) {
				GameObject go = _tempPooled [0].gameObject;
				_tempPooled.RemoveAt (0);
				_dictPooled [key] = _tempPooled;
				return go;
			}
			return null;
		}
		return null;
	}
}
