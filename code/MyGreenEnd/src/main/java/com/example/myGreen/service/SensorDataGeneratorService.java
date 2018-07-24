package com.example.myGreen.service;

import com.alibaba.fastjson.JSON;
import com.example.myGreen.database.entity.TemperatureSensor;
import com.example.myGreen.database.entity.TemperatureSensorData;
import com.example.myGreen.database.entity.WetnessSensor;
import com.example.myGreen.database.entity.WetnessSensorData;
import com.example.myGreen.database.entity.key.SensorDataKey;
import com.example.myGreen.database.repository.TemperatureSensorDataRepository;
import com.example.myGreen.database.repository.TemperatureSensorRepository;
import com.example.myGreen.database.repository.WetnessSensorDataRepository;
import com.example.myGreen.database.repository.WetnessSensorRepository;
import com.example.myGreen.service.mail.NormalDto;
import com.example.myGreen.webSocket.SingleTemperatureHandler;
import com.example.myGreen.webSocket.SingleWetnessHandler;
import com.example.myGreen.webSocket.TemperatureWebSocketHandler;
import com.example.myGreen.webSocket.WetnessWebSocketHandler;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.web.socket.TextMessage;
import org.springframework.web.socket.WebSocketSession;

import java.sql.Timestamp;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

@Service
public class SensorDataGeneratorService {

    private static Logger log = LoggerFactory.getLogger(SensorDataGeneratorService.class);

    @Autowired
    private TemperatureSensorRepository temperatureSensorRepository;
    @Autowired
    private WetnessSensorRepository wetnessSensorRepository;
    @Autowired
    private TemperatureSensorDataRepository temperatureSensorDataRepository;
    @Autowired
    private WetnessSensorDataRepository wetnessSensorDataRepository;

    /**
     *  data generator
     *  every 3 seconds insert a new line
     *  include WetnessSensorData, TemperatureSensorData
     */
    public NormalDto generate() {
        NormalDto normalDto = new NormalDto();

        /* Map to wrap data */
        Map<String, String> tHeatMapMap = new HashMap<>();
        Map<String, String> tLineChartMap = new HashMap<>();
        Map<String, String> wHeatMapMap = new HashMap<>();
        Map<String, String> wLineChartMap = new HashMap<>();

        /* Get sensors' ID and gardenId */
        List<TemperatureSensor> temperatureSensorList = temperatureSensorRepository.findSensorInfo();
        List<WetnessSensor> wetnessSensorIdList = wetnessSensorRepository.findSensorInfo();

        // run every 3 seconds
        final long timeInterval = 3000;//ms
        int i = 0;
        while (i < 1000) {
            log.info("Generate round {}", i);
            // ------- code for task to run
            java.util.Random r = new java.util.Random();

            for (TemperatureSensor sensor : temperatureSensorList) {
                long id = sensor.getId();
                SensorDataKey sensorDataKey = new SensorDataKey();
                sensorDataKey.setId(id);

                Date date = new Date();//获得系统时间.
                String nowTime = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(date);//将时间格式转换成符合Timestamp要求的格式.
                Timestamp goodsC_date = Timestamp.valueOf(nowTime);//转换时间
                sensorDataKey.setTime(goodsC_date);

                float x = r.nextFloat() - 0.5f;

                TemperatureSensorData temperatureSensorData = new TemperatureSensorData();
                temperatureSensorData.setId(sensorDataKey);
                temperatureSensorData.setTemperature(22 + x * 10);
                temperatureSensorDataRepository.save(temperatureSensorData);

                /* Inform heat map client */
                List<WebSocketSession> list = TemperatureWebSocketHandler.getWebSocketById(sensor.getGardenId());
                if (list == null || list.isEmpty()) {
                    continue;
                }
                /* Wrap data */
                tHeatMapMap.put("id", Long.toString(temperatureSensorData.getId().getId()));
                tHeatMapMap.put("temperature", Float.toString(temperatureSensorData.getTemperature()));
                for (WebSocketSession session : list) {
                    if (session != null) {
                        String jsonString = JSON.toJSONString(tHeatMapMap);
                        try {
                            /* @format: {"id":long, "temperature":float} */
                            session.sendMessage(new TextMessage(jsonString));
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                }

                /* Inform line chart client */
                list = SingleTemperatureHandler.getWebSocketById(id);
                if (list == null || list.isEmpty()) {
                    continue;
                }
                /* Wrap data */
                tLineChartMap.put("temperature", Float.toString(temperatureSensorData.getTemperature()));
                tLineChartMap.put("time", temperatureSensorData.getId().getTime().toString());
                for (WebSocketSession session : list) {
                    if (session != null) {
                        try {
                            /* @format: {"temperature":float, "time":"YYYY-MM-DD HH:MM:SS.S"} */
                            session.sendMessage(new TextMessage(JSON.toJSONString(tLineChartMap)));
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                }
            }

            for (WetnessSensor sensor : wetnessSensorIdList) {
                long id = sensor.getId();
                SensorDataKey sensorDataKey = new SensorDataKey();
                sensorDataKey.setId(id);

                Date date = new Date();//获得系统时间.
                String nowTime = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(date);//将时间格式转换成符合Timestamp要求的格式.
                Timestamp goodsC_date = Timestamp.valueOf(nowTime);//把时间转换
                sensorDataKey.setTime(goodsC_date);

                float y = r.nextFloat();

                WetnessSensorData wetnessSensorData = new WetnessSensorData();
                wetnessSensorData.setId(sensorDataKey);
                wetnessSensorData.setWetness(y * 100);
                wetnessSensorDataRepository.save(wetnessSensorData);

                /* Inform heat map client */
                List<WebSocketSession> list = WetnessWebSocketHandler.getWebSocketById(sensor.getGardenId());
                if (list == null || list.isEmpty()) {
                    continue;
                }
                /* Wrap data */
                wHeatMapMap.put("id", Long.toString(wetnessSensorData.getId().getId()));
                wHeatMapMap.put("wetness", Float.toString(wetnessSensorData.getWetness()));
                for (WebSocketSession session : list) {
                    if (session != null) {
                        String jsonString = JSON.toJSONString(wHeatMapMap);
                        try {
                            /* @format: {"id":long, "temperature":float} */
                            session.sendMessage(new TextMessage(jsonString));
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                }

                /* Inform line chart client */
                list = SingleWetnessHandler.getWebSocketById(id);
                if (list == null || list.isEmpty()) {
                    continue;
                }
                /* Wrap data */
                wLineChartMap.put("temperature", Float.toString(wetnessSensorData.getWetness()));
                wLineChartMap.put("time", wetnessSensorData.getId().getTime().toString());
                for (WebSocketSession session : list) {
                    if (session != null) {
                        try {
                            /* @format: {"temperature":float, "time":"YYYY-MM-DD HH:MM:SS.S"} */
                            session.sendMessage(new TextMessage(JSON.toJSONString(wLineChartMap)));
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                }
            }

            i++;
            try {
                Thread.sleep(timeInterval);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }

        normalDto.setCode(0);
        normalDto.setResult("success,please close the window");
        return normalDto;
    }
}
