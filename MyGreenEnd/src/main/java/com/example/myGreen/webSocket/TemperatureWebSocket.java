package com.example.myGreen.webSocket;

import org.springframework.stereotype.Component;
import javax.websocket.OnClose;
import javax.websocket.OnMessage;
import javax.websocket.OnOpen;
import javax.websocket.Session;
import javax.websocket.server.ServerEndpoint;
import java.io.IOException;
import java.util.concurrent.CopyOnWriteArraySet;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

@Component
@ServerEndpoint("/checkLatestTemperature")
public class TemperatureWebSocket {

    private static String name = "TemperatureWebSocket";

    private static int onlineCount = 0;

    private static CopyOnWriteArraySet<TemperatureWebSocket> webSocketSet = new CopyOnWriteArraySet<>();

    private Session session;

    private ExecutorService pool;

    @OnOpen
    public void onOpen(Session session) {
        this.session = session;
        webSocketSet.add(this);
        addOnlineCount();
        pool = Executors.newFixedThreadPool(1);
        printInfo("有新链接加入!当前在线人数为" + getOnlineCount());
    }

    @OnClose
    public void onClose() {
        pool.shutdownNow();
        webSocketSet.remove(this);
        subOnlineCount();
        printInfo("有一链接关闭!当前在线人数为" + getOnlineCount());
    }

    @OnMessage
    public void onMessage(String message, Session session) throws IOException {
        printInfo("来自客户端的消息:" + message);

        if (message.isEmpty()) {
            return;
        }

        long id = Long.parseLong(message);

        pool.submit(new TemperatureThreadRunner(id, session));
    }

    private void sendMessage(String message) throws IOException {
        this.session.getBasicRemote().sendText(message);
    }

    private void printInfo(String msg) {
        System.out.println(name+":"+msg);
    }

    public static synchronized int getOnlineCount() {
        return TemperatureWebSocket.onlineCount;
    }

    public static synchronized void addOnlineCount() {
        TemperatureWebSocket.onlineCount++;
    }

    public static synchronized void subOnlineCount() {
        TemperatureWebSocket.onlineCount--;
    }
}