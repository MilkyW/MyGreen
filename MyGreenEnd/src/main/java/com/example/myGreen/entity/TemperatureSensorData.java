package com.example.myGreen.entity;

import javax.persistence.*;

import com.example.myGreen.key.SensorDataKey;

@Entity
@Table(name="TEMPERATURESENSORDATA")
public class TemperatureSensorData {

    @EmbeddedId
    private SensorDataKey id;

    private float temperature;

    public SensorDataKey getId() {
        return id;
    }

    public void setId(SensorDataKey id) {
        this.id = id;
    }

    public float getTemperature() {
        return temperature;
    }

    public void setTemperature(float temperature) {
        this.temperature = temperature;
    }
}
