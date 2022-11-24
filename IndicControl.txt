using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[ExecuteInEditMode]
public class IndicCon : MonoBehaviour
{
	public static bool ResetFreeCells = false;
	public Material FreeCell;
	public Material RockWall;
	public Material DeathTrap;
	public Material StartMat;
	public Material FinishMat;
	public Material MarkedPath;
	GameObject[] arr;
	GameObject StartObj;
	GameObject FinishObj;
	int br = 0;
	void Start()
	{//             chaning the colors of the different blocks
		arr = GameObject.FindGameObjectsWithTag("FreeSpace");//----------- for the free spaces
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i].GetComponent<MeshRenderer>().material = FreeCell;
		}
		arr = GameObject.FindGameObjectsWithTag("RockWAll");//------------ for the rocks
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i].GetComponent<MeshRenderer>().material = RockWall;
		}
		arr = GameObject.FindGameObjectsWithTag("DeathTrap");//------------ for the Deathtraps
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i].GetComponent<MeshRenderer>().material = DeathTrap;
		}
		FinishObj = GameObject.FindGameObjectWithTag("FinishUnit").gameObject;
		StartObj = GameObject.FindGameObjectWithTag("StartUnit").gameObject;
		StartObj.GetComponent<MeshRenderer>().material = StartMat;
		FinishObj.GetComponent<MeshRenderer>().material = FinishMat;
		//------------- END of color corrections --------------------
		//print("Shortest path length is:"+PathFinder.BestPath.Length);
	}
	void Update()
	{
		if (br < 2)
		{
			br = br + 1;
			if (br == 2)
			{//------------------------ executed ijn the second frame START
				for (int i = 0; i < PathFinder.BestPath.Length; i++)
				{
					if (PathFinder.BestPath[i] != StartObj && PathFinder.BestPath[i] != FinishObj)
					{
						PathFinder.BestPath[i].GetComponent<MeshRenderer>().material = FreeCell;
					}
				}
			}//-------------------00000000000000----- executed ijn the second frame END
			if (ResetFreeCells == true)
			{
				arr = GameObject.FindGameObjectsWithTag("FreeSpace");
				for (int i = 0; i < arr.Length; i++)
				{
					arr[i].gameObject.GetComponent<MeshRenderer>().material = FreeCell;
				}
				ResetFreeCells = false;
			}


			//-----------------------------------------------------------------------------

		}




		//---------------------------------------------------------------

		GameObject CurrSelectedOBJ=null;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, Mathf.Infinity))
		{
			CurrSelectedOBJ= hit.collider.gameObject;
		}
        if (CurrSelectedOBJ != null)
        {
			if(CurrSelectedOBJ.tag== "FreeSpace" || CurrSelectedOBJ.tag == "StartUnit" || CurrSelectedOBJ.tag == "FinishUnit" || CurrSelectedOBJ.tag == "RockWAll")
            {
				if (true)
                {
                    if (Input.GetKey("1"))
                    {
						CurrSelectedOBJ.tag = "FreeSpace";
						ReColor();
					}
					if (Input.GetKey("2"))
					{
						CurrSelectedOBJ.tag = "RockWAll";
						ReColor();
					}
					if (Input.GetKey("3"))
					{
						CurrSelectedOBJ.tag = "StartUnit";
						ReColor();
					}
					if (Input.GetKey("4"))
					{
						CurrSelectedOBJ.tag = "FinishUnit";
						ReColor();
					}
				}
            }




        }



	}
	public void MarkBestPath()
    {
		for (int i = 0; i < PathFinder.BestPath.Length; i++)
		{
			if (PathFinder.BestPath[i] != StartObj && PathFinder.BestPath[i] != FinishObj)
			{
				PathFinder.BestPath[i].GetComponent<MeshRenderer>().material = MarkedPath;
			}
		}
	}
	public void ReColor()
    {
		arr = GameObject.FindGameObjectsWithTag("FreeSpace");//----------- for the free spaces
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i].GetComponent<MeshRenderer>().material = FreeCell;
		}
		arr = GameObject.FindGameObjectsWithTag("RockWAll");//------------ for the rocks
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i].GetComponent<MeshRenderer>().material = RockWall;
		}
		arr = GameObject.FindGameObjectsWithTag("DeathTrap");//------------ for the Deathtraps
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i].GetComponent<MeshRenderer>().material = DeathTrap;
		}
		FinishObj = GameObject.FindGameObjectWithTag("FinishUnit").gameObject;
		StartObj = GameObject.FindGameObjectWithTag("StartUnit").gameObject;
		StartObj.GetComponent<MeshRenderer>().material = StartMat;
		FinishObj.GetComponent<MeshRenderer>().material = FinishMat;
	}
}
