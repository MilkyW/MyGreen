package com.example.myGreen.service;

import com.alibaba.fastjson.JSON;
import com.example.myGreen.database.entity.Garden;
import com.example.myGreen.database.repository.GardenRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

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

    public boolean deleteGardenById(long id) {
        gardenRepository.deleteById(id);
        return true;
    }

    public boolean updateGarden(Garden garden) {
        Optional<Garden> result = gardenRepository.findById(garden.getId());
        if (!result.isPresent()) {
            return false;
        }
        Garden oldGarden = result.get();

        oldGarden.setName(garden.getName());
        oldGarden.setWidth(garden.getWidth());
        oldGarden.setLength(garden.getLength());
        oldGarden.setIdealTemperature(garden.getIdealTemperature());
        oldGarden.setIdealWetness(garden.getIdealWetness());

        gardenRepository.save(garden);
        return true;
    }
}
