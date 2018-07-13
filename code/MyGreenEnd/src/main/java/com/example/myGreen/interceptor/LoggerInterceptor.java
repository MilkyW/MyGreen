package com.example.myGreen.interceptor;

import com.alibaba.fastjson.JSON;
import com.example.myGreen.tool.IP;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.web.servlet.handler.HandlerInterceptorAdapter;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

public class LoggerInterceptor extends HandlerInterceptorAdapter {

    private Logger logger = LoggerFactory.getLogger(LoggerInterceptor.class);

    @Override
    public boolean preHandle(HttpServletRequest request, HttpServletResponse response, Object handler) throws Exception {
        String sessionId = request.getRequestedSessionId();
        String uri = request.getRequestURI();
        String param = JSON.toJSONString(request.getParameterMap());
        String method = request.getMethod();
        String ip = IP.getIPAddress(request);

        logger.info("{} {} {}", ip, method, uri);

        return true;
    }
}
