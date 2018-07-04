package com.example.myGreen.entity;

import javax.persistence.*;

@Entity
@Table(name="GARDEN")
public class Garden {
    @Id
    private long id;

    private long userId;
    private int width;
    private int length;
    private String name;
    private float idealTemperature;
    private float idealWetness;

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    public long getUserId() {
        return userId;
    }

    public void setUserId(long userId) {
        this.userId = userId;
    }

    public int getWidth() {
        return width;
    }

    public void setWidth(int width) {
        this.width = width;
    }

    public int getLength() {
        return length;
    }

    public void setLength(int length) {
        this.length = length;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public float getIdealTemperature() {
        return idealTemperature;
    }

    public void setIdealTemperature(float idealTemperature) {
        this.idealTemperature = idealTemperature;
    }

    public float getIdealWetness() {
        return idealWetness;
    }

    public void setIdealWetness(float idealWetness) {
        this.idealWetness = idealWetness;
    }
}
