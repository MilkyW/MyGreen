package com.example.myGreen;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.ApplicationContext;

@SpringBootApplication
public class MyGreenApplication {

    public static void main(String[] args) {
        ApplicationContext appContext = SpringApplication.run(MyGreenApplication.class, args);
        SpringUtil.setApplicationContext(appContext);
    }
}
