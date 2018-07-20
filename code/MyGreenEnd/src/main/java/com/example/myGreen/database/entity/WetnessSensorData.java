package com.example.myGreen.database.entity;

import com.example.myGreen.database.entity.key.SensorDataKey;

import javax.persistence.EmbeddedId;
import javax.persistence.Entity;
import javax.persistence.Table;

@Entity
@Table(name = "WETNESSSENSORDATA")
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