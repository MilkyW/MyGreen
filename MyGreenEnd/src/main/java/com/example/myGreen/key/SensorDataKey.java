package com.example.myGreen.key;

import javax.persistence.Embeddable;
import javax.persistence.Column;
import java.io.Serializable;
import java.sql.Timestamp;

@Embeddable
public class SensorDataKey implements Serializable  {

    @Column(name = "time")
    private Timestamp time;

    @Column(name = "ID")
    private long id;

    public Timestamp getTime() {
        return time;
    }

    public void setTime(Timestamp time) {
        this.time = time;
    }

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }
}