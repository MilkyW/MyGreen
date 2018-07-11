package com.example.myGreen.entity;

import javax.persistence.*;

import com.example.myGreen.key.SensorDataKey;

@Entity
@Table(name="WetnessSensorData")
public class WetnessSensorData {

    @EmbeddedId
    private SensorDataKey id;

    private float wetness;

    public SensorDataKey getId() {
        return id;
    }

    public void setId(SensorDataKey id) {
        this.id = id;
    }

    public float getWetness() {
        return wetness;
    }

    public void setWetness(float wetness) {
        this.wetness = wetness;
    }
}