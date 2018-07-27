package com.example.myGreen.httpReqPreHandle.interceptor;

import com.alibaba.fastjson.JSON;
import com.example.myGreen.tool.IP;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.web.servlet.handler.HandlerInterceptorAdapter;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.BufferedReader;

/**
 * 拦截HTTP请求，以日志形式输出请求信息
 * 信息包括：IP地址，请求方法，URI，请求参数，BODY
 */
public class LoggerInterceptor extends HandlerInterceptorAdapter {

    private static Logger logger = LoggerFactory.getLogger(LoggerInterceptor.class);

    @Override
    public boolean preHandle(HttpServletRequest request, HttpServletResponse response, Object handler) throws Exception {
        String uri = request.getRequestURI();
        String param = JSON.toJSONString(request.getParameterMap());
        String method = request.getMethod();
        String ip = IP.getIPAddress(request);

        /* Get body */
        BufferedReader br = request.getReader();
        String str;
        String body = "";
        while ((str = br.readLine()) != null) {
            body += str;
        }

        logger.info("{} {} {} {} {}", ip, method, uri, param, body);

        return true;
    }
}
