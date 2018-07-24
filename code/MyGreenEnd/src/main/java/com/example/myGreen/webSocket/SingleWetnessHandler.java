package com.example.myGreen.webSocket;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.web.socket.*;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

public class SingleWetnessHandler implements WebSocketHandler {

    private static int onlineCount = 0;

    private static Logger log = LoggerFactory.getLogger(SingleWetnessHandler.class);

    private static final ArrayList<WebSocketSession> users = new ArrayList<>();

    private static final Map<Long, List<WebSocketSession>> map = new ConcurrentHashMap<>();

    private static synchronized int getOnlineCount() {
        return SingleWetnessHandler.onlineCount;
    }

    private static synchronized void addOnlineCount() {
        SingleWetnessHandler.onlineCount++;
        log.info("用户连接，在线用户:" + getOnlineCount());
    }

    private static synchronized void subOnlineCount() {
        SingleWetnessHandler.onlineCount--;
        log.info("用户断开，在线用户:" + getOnlineCount());
    }

    private static void deleteSessionFromMap(WebSocketSession session) {
        if (!session.getAttributes().containsKey("id")) {
            return;
        }
        long id = (long) session.getAttributes().get("id");
        if (map.containsKey(id)) {
            map.get(id).remove(session);
        }
    }

    public static List<WebSocketSession> getWebSocketById(long id) {
        return map.get(id);
    }

    /*
     * WebSocket
     */
    @Override
    public void afterConnectionEstablished(WebSocketSession session) throws Exception {
        users.add(session);

        addOnlineCount();
    }

    @Override
    public void handleMessage(WebSocketSession session, WebSocketMessage<?> message) throws Exception {
        String msg = message.getPayload().toString();
        log.info("来自客户端{}的消息: {}", session.getRemoteAddress().getHostString(), msg);
        if (message.getPayloadLength() == 0) {
            return;
        }

        /* 注册消息推送 */
        long id = Long.parseLong(msg);
        session.getAttributes().put("id", id);
        if (!map.containsKey(id)) {
            map.putIfAbsent(id, new ArrayList<WebSocketSession>());
        }
        List<WebSocketSession> list = map.get(id);
        list.add(session);
    }

    @Override
    public void handleTransportError(WebSocketSession session, Throwable exception) throws Exception {
        if (session.isOpen()) {
            session.close();
        }

        deleteSessionFromMap(session);
        users.remove(session);

        log.info("handleTransportError" + exception.getMessage());
    }

    @Override
    public void afterConnectionClosed(WebSocketSession session, CloseStatus closeStatus) throws Exception {
        deleteSessionFromMap(session);
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
