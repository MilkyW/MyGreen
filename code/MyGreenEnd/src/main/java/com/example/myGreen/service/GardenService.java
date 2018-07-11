package com.example.myGreen.service;

import com.example.myGreen.entity.Garden;
import com.example.myGreen.repository.GardenRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class GardenService {

    @Autowired
    private GardenRepository gardenRepository;

    public List<Garden> getGardenByUserId(long userId) {
        return gardenRepository.findByUserId(userId);
    }

    public void saveGarden(Garden garden) {
        gardenRepository.save(garden);
    }
}
