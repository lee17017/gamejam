using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;

public class PlayerPortrait : MonoBehaviour {
	public Image image;
	private Role role = Role.None;
	public Sprite[] roleImages;

	void Start() {
		role = Role.None;
		image = this.GetComponentInParent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(1)) {
			SetRole (Role.Move);
		}
	}

	public void SetRole(Role role) {
		this.role = role;
		image = this.GetComponentInParent<Image> ();
		image.color = Color.white;
		image.sprite = roleImages [(int)role];
	}

	public Role getRole() {
		return role;
	}
}
