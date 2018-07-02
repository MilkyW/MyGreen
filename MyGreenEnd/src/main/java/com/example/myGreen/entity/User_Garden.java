package com.example.myGreen.entity;

import javax.persistence.*;

@Entity
@Table(name="USER")
public class User_Garden {
    @Id
    @GeneratedValue(strategy=GenerationType.IDENTITY)
    private long UserID;

    private long GardenID;

    public long getUserID() {
        return UserID;
    }

    public void setUserID(long userID) {
        UserID = userID;
    }

    public long getGardenID() {
        return GardenID;
    }

    public void setGardenID(long gardenID) {
        GardenID = gardenID;
    }
}