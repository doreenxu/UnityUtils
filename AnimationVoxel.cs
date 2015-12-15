using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class AnimationVoxel : MonoBehaviour {

	public enum SetupMethod{
		ASSIGN_FRAME,
		ASSIGN_CHILD_FRAME,
		CHILD_FRAME
	}
	public SetupMethod setupMethod;

	public GameObject[] frameObjects;
	public float framePerSecond;
	public enum AnimationType{
		LOOP, PINGPONG, ONCE, SWITCHABLE
	}
	public AnimationType animationType;
	private List<GameObject> framesActive = new List<GameObject>();
	private GameObject frameCurrentActive;	

	private int frameCurrentIndex;
	private float frameTimer;

	private bool setupComplete = false;
	private bool isPlay = false;
	public bool playOnStart = true;

	void Awake(){
		if(setupMethod == SetupMethod.ASSIGN_FRAME)
			SetupAssignFrame();
		else if(setupMethod == SetupMethod.CHILD_FRAME)
			SetupChildFrame();
		else if(setupMethod == SetupMethod.ASSIGN_CHILD_FRAME)
			SetupAssignChildFrame();

		isPlay = playOnStart;
		
	}

	void SetupAssignFrame(){
		int i=0;
		foreach(GameObject go in frameObjects){
			GameObject g = Instantiate(go, Vector3.zero, Quaternion.identity) as GameObject;
			g.transform.SetParent(transform);
			framesActive.Add(g);
			if(i != 0)
				g.SetActive(false);
			else
				frameCurrentActive = g;
			i++;
		}
		frameCurrentIndex = 0;
		setupComplete = true;
	}
	void SetupChildFrame(){
		int i = 0;
		foreach(Transform go in transform.Cast<Transform>()){
			framesActive.Add(go.gameObject);
			if(i != 0)
				go.gameObject.SetActive(false);
			else
				frameCurrentActive = go.gameObject;
			i++;
		}
		frameCurrentIndex = 0;
		if(transform.Cast<Transform>().Count() == 0){
			Debug.LogError("Child Empty. Make sure Frame Animation is available on childern");
		}else
			setupComplete = true;
	}
	void SetupAssignChildFrame(){
		int i = 0;
		foreach(GameObject go in frameObjects){
			framesActive.Add(go);
			if(i != 0){
				try{
					go.SetActive(false);
				}catch(Exception e){
					Debug.LogError(e + "Make sure Frame Objects (Inspector) is assigned with childern, not with on Assets Directory / Prefabs");
					goto end;
				}
			}
			else
				frameCurrentActive = go;
			i++;
		}
		frameCurrentIndex = 0;
		if(transform.Cast<Transform>().Count() == 0){
			Debug.LogError("Child Empty. Assign Frame Object with Child Object");
		}
		else if(frameObjects.Count() == 0){
			Debug.LogError("Frame Objects Empty. Assign Frame Object with Child Object");
		}
		setupComplete = true;
	end:
		if(!setupComplete)
			Debug.LogError("Make sure Frame Objects (Inspector) is assigned with childern, not with on Assets Directory / Prefabs");
		setupComplete = true;

	}

	void Update(){
		Animate();
	}

	private int indexIncremental = 1;
	void Animate(){
		if(setupComplete && isPlay && animationType != AnimationType.SWITCHABLE){
			frameTimer+= Time.deltaTime;
			if(frameTimer >= framePerSecond){
				frameCurrentActive.SetActive(false);
				frameCurrentActive = framesActive[frameCurrentIndex];
				frameCurrentActive.SetActive(true);
				frameCurrentIndex += indexIncremental;
				frameTimer = 0;
				if(animationType == AnimationType.LOOP && frameCurrentIndex >= framesActive.Count)
					frameCurrentIndex = 0;
				else if(animationType == AnimationType.PINGPONG){
					if(frameCurrentIndex >= framesActive.Count){
						frameCurrentIndex --;
						indexIncremental = -1;
					}
					else if(frameCurrentIndex <= 0){
						indexIncremental = 1;
					}
				}
				else if(animationType == AnimationType.ONCE && frameCurrentIndex >= framesActive.Count){
					isPlay = false;
					frameCurrentIndex = 0;
				}
			}
		}
	}
	public void SwitchFrame(int index){
		if(animationType == AnimationType.SWITCHABLE){
			if(index >= 0 && index < framesActive.Count){
				frameCurrentActive.SetActive(false);
				frameCurrentActive = framesActive[index];
				frameCurrentActive.SetActive(true);
			}
			else{
				Debug.LogError(index + " is out of range of Array FramesActive");
			}
		}
	}
}
