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
	/// <summary>
	/// 保持准星大小
	/// </summary>
	private Vector3 originScale;

	// Use this for initialization
	void Start()
	{
		originPos = reticleCanvas.transform.localPosition;//准星初始位置
		originScale = reticleCanvas.transform.localScale;

		InvokeRepeating("GetDatas", 0.0f, 0.1f);
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	void GetDatas()
	{
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100))
		{
			reticleCanvas.transform.localPosition = hit.point;//获取准星碰撞点坐标
			Debug.Log(hit.point);
			reticleCanvas.transform.forward = hit.normal;//准星与物体法线方向保持一致
			reticleCanvas.transform.localScale = originScale * hit.distance;//保持准星大小一致
		}
		else
		{
			reticleCanvas.transform.localPosition = originPos;
			reticleCanvas.transform.forward = Camera.main.transform.forward;//返回场景中主摄像机的方向
			reticleCanvas.transform.localScale = originScale;
		}
	}
}
