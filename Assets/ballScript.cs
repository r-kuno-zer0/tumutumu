﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ballScript : MonoBehaviour {
	
	public GameObject ballPrefab;
	public Sprite[] ballSprites;
	private GameObject firstBall;
	private GameObject lastBall;
	private string currentName;
	List<GameObject> removableBallList = new List<GameObject>();
	
	void Start () {
		StartCoroutine(DropBall(50));
	}
	
	void Update () {
			if (Input.GetMouseButtonDown (0) && firstBall == null) {
				OnDragStart ();
			}　else if (Input.GetMouseButtonUp (0)) {
				//クリックを終えた時
				OnDragEnd ();
			} else if (firstBall != null) {
				OnDragging ();
			}
	}
	
	private void OnDragStart() {
		RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
		
		if (hit.collider != null) {
			GameObject hitObj = hit.collider.gameObject;
			string ballName = hitObj.name;
			if (ballName.StartsWith ("Piyo")) {
				firstBall = hitObj;
				lastBall = hitObj;
				currentName = hitObj.name;
				removableBallList = new List<GameObject>();
				PushToList(hitObj);
			}
		}
	}
	
	private void OnDragging ()
	{
		RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
		if (hit.collider != null) {
			GameObject hitObj = hit.collider.gameObject;
			
			if (hitObj.name == currentName && lastBall != hitObj) {
				float distance = Vector2.Distance (hitObj.transform.position, lastBall.transform.position);
				if (distance < 1.0f) {
					lastBall = hitObj;
					PushToList (hitObj);
				}
			}
		}
	}
	
	private void OnDragEnd () {
		int remove_cnt = removableBallList.Count;
		if (remove_cnt >= 3) {
			for (int i = 0; i < remove_cnt; i++) {
				Destroy (removableBallList [i]);
			}
			//ボールを新たに生成
			StartCoroutine (DropBall (remove_cnt));
		}
		firstBall = null;
		lastBall = null;
	}
	
	IEnumerator DropBall(int count) {
		for (int i = 0; i < count; i++) {
			Vector2 pos = new Vector2(Random.Range(-2.0f, 2.0f), 7f);
			GameObject ball = Instantiate(ballPrefab, pos,
				Quaternion.AngleAxis(Random.Range(-40, 40), Vector3.forward)) as GameObject;
			int spriteId = Random.Range(0, 5);
			ball.name = "Piyo" + spriteId;
			SpriteRenderer spriteObj = ball.GetComponent<SpriteRenderer>();
			spriteObj.sprite = ballSprites[spriteId];
			yield return new WaitForSeconds(0.05f);
		}
	}
	
	void PushToList (GameObject obj) {
		removableBallList.Add (obj);
	}
}