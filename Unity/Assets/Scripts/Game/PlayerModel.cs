using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[SerializeField]

public class PlayerModel
{

    public string playerName;
    public int matal;
    public int crystal;
    public int deuterium;
    public List<PlaneModel> Planes;

    public PlayerModel(string name)
    {
        this.playerName = name;
        this.matal = 500;
        this.crystal = 300;
        this.deuterium = 100;
    }

    public void CollectResources()
    {
        matal += 10;
        crystal += 5;
        deuterium += 2;
    }
    
}
