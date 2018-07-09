package com.example.myGreen.webSocket;

import com.example.myGreen.SpringUtil;
import com.example.myGreen.entity.TemperatureSensorData;
import com.example.myGreen.repository.TemperatureSensorDataRepository;
import org.springframework.web.socket.TextMessage;
import org.springframework.web.socket.WebSocketSession;

import java.io.IOException;
import java.sql.Timestamp;

public class TemperatureThread extends Thread {
    private long id;
    private WebSocketSession session;
    private static int gap = 500;//ms

    public TemperatureThread(long id, WebSocketSession session) {
        this.id = id;
        this.session = session;
    }

    private void sendMessage(String msg) throws IOException {
        this.session.sendMessage(new TextMessage(msg));
    }

    @Override
    public void run() {
        TemperatureSensorDataRepository temperatureSensorDataRepository = SpringUtil.getBean(TemperatureSensorDataRepository.class);

        Timestamp latestTimestamp = new Timestamp(System.currentTimeMillis());

        while (true) {
            if(this.isInterrupted()) {
                return;
            }

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
                Thread.sleep(TemperatureThread.gap);
            } catch (InterruptedException e) {
                e.printStackTrace();
                return;
            }
        }
    }
}
