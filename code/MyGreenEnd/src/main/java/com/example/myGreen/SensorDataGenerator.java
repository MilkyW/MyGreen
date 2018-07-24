package com.example.myGreen;

import com.example.myGreen.service.SensorDataGeneratorService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.ApplicationArguments;
import org.springframework.boot.ApplicationRunner;
import org.springframework.core.annotation.Order;
import org.springframework.stereotype.Component;

/**
 * 实现ApplicationRunner接口，Springboot容器加载完后自动开始执行。负责模拟生成传感器数据
 */
@Component
@Order(value = 1)
public class SensorDataGenerator implements ApplicationRunner {

    @Autowired
    SensorDataGeneratorService service;

    @Override
    public void run(ApplicationArguments args) throws Exception {
        service.generate();
    }
}
