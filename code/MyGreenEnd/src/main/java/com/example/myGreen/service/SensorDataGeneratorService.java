package com.example.myGreen.service;

import com.alibaba.fastjson.JSON;
import com.example.myGreen.dto.NormalDto;
import com.example.myGreen.dto.SensorInfo;
import com.example.myGreen.entity.TemperatureSensor;
import com.example.myGreen.entity.TemperatureSensorData;
import com.example.myGreen.entity.WetnessSensorData;
import com.example.myGreen.entity.key.SensorDataKey;
import com.example.myGreen.repository.TemperatureSensorDataRepository;
import com.example.myGreen.repository.TemperatureSensorRepository;
import com.example.myGreen.repository.WetnessSensorDataRepository;
import com.example.myGreen.repository.WetnessSensorRepository;
import com.example.myGreen.webSocket.TemperatureWebSocketHandler;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.web.socket.TextMessage;
import org.springframework.web.socket.WebSocketSession;

import java.sql.Timestamp;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;

@Service
public class SensorDataGeneratorService {

    @Autowired
    private TemperatureSensorRepository temperatureSensorRepository;
    @Autowired
    private WetnessSensorRepository wetnessSensorRepository;
    @Autowired
    private TemperatureSensorDataRepository temperatureSensorDataRepository;
    @Autowired
    private WetnessSensorDataRepository wetnessSensorDataRepository;

    /*  data generation
     *  every 3 seconds insert a new line
     *  include WetnessSensorData, TemperatureSensorData
     */
    public NormalDto generate() {
        NormalDto normalDto = new NormalDto();

        /* Get sensors' ID and gardenId */
        List<TemperatureSensor> temperatureSensorList = temperatureSensorRepository.findSensorInfo();
        //List<Long> wetnessSensorIdList = wetnessSensorRepository.findAllId();

        // run every 3 seconds
        final long timeInterval = 3000;//ms
        int i = 0;
        while (i < 1000) {
            // ------- code for task to run
            java.util.Random r = new java.util.Random();

            for (TemperatureSensor sensor: temperatureSensorList) {
                long id = sensor.getId();
                SensorDataKey sensorDataKey = new SensorDataKey();
                sensorDataKey.setId(id);

                Date date = new Date();//获得系统时间.
                String nowTime = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(date);//将时间格式转换成符合Timestamp要求的格式.
                Timestamp goodsC_date = Timestamp.valueOf(nowTime);//把时间转换
                sensorDataKey.setTime(goodsC_date);

                float x = r.nextFloat() - 0.5f;

                TemperatureSensorData temperatureSensorData = new TemperatureSensorData();
                temperatureSensorData.setId(sensorDataKey);
                temperatureSensorData.setTemperature(22 + x * 10);
                temperatureSensorDataRepository.save(temperatureSensorData);

                /* Inform client */
                WebSocketSession session = TemperatureWebSocketHandler.getWebSocketByGardenId(sensor.getGardenId());
                if (session != null) {
                    try {
                        /* Turn data into json string */
                        session.sendMessage(new TextMessage(JSON.toJSONString(temperatureSensorData)));
                    }catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            }

//            for (long id : wetnessSensorIdList) {
//                SensorDataKey sensorDataKey = new SensorDataKey();
//                sensorDataKey.setId(id);
//
//                Date date = new Date();//获得系统时间.
//                String nowTime = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(date);//将时间格式转换成符合Timestamp要求的格式.
//                Timestamp goodsC_date = Timestamp.valueOf(nowTime);//把时间转换
//                sensorDataKey.setTime(goodsC_date);
//
//                float y = r.nextFloat();
//
//                WetnessSensorData wetnessSensorData = new WetnessSensorData();
//                wetnessSensorData.setId(sensorDataKey);
//                wetnessSensorData.setWetness(y * 100);
//                wetnessSensorDataRepository.save(wetnessSensorData);
//
//                /* Inform client */
//            }

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
