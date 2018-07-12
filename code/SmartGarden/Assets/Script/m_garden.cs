using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m_garden {

    private long id;
    private string name;
    private float ideal_temperature;
    private float ideal_humidty;
    private int length;
    private int width;
    private List<sensor> sensors;
    private List<controller> controllers;

    public m_garden()
    {
        sensors = new List<sensor>();
        controllers = new List<controller>();
    }

    public long getId() { return id; }
    public string getName() { return name; }
    public float getIdealTemperature() { return ideal_temperature; }
    public float getIdealHumidty() { return ideal_humidty; }
    public int getLength() { return length; }
    public int getWidth() { return width; }
    public List<sensor> getSensors() { return sensors; }
    public List<controller> getControllers() { return controllers; }

    public void setId(long id_) { id = id_; }
    public void setName(string name_) { name = name_; }
    public void setIdealTemperature(float ideal_temperature_) { ideal_temperature = ideal_temperature_; }
    public void setIdealHumidty(float ideal_humidty_) { ideal_humidty = ideal_humidty_; }
    public void setLength(int length_) { length = length_; }
    public void setWidth(int width_) { width = width_; }
    public void setSensors(List<sensor> sensors_) { sensors = sensors_; }
    public void setControllers(List<controller> controllers_) { controllers = controllers_; }
    public void addSensor(sensor sensor_) { sensors.Add(sensor_); }
    public void addSensor(List<sensor> sensors_)
    {
        foreach (sensor e in sensors_)
            sensors.Add(e);
    }
    public void addController(controller controller_) { controllers.Add(controller_); }
    public void addController(List<controller> controllers_)
    {
        foreach (controller e in controllers)
            controllers.Add(e);
    }
}
