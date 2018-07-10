package com.example.myGreen.repository;

import com.example.myGreen.Application;
import com.example.myGreen.entity.TemperatureSensorData;
import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;

import java.sql.Timestamp;
import java.util.List;

@RunWith(SpringRunner.class)
@SpringBootTest(classes = Application.class)
public class TemperatureSensorDataRepositoryTest {

    @Autowired
    private TemperatureSensorDataRepository repository;

    @Test
    public void findBySensorId() {
        List<TemperatureSensorData> list = repository.findBySensorId(1);
        for (TemperatureSensorData temperatureSensorData : list) {
            Assert.assertEquals(temperatureSensorData.getId().getId(), 1);
        }
    }

    @Test
    public void findLatestDataById() {
        List<TemperatureSensorData> list = repository.findBySensorId(1);
        Timestamp latestTime = repository.findLatestDataById(1).getId().getTime();
        for (TemperatureSensorData data : list) {
            Timestamp time = data.getId().getTime();
            Assert.assertTrue(latestTime.after(time) || latestTime.equals(time));
        }
    }
}