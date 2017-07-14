using System;
using System.IO;
using System.Text;
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

		FileStream f = new FileStream(@"D:\UnityConsole\test.csv", FileMode.OpenOrCreate, FileAccess.Write);
		StreamWriter sw = new StreamWriter(f);
		sw.BaseStream.Seek(0, SeekOrigin.End);
		sw.WriteLine(Environment.NewLine);

		byte[] inputTime = Encoding.UTF8.GetBytes(
			DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss\r\n"));
		f.Position = f.Length;//在文本的末尾追加字符
		f.Write(inputTime, 0, inputTime.Length);
		f.Close();

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
			using (FileStream fileW = new FileStream(@"D:\UnityConsole\test.csv", FileMode.OpenOrCreate, FileAccess.Write))
			{
				StreamWriter sw = new StreamWriter(fileW);
				sw.BaseStream.Seek(0, SeekOrigin.End);
				sw.WriteLine(Environment.NewLine);

				string coordinate = string.Format("{0:f6},{1:f6},{2:f6}, {3}\n", hit.point.x, hit.point.y, hit.point.z, DateTime.Now.ToString("HH:mm:ss"));
				byte[] data = Encoding.UTF8.GetBytes(coordinate);
				fileW.Position = fileW.Length;
				fileW.Write(data, 0, data.Length);
			}

			Debug.Log("<color=#50cccc>" + "Coordinate：" + hit.point.ToString("f6") + "</color>" + "    "
					+ "<color=#a7311a>" + DateTime.Now.ToString("HH:mm:ss") + "</color>");

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
