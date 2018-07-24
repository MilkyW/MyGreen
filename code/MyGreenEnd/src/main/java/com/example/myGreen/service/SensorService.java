package com.example.myGreen.service;

import com.alibaba.fastjson.JSON;
import com.example.myGreen.database.entity.TemperatureSensor;
import com.example.myGreen.database.entity.TemperatureSensorData;
import com.example.myGreen.database.entity.WetnessSensor;
import com.example.myGreen.database.entity.WetnessSensorData;
import com.example.myGreen.database.repository.TemperatureSensorDataRepository;
import com.example.myGreen.database.repository.TemperatureSensorRepository;
import com.example.myGreen.database.repository.WetnessSensorDataRepository;
import com.example.myGreen.database.repository.WetnessSensorRepository;
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

    /* Temperature */
    public List<TemperatureSensor> getTemperatureSensorByGardenId(long gardenId) {
        return tsRepo.findByGardenId(gardenId);
    }

    public void saveTemperatureSensor(TemperatureSensor sensor) {
        tsRepo.save(sensor);
    }

    public boolean deleteTemperatureSensorById(long id) {
        tsRepo.deleteById(id);
        return true;
    }

    public boolean updateTemperatureSensorNameById(long id, String name) {
        tsRepo.updateNameById(id, name);
        return true;
    }

    /* Temperature Sensor Data */
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

    public String getRecentTemperatureDataById(long id, int num) {
        ArrayList<String> list = new ArrayList<>();

        List<TemperatureSensorData> dataList = tsDataRepo.findRecentDataById(id, num);
        for (TemperatureSensorData data : dataList) {
            Map<String, String> map = new HashMap<>();
            map.put("temperature", Float.toString(data.getTemperature()));
            map.put("time", data.getId().getTime().toString());

            list.add(JSON.toJSONString(map));
        }

        return list.toString();
    }

    /* Wetness */
    public List<WetnessSensor> getWetnessSensorByGardenId(long gardenId) {
        return wsRepo.findByGardenId(gardenId);
    }

    public void saveWetnessSensor(WetnessSensor sensor) {
        wsRepo.save(sensor);
    }

    public boolean deleteWetnessSensorById(long id) {
        wsRepo.deleteById(id);
        return true;
    }

    public boolean updateWetnessSensorNameById(long id, String name) {
        wsRepo.updateNameById(id, name);
        return true;
    }

    /* Wetness Sensor Data */
    public String getLatestWetnessByGardenId(long gardenId) {
        ArrayList<String> list = new ArrayList<>();

        List<Long> sensorIdList = wsRepo.findSensorIdByGardenId(gardenId);
        for (long sensorId : sensorIdList) {
            Float wetness = wsDataRepo.findLatestWetnessById(sensorId);
            if (wetness == null) {
                continue;
            }

            Map<String, String> map = new HashMap<>();
            map.put("id", Long.toString(sensorId));
            map.put("wetness", Float.toString(wetness));

            list.add(JSON.toJSONString(map));
        }

        return list.toString();
    }

    public String getRecentWetnessDataById(long id, int num) {
        ArrayList<String> list = new ArrayList<>();

        List<WetnessSensorData> dataList = wsDataRepo.findRecentDataById(id, num);
        for (WetnessSensorData data : dataList) {
            Map<String, String> map = new HashMap<>();
            map.put("wetness", Float.toString(data.getWetness()));
            map.put("time", data.getId().getTime().toString());

            list.add(JSON.toJSONString(map));
        }

        return list.toString();
    }
}
