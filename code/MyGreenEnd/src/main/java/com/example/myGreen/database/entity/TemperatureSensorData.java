package com.example.myGreen.database.entity;

import com.example.myGreen.database.entity.key.SensorDataKey;

import javax.persistence.EmbeddedId;
import javax.persistence.Entity;
import javax.persistence.Table;

@Entity
@Table(name = "TEMPERATURESENSORDATA")
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
