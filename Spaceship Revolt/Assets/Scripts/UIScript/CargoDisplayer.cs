using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CargoDisplayer : MonoBehaviour
{
    // Start is called before the first frame update
    List<Image> UIImageList = new List<Image>();
    void Start()
    {
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            UIImageList.Add(gameObject.transform.GetChild(i).GetComponent<Image>());
        }
        GameEvents.current.onCargoChange += DisplayCargoInventory;
    }


    public void DisplayCargoInventory()
    {
        List<GameObject> tempCargoList = GameData.Instance.GetCargo();
        for(int i = 0; i < UIImageList.Count; i++)
        {
            if(i < tempCargoList.Count)
            {
                UIImageList[i].sprite = tempCargoList[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            }
            else
            {
                UIImageList[i].sprite = null;
            }
        }
    }

}
