package com.example.myGreen.entity;

import javax.persistence.*;

@Entity
@Table(name="PROBLEM")
public class Problem {
    @Id
    @GeneratedValue(strategy=GenerationType.IDENTITY)
    private long id;

    private String title;

    public long getId() {
        return id;
    }
    public void setId(long id) {
        this.id = id;
    }
    public String getTitle() {
        return title;
    }
    public void setTitle(String title) {
        this.title = title;
    }
}
