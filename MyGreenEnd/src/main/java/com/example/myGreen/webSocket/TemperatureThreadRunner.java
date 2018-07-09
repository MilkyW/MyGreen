package com.example.myGreen.webSocket;

import com.example.myGreen.SpringUtil;
import com.example.myGreen.entity.TemperatureSensorData;
import com.example.myGreen.repository.TemperatureSensorDataRepository;

import javax.websocket.Session;
import java.io.IOException;
import java.sql.Timestamp;

public class TemperatureThreadRunner implements Runnable{
    private long id;
    private Session session;
    private static int gap = 500;//ms

    public TemperatureThreadRunner(long id, Session session) {
        this.id = id;
        this.session = session;
    }

    private void sendMessage(String msg) throws IOException {
        this.session.getBasicRemote().sendText(msg);
    }


    @Override
    public void run() {
        TemperatureSensorDataRepository temperatureSensorDataRepository = SpringUtil.getBean(TemperatureSensorDataRepository.class);

        Timestamp latestTimestamp = new Timestamp(System.currentTimeMillis());

        while (true) {
            /* Check if data update */
            if (temperatureSensorDataRepository == null) {
                System.out.print("temperatureSensorDataRepository is null!");
                try {
                    sendMessage("Server error");
                } catch (IOException e) {
                    e.printStackTrace();
                }
                return;
            }

            TemperatureSensorData data = temperatureSensorDataRepository.findLatestDataById(id);
            if (data != null) {
                if (data.getId().getTime().after(latestTimestamp)) {
                    latestTimestamp = data.getId().getTime();
                    /*广播使用convertAndSendToUser方法
                        第一个参数为用户id，此时js中的订阅地址为"/user/" + 用户Id + "/message",其中"/user"是固定的*/
                    try {
                        sendMessage(String.valueOf(data.getTemperature()));
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                }
            }

            try {
                Thread.sleep(TemperatureThreadRunner.gap);
            } catch (InterruptedException e) {
                e.printStackTrace();
                return;
            }
        }
    }
}