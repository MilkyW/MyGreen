package com.example.myGreen.repository;

import com.example.myGreen.Application;
import com.example.myGreen.database.entity.TemperatureSensorData;
import com.example.myGreen.database.repository.TemperatureSensorDataRepository;
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

    @Test
    public void findLatestTemperatureById() throws Exception {
        long id = 1;

        Float temperature = repository.findLatestTemperatureById(id);
    }

    @Test
    public void findRecentDataById() throws Exception {
        long id = 1;
        int num = 10;

        List<TemperatureSensorData> list = repository.findRecentDataById(id, num);
        if (list.size() > num) {
            Assert.fail(String.format("take more than {} pieces of data", num));
        }
        for (TemperatureSensorData data:list) {
            Assert.assertEquals(data.getId().getId(), id);
        }
    }
}