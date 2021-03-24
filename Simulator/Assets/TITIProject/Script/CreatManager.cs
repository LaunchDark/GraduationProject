using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatManager : MonoBehaviour
{
    public Building building;
    void Start()
    {
        //gameObject.AddComponent<BuildingInfo>();
        BuildingInfo.Instance.Init();
        

        Vector3 pos = new Vector3(0, 0, 0);
        Building build = new Building(BuildingType.none,2.5f, 10.0f, 5.0f, 0.1f, pos);
        int num = 1;
        GameObject building1 = new GameObject("building" + num);
        build.CreatFloor(building1);
        build.CreatTop(building1);
        build.SetWall();
        Vector2 winPos = new Vector2(0.5f, 1);
        Vector2 DoorPos = new Vector2(-0.5f, 0);
        build.walls[0].SetHole(HoleType.Door, DoorPos);
        build.walls[1].SetHole(HoleType.Door, DoorPos);
        build.walls[2].SetHole(HoleType.Windows, winPos);
        build.walls[3].SetHole(HoleType.Windows, winPos);
        build.CreatWall(building1);
    }

    
    


    void LoadTxT()
    {
        // 将test01 中的内容加载进txt文本中
        TextAsset txt = Resources.Load("test01") as TextAsset;
        // 输出该文本的内容
        Debug.Log(txt);

        // 以换行符作为分割点，将该文本分割成若干行字符串，并以数组的形式来保存每行字符串的内容
        string[] str = txt.text.Split('\n');
        // 将该文本中的字符串输出 
        Debug.Log("str[0]= " + str[0]);
        Debug.Log("str[1]= " + str[1]);

        // 将每行字符串的内容以逗号作为分割点，并将每个逗号分隔的字符串内容遍历输出
        foreach (string strs in str)
        {
            string[] ss = strs.Split(',');
            Debug.Log(ss[0]);
            Debug.Log(ss[1]);
            Debug.Log(ss[2]);
            Debug.Log(ss[3]);
        }
    }

}
