using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
	string label = "";
	float count;

	//IEnumerator Start()
	//{
		
	//	GUI.depth = 2;
	//	while (true)
	//	{
	//		if (Time.timeScale == 1)
	//		{
	//			yield return new WaitForSeconds(0.1f);
	//			count = (1 / Time.deltaTime);
	//			label = "FPS :" + (Mathf.Round(count));
	//		}
	//		else
	//		{
	//			label = "Pause";
	//		}
	//		yield return new WaitForSeconds(0.5f);
	//	}
	//}

    void OnGUI()
	{
		GUI.contentColor = Color.black;
		GUI.TextField(new Rect(0, 0, 200, 100), "Sprint-6 Build");
		
		GUI.skin.textField.fontSize = 50;
	}
}
