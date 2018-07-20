package com.example.myGreen.service;

import com.example.myGreen.database.entity.GardenController;
import com.example.myGreen.database.repository.GardenControllerRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class ControllerService {

    @Autowired
    private GardenControllerRepository controllerRepository;

    public List<GardenController> getControllerByGardenId(long gardenId) {
        return controllerRepository.findByGardenId(gardenId);
    }

    public void updateControllerValidById(long id, boolean valid) {
        System.out.println("valid:" + valid);
        controllerRepository.updateValidById(id, valid);
    }

    public void saveController(GardenController controller) {
        controllerRepository.save(controller);
    }

    public boolean deleteControllerById(long id) {
        controllerRepository.deleteById(id);
        return true;
    }
}
