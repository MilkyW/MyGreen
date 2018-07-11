package com.example.myGreen.entity;

import javax.persistence.*;

@Entity
@Table(name = "REG")

public class Register {
    @Id
    @GeneratedValue(strategy=GenerationType.IDENTITY)
    private long id;

    private String account;
    private String token;
    private long time;

    public long getRid() {
        return id;
    }

    public void setRid(long rid) {
        this.id = rid;
    }

    public String getAccount() {
        return account;
    }

    public void setAccount(String account) {
        this.account = account;
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
