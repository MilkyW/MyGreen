using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sensor {

    private long id;
    private string name;
    private int location_x;
    private int location_y;
    private bool type;
    private float data;

    public long getId() { return id; }
    public string getName() { return name; }
    public int getX() { return location_x; }
    public int getY() { return location_y; }
    public bool getType() { return type; }
    public float getData() { return data; }

    public void setId(long id_) { id = id_; }
    public void setName(string name_) { name = name_; }
    public void setX(int x) { location_x = x; }
    public void setY(int y) { location_y = y; }
    public void setType(bool type_) { type = type_; }
    public void setData(float data_) { data = data_; }
}
