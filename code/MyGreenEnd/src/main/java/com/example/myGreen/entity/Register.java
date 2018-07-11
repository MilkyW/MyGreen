package com.example.myGreen.entity;

import javax.persistence.*;

@Entity
@Table(name = "REGISTER")
public class Register {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private long id;

    private String username;
    private String token;
    private long time;

    public long getRid() {
        return id;
    }

    public void setRid(long rid) {
        this.id = id;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String account) {
        this.username = username;
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
