using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MakeMoreSlots : MonoBehaviour {//----------------------- THIS SCRIPT IS DEACTIVATED RIGHT NOW ---------
	GameObject[] ArrOfSpacs = null;//-------------------------a ttetntion to above message----------
								   // Use this for initialization//-------------------------a ttetntion to above message----------
	int CellSliderInt;
	public GameObject PoleObj;
	public GameObject SpaceObj;
	 GameObject TextObjForRowsNCols;
	void Awake()
    {
		CellSliderInt = (int)GameObject.Find("CellSlider").GetComponent<Slider>().value;
	}
	void Start ()
	{
		TextObjForRowsNCols = GameObject.Find("RNC_Text");
		CellSliderInt = (int)GameObject.Find("CellSlider").GetComponent<Slider>().value;
		//sources srcs = gameObject.GetComponent<sources>();
		//ArrOfSpacs = srcs.ArrPush (ArrOfSpacs, GameObject.FindGameObjectWithTag ("FinishUnit").gameObject);
		//ArrOfSpacs = srcs.ArrPush (ArrOfSpacs, GameObject.FindGameObjectWithTag ("StartUnit").gameObject);
		//GameObject[] FreeSpacs = GameObject.FindGameObjectsWithTag ("FreeSpace");
		//GameObject[] RockSpacs = GameObject.FindGameObjectsWithTag ("RockWAll");
		//	for (int i = 0; i < FreeSpacs.Length; i++) {
		//ArrOfSpacs = srcs.ArrPush (ArrOfSpacs, FreeSpacs[i]);
		//	}
		//for (int i = 0; i < RockSpacs.Length; i++) {
		//ArrOfSpacs = srcs.ArrPush (ArrOfSpacs, RockSpacs[i]);
		//	}
		//----------------- the Arr is full now ----------------------
		//for (int i = 0; i < ArrOfSpacs.Length; i++) {
		//ArrOfSpacs[i].name="NewBlock"+i;
		//Instantiate (ArrOfSpacs[i], new Vector3 (ArrOfSpacs[i].transform.position.x, ArrOfSpacs[i].transform.position.y, ArrOfSpacs[i].transform.position.z+12), Quaternion.identity);
		//}
	}
	
	// Update is called once per frame
	void Update () {
		TextObjForRowsNCols.GetComponent<Text>().text = CellSliderInt.ToString();
	}
	public void RemakeSlots()
    {
		CellSliderInt = (int)GameObject.Find("CellSlider").GetComponent<Slider>().value;
		sources srcs = gameObject.GetComponent<sources>();
		Destroy(GameObject.FindGameObjectWithTag("FinishUnit").gameObject);
		Destroy(GameObject.FindGameObjectWithTag("StartUnit").gameObject);
		GameObject[] FreeSpacs = GameObject.FindGameObjectsWithTag("FreeSpace");
		GameObject[] RockSpacs = GameObject.FindGameObjectsWithTag("RockWAll");
		for (int i = 0; i < FreeSpacs.Length; i++)
		{
			Destroy(FreeSpacs[i]);
		}
		for (int i = 0; i < RockSpacs.Length; i++)
		{
			Destroy(RockSpacs[i]);
		}
		Vector3 CenPos = PoleObj.transform.position;
		//----------------- the Arr is full now ----------------------
		for (int i = 0; i < CellSliderInt; i++)
		{
			float NewZPos = (CenPos.z-((CellSliderInt-1)*2)/2)+i*2;
			for (int j = 0; j < CellSliderInt; j++)
			{
				float NewXPos = (CenPos.x - ((CellSliderInt - 1) * 2) / 2) + j * 2;
				Vector3 NewCellPos = new Vector3(NewXPos, CenPos.y, NewZPos);
				GameObject nEWobj =  Instantiate(SpaceObj, NewCellPos, Quaternion.identity);
				nEWobj.name = "NewBlock" + i + ";" + j;
				nEWobj.tag = "FreeSpace";
				if (i == 0 && j == 0) nEWobj.tag = "StartUnit";
				if (i == CellSliderInt-1 && j == CellSliderInt-1) nEWobj.tag = "FinishUnit";
			}
		}
		StartCoroutine(rEC());
	}
	IEnumerator rEC()
    {
		yield return new WaitForSeconds(0.1f);
		IndicCon IC = GameObject.Find("PlatForm").GetComponent<IndicCon>();
		IC.ReColor();
		print("HEREEE");
	}
}
