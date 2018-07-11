package com.example.myGreen.webSocket;

import com.example.myGreen.SpringUtil;
import com.example.myGreen.entity.WetnessSensorData;
import com.example.myGreen.repository.WetnessSensorDataRepository;
import org.springframework.web.socket.TextMessage;
import org.springframework.web.socket.WebSocketSession;

import java.io.IOException;
import java.sql.Timestamp;

public class WetnessThread extends Thread {
    private long id;
    private WebSocketSession session;
    private static int gap = 3000;//ms

    public WetnessThread(long id, WebSocketSession session) {
        this.id = id;
        this.session = session;
    }

    private void sendMessage(String msg) throws IOException {
        this.session.sendMessage(new TextMessage(msg));
    }

    @Override
    public void run() {
        WetnessSensorDataRepository wetnessSensorDataRepository = SpringUtil.getBean(WetnessSensorDataRepository.class);

        Timestamp latestTimestamp = new Timestamp(System.currentTimeMillis());

        while (true) {
            if (this.isInterrupted()) {
                return;
            }

            /* Check if data update */
            if (wetnessSensorDataRepository == null) {
                System.out.print("wetnessSensorDataRepository is null!");
                try {
                    sendMessage("Server error");
                } catch (IOException e) {
                    e.printStackTrace();
                }
                return;
            }

            WetnessSensorData data = wetnessSensorDataRepository.findLatestDataById(id);
            if (data != null) {
                if (data.getId().getTime().after(latestTimestamp)) {
                    latestTimestamp = data.getId().getTime();
                    /*广播使用convertAndSendToUser方法
                        第一个参数为用户id，此时js中的订阅地址为"/user/" + 用户Id + "/message",其中"/user"是固定的*/
                    try {
                        sendMessage(String.valueOf(data.getWetness()));
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                }
            }

            try {
                Thread.sleep(WetnessThread.gap);
            } catch (InterruptedException e) {
                e.printStackTrace();
                return;
            }
        }
    }
}
