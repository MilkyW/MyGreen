package com.example.myGreen.repository;

import com.example.myGreen.Application;
import com.example.myGreen.entity.WetnessSensor;
import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;

import java.util.List;

@RunWith(SpringRunner.class)
@SpringBootTest(classes = Application.class)
public class WetnessSensorRepositoryTest {

    @Autowired
    WetnessSensorRepository repository;

    @Test
    public void findByGardenId() {
        List<WetnessSensor> list = repository.findByGardenId(1);
        for (WetnessSensor wernessSensor : list) {
            Assert.assertEquals(wernessSensor.getGardenId(), 1);
        }
    }

    @Test
    public void findByName() {
        List<WetnessSensor> list = repository.findByName("w1");
        for (WetnessSensor wernessSensor : list) {
            Assert.assertEquals(wernessSensor.getName(), "w1");
        }
    }

    @Test
    public void updateValidById() {
        repository.updateValidById(1, false);
        repository.updateValidById(1, true);
    }
}