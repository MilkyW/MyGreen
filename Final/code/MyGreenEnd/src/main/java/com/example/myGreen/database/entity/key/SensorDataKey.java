package com.example.myGreen.database.entity.key;

import javax.persistence.Column;
import javax.persistence.Embeddable;
import java.io.Serializable;
import java.sql.Timestamp;

/**
 * 传感器数据的复合主键
 */
@Embeddable
public class SensorDataKey implements Serializable {

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