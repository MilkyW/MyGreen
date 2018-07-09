package com.example.myGreen.repository;


import com.example.myGreen.Application;
import com.example.myGreen.entity.TemperatureSensor;
import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;

import java.util.List;

@RunWith(SpringRunner.class)
@SpringBootTest(classes = Application.class)
public class TemperatureSensorRepositoryTest {

    @Autowired
    private TemperatureSensorRepository repository;

    @Test
    public void findByGardenId() {
        List<TemperatureSensor> list = repository.findByGardenId(1);
        for (TemperatureSensor temperatureSensor : list) {
            Assert.assertEquals(temperatureSensor.getGardenId(), 1);
        }
    }

    @Test
    public void findByName() {
        List<TemperatureSensor> list = repository.findByName("t1");
        for (TemperatureSensor temperatureSensor : list) {
            Assert.assertEquals(temperatureSensor.getName(), "t1");
        }
    }

    @Test
    public void updateValidById() {
        repository.updateValidById(1, false);
        repository.updateValidById(1, true);
    }
}