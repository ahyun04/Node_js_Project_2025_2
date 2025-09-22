using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class PlaneModel
{
    public int id;
    public string name;
    public int matal;
    public int crystal;
    public int deuterium;

    public PlaneModel(int id, string name)
    {
        this.id = id;
        this.name = name;
        this.matal = 500;
        this.crystal = 300;
        this.deuterium = 100;
    }
}
