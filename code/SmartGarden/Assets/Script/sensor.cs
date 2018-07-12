using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sensor {

    private long id;
    private string name;
    private double location_x;
    private double location_y;
    private bool type;

    public long getId() { return id; }
    public string getName() { return name; }
    public double getX() { return location_x; }
    public double getY() { return location_y; }
    public bool getType() { return type; }

    public void setId(long id_) { id = id_; }
    public void setName(string name_) { name = name_; }
    public void setX(double x) { location_x = x; }
    public void setY(double y) { location_y = y; }
    public void setType(bool type_) { type = type_; }
}
