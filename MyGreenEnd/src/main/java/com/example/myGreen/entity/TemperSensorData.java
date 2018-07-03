package com.example.myGreen.entity;

import javax.persistence.*;

import com.example.myGreen.key.SensorDataKey;

@Entity
@Table(name="TEMPERSENSORDATA")
public class TemperSensorData {

    @EmbeddedId
    private SensorDataKey id;

    private float temper;

    public SensorDataKey getId() {
        return id;
    }

    public void setId(SensorDataKey id) {
        this.id = id;
    }

    public float getTemper() {
        return temper;
    }

    public void setTemper(float temper) {
        this.temper = temper;
    }
}
