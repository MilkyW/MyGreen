package com.example.myGreen.repository;

import com.example.myGreen.Application;
import com.example.myGreen.database.entity.WetnessSensorData;
import com.example.myGreen.database.repository.WetnessSensorDataRepository;
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
public class WetnessSensorDataRepositoryTest {

    @Autowired
    private WetnessSensorDataRepository repository;

    @Test
    public void findBySensorId() {
        long id = 1;

        List<WetnessSensorData> list = repository.findBySensorId(id);

        if (list.isEmpty()) {
            Assert.fail(String.format("Wetness sensor ID:{} has no data", id));
        }

        for (WetnessSensorData temperatureSensorData : list) {
            Assert.assertEquals(temperatureSensorData.getId().getId(), 1);
        }
    }
    
    @Test
    public void findLatestWetnessById() throws Exception {
        long id = 1;
        
        repository.findLatestWetnessById(id);
    }
    
    @Test
    public void findRecentDataById() throws Exception {
        long id = 1;
        int num = 10;

        List<WetnessSensorData> list = repository.findRecentDataById(id, num);
        if (list.size() > num) {
            Assert.fail(String.format("take more than {} pieces of data", num));
        }
        for (WetnessSensorData data:list) {
            Assert.assertEquals(data.getId().getId(), id);
        }
    }
}