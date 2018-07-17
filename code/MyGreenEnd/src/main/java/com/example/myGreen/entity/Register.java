package com.example.myGreen.entity;

import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.Table;

@Entity
@Table(name = "REGISTER")
public class Register {
    @Id
    private long id;

    private String token;
    private long time;

    public Register() {
    }

    public Register(long id, String token, long time) {
        this.id = id;
        this.token = token;
        this.time = time;
    }

    public long getId() {
        return id;
    }

    public void setId(long rid) {
        this.id = id;
    }

    public String getToken() {
        return token;
    }

    public void setToken(String token) {
        this.token = token;
    }

    public long getTime() {
        return time;
    }

    public void setTime(long time) {
        this.time = time;
    }
}
