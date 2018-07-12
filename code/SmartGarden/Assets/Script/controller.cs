using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller  {

    private long id;
    private string name;
    private int location_x;
    private int location_y;
    private bool state;

    public long getId() { return id; }
    public string getName() { return name; }
    public int getX() { return location_x; }
    public int getY() { return location_y; }
    public bool getState() { return state; }

    public void setId(long id_) { id = id_; }
    public void setName(string name_) { name = name_; }
    public void setX(int x) { location_x = x; }
    public void setY(int y) { location_y = y; }
    public void setState(bool state_) { state = state_; }
}
