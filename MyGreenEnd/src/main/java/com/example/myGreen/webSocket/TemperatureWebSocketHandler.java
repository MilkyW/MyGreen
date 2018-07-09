package com.example.myGreen.webSocket;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.web.socket.*;

import java.io.IOException;
import java.util.ArrayList;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

public class TemperatureWebSocketHandler implements WebSocketHandler {

    private static int onlineCount = 0;

    private Logger log = LoggerFactory.getLogger(TemperatureWebSocketHandler.class);

    private static final ArrayList<WebSocketSession> users = new ArrayList<WebSocketSession>();

    private ExecutorService pool;

    private void printInfo(String msg) {
        System.out.println("[TemperatureWebSocket]" + msg);
    }

    private static synchronized int getOnlineCount() {
        return TemperatureWebSocketHandler.onlineCount;
    }

    private static synchronized void addOnlineCount() {
        TemperatureWebSocketHandler.onlineCount++;
    }

    private static synchronized void subOnlineCount() {
        TemperatureWebSocketHandler.onlineCount--;
    }

    @Override
    public void afterConnectionEstablished(WebSocketSession session) throws Exception {
        users.add(session);
        pool = Executors.newFixedThreadPool(1);

        addOnlineCount();
        printInfo("用户连接，在线用户:" + getOnlineCount());
    }

    @Override
    public void handleMessage(WebSocketSession session, WebSocketMessage<?> message) throws Exception {
        String msg = message.getPayload().toString();
        printInfo("来自客户端的消息:" + msg);

        if (message.getPayloadLength() == 0) {
            return;
        }

        long id = Long.parseLong(msg);
        pool.submit(new TemperatureThreadRunner(id, session));
    }

    @Override
    public void handleTransportError(WebSocketSession session, Throwable exception) throws Exception {
        if (session.isOpen()) {
            session.close();
        }
        pool.shutdownNow();
        users.remove(session);

        subOnlineCount();
        printInfo("handleTransportError" + exception.getMessage());
    }

    @Override
    public void afterConnectionClosed(WebSocketSession session, CloseStatus closeStatus) throws Exception {
        users.remove(session);
        pool.shutdownNow();

        subOnlineCount();
        printInfo("用户断开，在线用户:" + getOnlineCount());
    }

    @Override
    public boolean supportsPartialMessages() {
        return false;
    }

    public void sendMessageToUsers(TextMessage message) {
        for (WebSocketSession user : users) {
            try {
                if (user.isOpen()) {
                    user.sendMessage(message);
                }
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }
}