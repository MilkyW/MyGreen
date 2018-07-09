package com.example.myGreen.WebSocket;

import com.example.myGreen.entity.TemperatureSensorData;
import com.example.myGreen.repository.TemperatureSensorDataRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import org.springframework.stereotype.Controller;
import org.springframework.context.ApplicationContext;

import javax.websocket.OnClose;
import javax.websocket.OnMessage;
import javax.websocket.OnOpen;
import javax.websocket.Session;
import javax.websocket.server.ServerEndpoint;
import java.io.IOException;
import java.sql.Timestamp;
import java.util.concurrent.CopyOnWriteArraySet;

@Component
@ServerEndpoint("/checkLatestTemperature")
public class MyWebSocket {

    private static int onlineCount = 0;

    private static CopyOnWriteArraySet<MyWebSocket> webSocketSet = new CopyOnWriteArraySet<>();

    private Session session;

    private boolean checkDatabase;

    @OnOpen
    public void onOpen (Session session){
        this.session = session;
        webSocketSet.add(this);
        addOnlineCount();
        System.out.println("有新链接加入!当前在线人数为" + getOnlineCount());
    }

    @OnClose
    public void onClose (){
        checkDatabase = false;
        webSocketSet.remove(this);
        subOnlineCount();
        System.out.println("有一链接关闭!当前在线人数为" + getOnlineCount());
    }

    @OnMessage
    public void onMessage (String message, Session session) throws IOException {
        TemperatureSensorDataRepository temperatureSensorDataRepository = SpringUtil.getBean(TemperatureSensorDataRepository.class);

        System.out.println("来自客户端的消息:" + message);

        long id = Long.parseLong(message);

        Timestamp latestTimestamp = new Timestamp(System.currentTimeMillis());

        checkDatabase = true;
        while(checkDatabase) {
            /* Check if data update */
            if (temperatureSensorDataRepository == null) {
                System.out.print("temperatureSensorDataRepository is null!");
                sendMessage("Server error");
                return;
            }

            TemperatureSensorData data = temperatureSensorDataRepository.findLatestDataById(id);
            if (data!=null) {
                if (data.getId().getTime().after(latestTimestamp)) {
                    latestTimestamp = data.getId().getTime();
                    /*广播使用convertAndSendToUser方法
                        第一个参数为用户id，此时js中的订阅地址为"/user/" + 用户Id + "/message",其中"/user"是固定的*/
                    sendMessage(String.valueOf(data.getTemperature()));
                }
            }

            try {
                Thread.sleep(500);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
    }

    public void sendMessage (String message) throws IOException {
        this.session.getBasicRemote().sendText(message);
    }

    public static synchronized  int getOnlineCount (){
        return MyWebSocket.onlineCount;
    }

    public static synchronized void addOnlineCount (){
        MyWebSocket.onlineCount++;
    }

    public static synchronized void subOnlineCount (){
        MyWebSocket.onlineCount--;
    }
}