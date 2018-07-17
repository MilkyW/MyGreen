package com.example.myGreen;

import com.example.myGreen.tool.SpringUtil;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.ApplicationContext;

@SpringBootApplication
public class Application {

    public static void main(String[] args) {
        ApplicationContext appContext = SpringApplication.run(Application.class, args);
        SpringUtil.setApplicationContext(appContext);
    }
}
