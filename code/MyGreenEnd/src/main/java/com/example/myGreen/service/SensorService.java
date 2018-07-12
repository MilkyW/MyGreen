package com.example.myGreen.service;

import com.alibaba.fastjson.JSON;
import com.example.myGreen.entity.TemperatureSensor;
import com.example.myGreen.entity.WetnessSensor;
import com.example.myGreen.repository.TemperatureSensorDataRepository;
import com.example.myGreen.repository.TemperatureSensorRepository;
import com.example.myGreen.repository.WetnessSensorDataRepository;
import com.example.myGreen.repository.WetnessSensorRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

@Service
public class SensorService {

    @Autowired
    private TemperatureSensorRepository tsRepo;
    @Autowired
    private WetnessSensorRepository wsRepo;
    @Autowired
    private TemperatureSensorDataRepository tsDataRepo;
    @Autowired
    private WetnessSensorDataRepository wsDataRepo;

    public List<TemperatureSensor> getTemperatureSensorByGardenId(long gardenId) {
        return tsRepo.findByGardenId(gardenId);
    }

    public void saveTemperatureSensor(TemperatureSensor sensor) {
        tsRepo.save(sensor);
    }

    public List<WetnessSensor> getWetnessSensorByGardenId(long gardenId) {
        return wsRepo.findByGardenId(gardenId);
    }

    public void saveWetnessSensor(WetnessSensor sensor) {
        wsRepo.save(sensor);
    }

    public String getLatestTemperatureByGardenId(long gardenId) {
        ArrayList<String> list = new ArrayList<>();

        List<Long> sensorIdList = tsRepo.findSensorIdByGardenId(gardenId);
        for (long sensorId : sensorIdList) {
            Float temperature = tsDataRepo.findLatestTemperatureById(sensorId);
            if (temperature == null) {
                continue;
            }

            Map<String, String> map = new HashMap<>();
            map.put("id", Long.toString(sensorId));
            map.put("temperature", Float.toString(temperature));

            list.add(JSON.toJSONString(map));
        }

        return list.toString();
    }
}
