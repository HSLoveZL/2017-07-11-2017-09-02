using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GazeController : MonoBehaviour {

	/// <summary>
	/// 准星容器
	/// </summary>
	public Canvas reticleCanvas;
	/// <summary>
	/// 准星图片
	/// </summary>
	public Image reticleImage;
	/// <summary>
	/// 击中的物体
	/// </summary>
	private GameObject target;
	/// <summary>
	/// 初始准星位置
	/// </summary>
	private Vector3 originPos;

	// Use this for initialization
	void Start () {
		originPos = reticleCanvas.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 100))
		{
			reticleCanvas.transform.position = hit.point;

		}
		else
		{
			reticleCanvas.transform.localPosition = originPos;
		}
	}
}
