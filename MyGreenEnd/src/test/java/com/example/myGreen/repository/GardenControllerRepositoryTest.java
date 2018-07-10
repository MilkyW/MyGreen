package com.example.myGreen.repository;

import com.example.myGreen.Application;
import com.example.myGreen.entity.GardenController;
import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;

import java.util.List;

@RunWith(SpringRunner.class)
@SpringBootTest(classes = Application.class)
public class GardenControllerRepositoryTest {
    @Autowired
    private GardenControllerRepository repository;

    @Test
    public void findByGardenId() {
        List<GardenController> gardenControllerList = repository.findByGardenId(1);
        for (GardenController gardenController : gardenControllerList) {
            Assert.assertEquals(gardenController.getGardenId(), 1);
        }
    }

    @Test
    public void findByName() {
        List<GardenController> gardenControllerList = repository.findByName("c1");
        for (GardenController gardenController : gardenControllerList) {
            Assert.assertEquals(gardenController.getName(), "c1");
        }
    }

    @Test
    public void updateValidById() {
        repository.updateValidById(1, false);
        repository.updateValidById(1, true);
    }
}