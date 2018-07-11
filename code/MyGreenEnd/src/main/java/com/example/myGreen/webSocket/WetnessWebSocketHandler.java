package com.example.myGreen.webSocket;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.web.socket.*;

import java.io.IOException;
import java.util.ArrayList;
import java.util.HashMap;

public class WetnessWebSocketHandler implements WebSocketHandler {

    private static int onlineCount = 0;

    private static Logger log = LoggerFactory.getLogger(WetnessWebSocketHandler.class);

    private static final ArrayList<WebSocketSession> users = new ArrayList<WebSocketSession>();

    private static HashMap<String, WetnessThread> map = new HashMap<String, WetnessThread>();


    private static void printInfo(String msg) {
        log.info(msg);
    }

    private static synchronized int getOnlineCount() {
        return WetnessWebSocketHandler.onlineCount;
    }

    private static synchronized void addOnlineCount() {
        WetnessWebSocketHandler.onlineCount++;
        printInfo("用户连接，在线用户:" + getOnlineCount());
    }

    private static synchronized void subOnlineCount() {
        WetnessWebSocketHandler.onlineCount--;
        printInfo("用户断开，在线用户:" + getOnlineCount());
    }

    private void stopThread(WebSocketSession session) {
        WetnessThread thread = map.get(session.getId());
        if (thread != null) {
            thread.interrupt();
            map.remove(session.getId(), thread);
        }
    }

    @Override
    public void afterConnectionEstablished(WebSocketSession session) throws Exception {
        users.add(session);

        addOnlineCount();
    }

    @Override
    public void handleMessage(WebSocketSession session, WebSocketMessage<?> message) throws Exception {
        String msg = message.getPayload().toString();
        printInfo("来自客户端的消息:" + msg);

        if (message.getPayloadLength() == 0) {
            return;
        }

        long id = Long.parseLong(msg);
        WetnessThread thread = new WetnessThread(id, session);
        map.put(session.getId(), thread);
        thread.start();
    }

    @Override
    public void handleTransportError(WebSocketSession session, Throwable exception) throws Exception {
        if (session.isOpen()) {
            session.close();
        }

        stopThread(session);
        users.remove(session);

        subOnlineCount();
        printInfo("handleTransportError" + exception.getMessage());
    }

    @Override
    public void afterConnectionClosed(WebSocketSession session, CloseStatus closeStatus) throws Exception {
        stopThread(session);
        users.remove(session);

        subOnlineCount();
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