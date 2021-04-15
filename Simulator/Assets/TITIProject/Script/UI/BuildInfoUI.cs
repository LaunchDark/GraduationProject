//using Packages.Rider.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildInfoUI : MonoBehaviour
{
    public Building UIBuild;
    
    

    public Text PosX;
    public Text PosY;
    public Text ScaleX;
    public Text ScaleY;

    public Text HolePosX;
    public Text HolePosY;
    public Text Exist;
    public Text Type;

    public Text SelectWallName;

    public GameObject BuildInfo;
    public GameObject WallInfo;

    protected Vector2 buildpos;
    protected Vector2 buildScale;
    protected Vector2 holePos = new Vector2(0, 0);
    protected hole[] holes = new hole[4];
    protected bool[] wallExist = new bool[4]{true, true, true, true};
    protected int wallNum = 0;

    public mButton sPlusPosXBtn;
    public mButton sMinusPosXBtn;
    public mButton sPlusPosYBtn;
    public mButton sMinusPosYBtn;

    public mButton bPlusPosXBtn;
    public mButton bMinusPosXBtn;
    public mButton bPlusPosYBtn;
    public mButton bMinusPosYBtn;

    public mButton sPlusScaleXBtn;
    public mButton sMinusScaleXBtn;
    public mButton sPlusScaleYBtn;
    public mButton sMinusScaleYBtn;

    public mButton bPlusScaleXBtn;
    public mButton bMinusScaleXBtn;
    public mButton bPlusScaleYBtn;
    public mButton bMinusScaleYBtn;

    public mButton Wall0;
    public mButton Wall1;
    public mButton Wall2;
    public mButton Wall3;

    public mButton changeType;
    public mButton changeBool;
    public mButton changeBuildInfo;

    public mButton sPlusWallX;
    public mButton sMinusWallX;
    public mButton bPlusWallX;
    public mButton bMinusWallX;

    public mButton sPlusWallY;
    public mButton sMinusWallY;
    public mButton bPlusWallY;
    public mButton bMinusWallY;

    public void Awake()
    {
        for (int x = 0; x < 4; x++)
        {
            holes[x] = new hole();
        }

        sPlusPosXBtn.clickCallBack = sPlusPosX;
        sMinusPosXBtn.clickCallBack = sMinusPosX;
        bPlusPosXBtn.clickCallBack = bPlusPosX;
        bMinusPosXBtn.clickCallBack = bMinusPosX;

        sPlusPosYBtn.clickCallBack = sPlusPosY;
        sMinusPosYBtn.clickCallBack = sMinusPosY;
        bPlusPosYBtn.clickCallBack = bPlusPosY;
        bMinusPosYBtn.clickCallBack = bMinusPosY;

        sPlusScaleXBtn.clickCallBack = sPlusScaleX;
        sMinusScaleXBtn.clickCallBack = sMinusScaleX;
        bPlusScaleXBtn.clickCallBack = bPlusScaleX;
        bMinusScaleXBtn.clickCallBack = bMinusScaleX;

        sPlusScaleYBtn.clickCallBack = sPlusScaleY;
        sMinusScaleYBtn.clickCallBack = sMinusScaleY;
        bPlusScaleYBtn.clickCallBack = bPlusScaleY;
        bMinusScaleYBtn.clickCallBack = bMinusScaleY;

        Wall0.clickCallBack = ChanegWall0;
        Wall1.clickCallBack = ChanegWall1;
        Wall2.clickCallBack = ChanegWall2;
        Wall3.clickCallBack = ChanegWall3;

        changeBuildInfo.clickCallBack = ChangeBuildInfo;
        changeType.clickCallBack = ChangeType;
        changeBool.clickCallBack = ChangeExist;

        sPlusWallX.clickCallBack = sPlusHolePosX;
        sMinusWallX.clickCallBack = sMinusHolePosX;
        bPlusWallX.clickCallBack = bPlusHolePosX;
        bMinusWallX.clickCallBack = bMinusHolePosX;

        sPlusWallY.clickCallBack = sPlusHolePosY;
        sMinusWallY.clickCallBack = sMinusHolePosY;
        bPlusWallY.clickCallBack = bPlusHolePosY;
        bMinusWallY.clickCallBack = bMinusHolePosY;

    }

    public void UpdateInfo()
    {
        buildpos = new Vector2(UIBuild.pos.x, UIBuild.pos.z);
        buildScale = new Vector2(UIBuild.lenght, UIBuild.weight);
        PosX.text = ((int)(buildpos.x * 10)).ToString();
        PosY.text = ((int)(buildpos.y * 10)).ToString();
        ScaleX.text = ((int)(buildScale.x * 10)).ToString();
        ScaleY.text = ((int)(buildScale.y * 10)).ToString();
        
    }

    public void UpdateUIWalls()
    {
        for (int x = 0; x < 4; x++)
        {
            wallExist[x] = UIBuild.isWall[x];
            holes[x] = UIBuild.walls[x].Hole;
        }
    }

    public void UpdateWallInfo()
    {
        if (UIBuild.isWall[wallNum])
        {
            Exist.text = "true";
            Type.text = holes[wallNum].Type.ToString();
            changeType.gameObject.GetComponent<Button>().interactable = true;
            if (holes[wallNum].Type != HoleType.None)
            {
                if(holes[wallNum].Type == HoleType.Door)
                {
                    sPlusWallX.gameObject.GetComponent<Button>().interactable = true;
                    sMinusWallX.gameObject.GetComponent<Button>().interactable = true;
                    bPlusWallX.gameObject.GetComponent<Button>().interactable = true;
                    bMinusWallX.gameObject.GetComponent<Button>().interactable = true;
                    HolePosX.text = ((int)(holes[wallNum].pos.x * 10)).ToString();
                    HolePosY.text = "0";
                }
                else if(holes[wallNum].Type == HoleType.Windows)
                {
                    sPlusWallX.gameObject.GetComponent<Button>().interactable = true;
                    sMinusWallX.gameObject.GetComponent<Button>().interactable = true;
                    bPlusWallX.gameObject.GetComponent<Button>().interactable = true;
                    bMinusWallX.gameObject.GetComponent<Button>().interactable = true;

                    sPlusWallY.gameObject.GetComponent<Button>().interactable = true;
                    sMinusWallY.gameObject.GetComponent<Button>().interactable = true;
                    bPlusWallY.gameObject.GetComponent<Button>().interactable = true;
                    bMinusWallY.gameObject.GetComponent<Button>().interactable = true;
                    HolePosX.text = ((int)(holes[wallNum].pos.x * 10)).ToString();
                    HolePosY.text = ((int)(holes[wallNum].pos.y * 10)).ToString();
                }
            }
            else
            {
                HolePosX.text = "0";
                HolePosY.text = "0";
                sPlusWallX.gameObject.GetComponent<Button>().interactable = false;
                sMinusWallX.gameObject.GetComponent<Button>().interactable = false;
                bPlusWallX.gameObject.GetComponent<Button>().interactable = false;
                bMinusWallX.gameObject.GetComponent<Button>().interactable = false;

                sPlusWallY.gameObject.GetComponent<Button>().interactable = false;
                sMinusWallY.gameObject.GetComponent<Button>().interactable = false;
                bPlusWallY.gameObject.GetComponent<Button>().interactable = false;
                bMinusWallY.gameObject.GetComponent<Button>().interactable = false;
            }
        }
        else
        {
            Exist.text = "false";
            HolePosY.text = "0";
            HolePosY.text = "0";
            changeType.gameObject.GetComponent<Button>().interactable = false;

            sPlusWallX.gameObject.GetComponent<Button>().interactable = false;
            sMinusWallX.gameObject.GetComponent<Button>().interactable = false;
            bPlusWallX.gameObject.GetComponent<Button>().interactable = false;
            bMinusWallX.gameObject.GetComponent<Button>().interactable = false;

            sPlusWallY.gameObject.GetComponent<Button>().interactable = false;
            sMinusWallY.gameObject.GetComponent<Button>().interactable = false;
            bPlusWallY.gameObject.GetComponent<Button>().interactable = false;
            bMinusWallY.gameObject.GetComponent<Button>().interactable = false;
        }
    }

    public void UpdateBuild()
    {
        UIBuild.pos.init(new Vector3(buildpos.x, UIBuild.pos.y, buildpos.y));
        UIBuild.weight = buildScale.y;
        UIBuild.lenght = buildScale.x;
        UIBuild.SetWall();
        UpdateInfo();
        //将build返回BuildUImgr
        transform.parent.parent.GetComponent<CreatBuildUI>().UIBuilding = UIBuild;
        transform.parent.parent.GetComponent<CreatBuildUI>().UpdateSelectBuilding();
    }

    public void UpdateWall()
    {
        for (int x = 0; x < 4; x++)
        {
            UIBuild.isWall[x] = wallExist[x];
            UIBuild.walls[x].Hole = holes[x];
        }
        UpdateWallInfo();
        
        transform.parent.parent.GetComponent<CreatBuildUI>().UIBuilding = UIBuild;
        transform.parent.parent.GetComponent<CreatBuildUI>().UpdateSelectBuilding();
    }
    
    public void sPlusPosX()
    {
        buildpos.x += 0.1f;
        UpdateBuild();
    }

    public void sMinusPosX()
    {
        buildpos.x -= 0.1f;
        UpdateBuild();
    }

    public void bPlusPosX()
    {
        buildpos.x += 1.0f;
        UpdateBuild();
    }

    public void bMinusPosX()
    {
        buildpos.x -= 1.0f;
        UpdateBuild();
    }

    public void sPlusPosY()
    {
        buildpos.y += 0.1f;
        UpdateBuild();
    }

    public void sMinusPosY()
    {
        buildpos.y -= 0.1f;
        UpdateBuild();
    }

    public void bPlusPosY()
    {
        buildpos.y += 1.0f;
        UpdateBuild();
    }

    public void bMinusPosY()
    {
        buildpos.y -= 1.0f;
        UpdateBuild();
    }

    public void sPlusScaleX()
    {
        buildScale.x += 0.1f;
        UpdateBuild();
    }

    public void sMinusScaleX()
    {
        buildScale.x -= 0.1f;
        UpdateBuild();
    }

    public void bPlusScaleX()
    {
        buildScale.x += 1.0f;
        UpdateBuild();
    }

    public void bMinusScaleX()
    {
        buildScale.x -= 1.0f;
        UpdateBuild();
    }

    public void sPlusScaleY()
    {
        buildScale.y += 0.1f;
        UpdateBuild();
    }

    public void sMinusScaleY()
    {
        buildScale.y -= 0.1f;
        UpdateBuild();
    }

    public void bPlusScaleY()
    {
        buildScale.y += 1.0f;
        UpdateBuild();
    }

    public void bMinusScaleY()
    {
        buildScale.y -= 1.0f;
        UpdateBuild();
    }

    public void ChangeWallInfo()
    {
        BuildInfo.SetActive(false);
        WallInfo.SetActive(true);
    }

    public void ChangeBuildInfo()
    {
        BuildInfo.SetActive(true);
        WallInfo.SetActive(false);
    }

    public void ChanegWall0()
    {
        wallNum = 0;
        SelectWallName.text = "WallUp";
        UpdateInfo();
        UpdateWallInfo();
        ChangeWallInfo();
    }

    public void ChanegWall1()
    {
        SelectWallName.text = "WallLeft";
        wallNum = 1;
        UpdateInfo();
        UpdateWallInfo();
        ChangeWallInfo();
    }

    public void ChanegWall2()
    {
        SelectWallName.text = "WallDown";
        wallNum = 2;
        UpdateInfo();
        UpdateWallInfo();
        ChangeWallInfo();
    }

    public void ChanegWall3()
    {
        SelectWallName.text = "WallRight";
        wallNum = 3;
        ChangeWallInfo();
        UpdateInfo();
        UpdateWallInfo();
    }

    public void ChangeExist()
    {
        if (wallExist[wallNum])
        {
            wallExist[wallNum] = false;
        }
        else
        {
            wallExist[wallNum] = true;
        }
        UpdateWall();
    }

    public void ChangeType()
    {
      
        if(holes[wallNum].Type == HoleType.None)
        {
            holes[wallNum].Type = HoleType.Door;
        }
        else if(holes[wallNum].Type == HoleType.Door)
        {
            holes[wallNum].Type = HoleType.Windows;
        }
        else if(holes[wallNum].Type == HoleType.Windows)
        {
            holes[wallNum].Type = HoleType.None;
        }
        UpdateWall();
    }

    public void sPlusHolePosX()
    {
        holes[wallNum].pos.x += 0.1f;
        if(wallNum % 2 == 0)
        {
            if (holes[wallNum].pos.x > UIBuild.lenght / 2 - 1.6f)
            {
                bPlusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                bPlusWallX.gameObject.GetComponent<Button>().interactable = true;
            }
            if (holes[wallNum].pos.x < -UIBuild.lenght / 2 + 1.6f)
            {
                bMinusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                bMinusWallX.gameObject.GetComponent<Button>().interactable = true;
            }

            if (holes[wallNum].pos.x > UIBuild.lenght / 2 - 0.6f)
            {
                sPlusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
           else
            {
                sPlusWallX.gameObject.GetComponent<Button>().interactable = true;
            }
            if(holes[wallNum].pos.x < -UIBuild.lenght / 2 + 0.6f)
            {
                sMinusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                sMinusWallX.gameObject.GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            if (holes[wallNum].pos.x > UIBuild.weight / 2 - 1.6f)
            {
                bPlusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                bPlusWallX.gameObject.GetComponent<Button>().interactable = true;
            }
            if (holes[wallNum].pos.x < -UIBuild.weight / 2 + 1.6f)
            {
                bMinusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                bMinusWallX.gameObject.GetComponent<Button>().interactable = true;
            }

            if (holes[wallNum].pos.x > UIBuild.weight / 2 - 0.6f)
            {
                sPlusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                sPlusWallX.gameObject.GetComponent<Button>().interactable = true;
            }
            if (holes[wallNum].pos.x < -UIBuild.weight / 2 + 0.6f)
            {
                sMinusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                sMinusWallX.gameObject.GetComponent<Button>().interactable = true;
            }
        }
        UpdateWall();
    }

    public void sMinusHolePosX()
    {
        holes[wallNum].pos.x -= 0.1f;
        if (wallNum % 2 == 0)
        {
            if (holes[wallNum].pos.x > UIBuild.lenght / 2 - 1.6f)
            {
                bPlusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                bPlusWallX.gameObject.GetComponent<Button>().interactable = true;
            }
            if (holes[wallNum].pos.x < -UIBuild.lenght / 2 + 1.6f)
            {
                bMinusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                bMinusWallX.gameObject.GetComponent<Button>().interactable = true;
            }

            if (holes[wallNum].pos.x > UIBuild.lenght / 2 - 0.6f)
            {
                sPlusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                sPlusWallX.gameObject.GetComponent<Button>().interactable = true;
            }
            if (holes[wallNum].pos.x < -UIBuild.lenght / 2 + 0.6f)
            {
                sMinusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                sMinusWallX.gameObject.GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            if (holes[wallNum].pos.x > UIBuild.weight / 2 - 1.6f)
            {
                bPlusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                bPlusWallX.gameObject.GetComponent<Button>().interactable = true;
            }
            if (holes[wallNum].pos.x < -UIBuild.weight / 2 + 1.6f)
            {
                bMinusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                bMinusWallX.gameObject.GetComponent<Button>().interactable = true;
            }

            if (holes[wallNum].pos.x > UIBuild.weight / 2 - 0.6f)
            {
                sPlusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                sPlusWallX.gameObject.GetComponent<Button>().interactable = true;
            }
            if (holes[wallNum].pos.x < -UIBuild.weight / 2 + 0.6f)
            {
                sMinusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                sMinusWallX.gameObject.GetComponent<Button>().interactable = true;
            }
        }
        UpdateWall();
    }

    public void bPlusHolePosX()
    {
        holes[wallNum].pos.x += 1.0f;
        if (wallNum % 2 == 0)
        {
            if (holes[wallNum].pos.x > UIBuild.lenght / 2 - 1.6f)
            {
                bPlusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                bPlusWallX.gameObject.GetComponent<Button>().interactable = true;
            }
            if (holes[wallNum].pos.x < -UIBuild.lenght / 2 + 1.6f)
            {
                bMinusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                bMinusWallX.gameObject.GetComponent<Button>().interactable = true;
            }

            if (holes[wallNum].pos.x > UIBuild.lenght / 2 - 0.6f)
            {
                sPlusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                sPlusWallX.gameObject.GetComponent<Button>().interactable = true;
            }
            if (holes[wallNum].pos.x < -UIBuild.lenght / 2 + 0.6f)
            {
                sMinusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                sMinusWallX.gameObject.GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            if (holes[wallNum].pos.x > UIBuild.weight / 2 - 1.6f)
            {
                bPlusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                bPlusWallX.gameObject.GetComponent<Button>().interactable = true;
            }
            if (holes[wallNum].pos.x < -UIBuild.weight / 2 + 1.6f)
            {
                bMinusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                bMinusWallX.gameObject.GetComponent<Button>().interactable = true;
            }

            if (holes[wallNum].pos.x > UIBuild.weight / 2 - 0.6f)
            {
                sPlusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                sPlusWallX.gameObject.GetComponent<Button>().interactable = true;
            }
            if (holes[wallNum].pos.x < -UIBuild.weight / 2 + 0.6f)
            {
                sMinusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                sMinusWallX.gameObject.GetComponent<Button>().interactable = true;
            }
        }
        UpdateWall();
    }

    public void bMinusHolePosX()
    {
        holes[wallNum].pos.x -= 1.0f;
        if (wallNum % 2 == 0)
        {
            if (holes[wallNum].pos.x > UIBuild.lenght / 2 - 1.6f)
            {
                bPlusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                bPlusWallX.gameObject.GetComponent<Button>().interactable = true;
            }
            if (holes[wallNum].pos.x < -UIBuild.lenght / 2 + 1.6f)
            {
                bMinusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                bMinusWallX.gameObject.GetComponent<Button>().interactable = true;
            }

            if (holes[wallNum].pos.x > UIBuild.lenght / 2 - 0.6f)
            {
                sPlusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                sPlusWallX.gameObject.GetComponent<Button>().interactable = true;
            }
            if (holes[wallNum].pos.x < -UIBuild.lenght / 2 + 0.6f)
            {
                sMinusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                sMinusWallX.gameObject.GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            if (holes[wallNum].pos.x > UIBuild.weight / 2 - 1.6f)
            {
                bPlusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                bPlusWallX.gameObject.GetComponent<Button>().interactable = true;
            }
            if (holes[wallNum].pos.x < -UIBuild.weight / 2 + 1.6f)
            {
                bMinusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                bMinusWallX.gameObject.GetComponent<Button>().interactable = true;
            }

            if (holes[wallNum].pos.x > UIBuild.weight / 2 - 0.6f)
            {
                sPlusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                sPlusWallX.gameObject.GetComponent<Button>().interactable = true;
            }
            if (holes[wallNum].pos.x < -UIBuild.weight / 2 + 0.6f)
            {
                sMinusWallX.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                sMinusWallX.gameObject.GetComponent<Button>().interactable = true;
            }
        }
        UpdateWall();
    }

    public void sPlusHolePosY()
    {
        holes[wallNum].pos.y += 0.1f;
        if(holes[wallNum].pos.y >= UIBuild.height - 0.8f)
        {
            sPlusWallY.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            sPlusWallY.gameObject.GetComponent<Button>().interactable = true;
        }
        if(holes[wallNum].pos.y <= 0.1f)
        {
            sMinusWallY.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            sMinusWallY.gameObject.GetComponent<Button>().interactable = true;
        }

        if (holes[wallNum].pos.y >= UIBuild.height - 1.8f)
        {
            bPlusWallY.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            bPlusWallY.gameObject.GetComponent<Button>().interactable = true;
        }
        if (holes[wallNum].pos.y <= 1.1f)
        {
            bMinusWallY.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            bMinusWallY.gameObject.GetComponent<Button>().interactable = true;
        }
        UpdateWall();
    }

    public void sMinusHolePosY()
    {
        holes[wallNum].pos.y -= 0.1f;
        if (holes[wallNum].pos.y >= UIBuild.height - 0.8f)
        {
            sPlusWallY.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            sPlusWallY.gameObject.GetComponent<Button>().interactable = true;
        }
        if (holes[wallNum].pos.y <= 0.1f)
        {
            sMinusWallY.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            sMinusWallY.gameObject.GetComponent<Button>().interactable = true;
        }

        if (holes[wallNum].pos.y >= UIBuild.height - 1.8f)
        {
            bPlusWallY.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            bPlusWallY.gameObject.GetComponent<Button>().interactable = true;
        }
        if (holes[wallNum].pos.y <= 1.1f)
        {
            bMinusWallY.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            bMinusWallY.gameObject.GetComponent<Button>().interactable = true;
        }
        UpdateWall();
    }

    public void bPlusHolePosY()
    {
        holes[wallNum].pos.y += 1.0f;
        if (holes[wallNum].pos.y >= UIBuild.height - 0.8f)
        {
            sPlusWallY.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            sPlusWallY.gameObject.GetComponent<Button>().interactable = true;
        }
        if (holes[wallNum].pos.y <= 0.1f)
        {
            sMinusWallY.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            sMinusWallY.gameObject.GetComponent<Button>().interactable = true;
        }

        if (holes[wallNum].pos.y >= UIBuild.height - 1.8f)
        {
            bPlusWallY.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            bPlusWallY.gameObject.GetComponent<Button>().interactable = true;
        }
        if (holes[wallNum].pos.y <= 1.1f)
        {
            bMinusWallY.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            bMinusWallY.gameObject.GetComponent<Button>().interactable = true;
        }
        UpdateWall();
    }

    public void bMinusHolePosY()
    {
        holes[wallNum].pos.y -= 1.0f;
        if (holes[wallNum].pos.y >= UIBuild.height - 0.8f)
        {
            sPlusWallY.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            sPlusWallY.gameObject.GetComponent<Button>().interactable = true;
        }
        if (holes[wallNum].pos.y <= 0.1f)
        {
            sMinusWallY.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            sMinusWallY.gameObject.GetComponent<Button>().interactable = true;
        }

        if (holes[wallNum].pos.y >= UIBuild.height - 1.8f)
        {
            bPlusWallY.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            bPlusWallY.gameObject.GetComponent<Button>().interactable = true;
        }
        if (holes[wallNum].pos.y <= 1.1f)
        {
            bMinusWallY.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            bMinusWallY.gameObject.GetComponent<Button>().interactable = true;
        }
        UpdateWall();
    }
}
