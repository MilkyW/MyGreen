package com.example.myGreen.repository;

import com.example.myGreen.Application;
import com.example.myGreen.database.entity.Garden;
import com.example.myGreen.database.repository.GardenRepository;
import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;

import java.util.List;

@RunWith(SpringRunner.class)
@SpringBootTest(classes = Application.class)
public class GardenRepositoryTest {

    @Autowired
    GardenRepository repository;

    @Test
    public void findByUserId() {
        List<Garden> gardenList = repository.findByUserId(1);
        for (Garden garden : gardenList) {
            Assert.assertEquals(garden.getUserId(), 1);
        }
    }
}