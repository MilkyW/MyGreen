package com.example.myGreen.entity;

import javax.persistence.*;

@Entity
@Table(name = "TEMPERATURESENSOR")
public class TemperatureSensor {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private long id;

    private long gardenId;
    private int x;
    private int y;
    private String name;
    private boolean valid;

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    public long getGardenId() {
        return gardenId;
    }

    public void setGardenId(long gardenId) {
        this.gardenId = gardenId;
    }

    public int getX() {
        return x;
    }

    public void setX(int x) {
        this.x = x;
    }

    public int getY() {
        return y;
    }

    public void setY(int y) {
        this.y = y;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public boolean isValid() {
        return valid;
    }

    public void setValid(boolean valid) {
        this.valid = valid;
    }
}
