package com.example.myGreen.controller;

import com.example.myGreen.WebSocket.ReceiveMessage;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.messaging.handler.annotation.MessageMapping;
import org.springframework.messaging.simp.SimpMessagingTemplate;
import org.springframework.stereotype.Controller;

import com.example.myGreen.repository.*;
import com.example.myGreen.entity.*;

import java.sql.Timestamp;

@Controller
public class WebSocketController {

    @Autowired
    private UserRepository userRepository;
    @Autowired
    private GardenControllerRepository gardenControllerRepository;
    @Autowired
    private GardenRepository gardenRepository;
    @Autowired
    private TemperatureSensorRepository temperatureSensorRepository;
    @Autowired
    private WetnessSensorRepository wetnessSensorRepository;
    @Autowired
    private TemperatureSensorDataRepository temperatureSensorDataRepository;
    @Autowired
    private WetnessSensorDataRepository wetnessSensorDataRepository;

    @Autowired
    public SimpMessagingTemplate template;

    @MessageMapping("/temperature")
    public void temperature(ReceiveMessage rm) {
        System.out.println("Start checking database...");

        long id = Long.parseLong(rm.getId());
        Timestamp latestTimestamp = new Timestamp(System.currentTimeMillis());

        while(true) {
            TemperatureSensorData data = temperatureSensorDataRepository.findLatestDataById(id);
            if (data!=null) {
                if (data.getId().getTime().after(latestTimestamp)) {
                    latestTimestamp = data.getId().getTime();
                    /*广播使用convertAndSendToUser方法
                        第一个参数为用户id，此时js中的订阅地址为"/user/" + 用户Id + "/message",其中"/user"是固定的*/
                    template.convertAndSendToUser(rm.getId(), "/message", String.valueOf(data.getTemperature()));
                }
            }

            try {
                Thread.sleep(500);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
    }
}
