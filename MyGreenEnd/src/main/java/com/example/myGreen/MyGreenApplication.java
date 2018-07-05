package com.example.myGreen;

import com.example.myGreen.WebSocket.SpringUtil;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.ApplicationContext;

@SpringBootApplication
public class MyGreenApplication {

    public static void main(String[] args) {
        ApplicationContext ctx = SpringApplication.run(MyGreenApplication.class, args);
        SpringUtil.setApplicationContext(ctx);

        String[] beanNames =  ctx.getBeanDefinitionNames();
        System.out.println("beanNames个数："+beanNames.length);
        for(String bn:beanNames) {
            System.out.println(bn);
        }
    }
}
