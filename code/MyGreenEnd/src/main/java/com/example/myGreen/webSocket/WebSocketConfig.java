package com.example.myGreen.webSocket;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.servlet.config.annotation.WebMvcConfigurerAdapter;
import org.springframework.web.socket.WebSocketHandler;
import org.springframework.web.socket.config.annotation.EnableWebSocket;
import org.springframework.web.socket.config.annotation.WebSocketConfigurer;
import org.springframework.web.socket.config.annotation.WebSocketHandlerRegistry;

@Configuration
//@EnableWebMvc
@EnableWebSocket
public class WebSocketConfig extends WebMvcConfigurerAdapter implements WebSocketConfigurer {

    @Override
    public void registerWebSocketHandlers(WebSocketHandlerRegistry registry) {
        registry.addHandler(temperatureWebSocketHandler(), "/getAllLatestTemperature");
        registry.addHandler(singleTemperatureHandler(), "/getSingleLatestTemperature");
//        registry.addHandler(wetnessWebSocketHandler(), "/checkLatestWetness");
//        registry.addHandler(temperatureWebSocketHandler(), "/checkLatestTemperature/sockjs").setAllowedOrigins("*").withSockJS();
//        registry.addHandler(temperatureWebSocketHandler(), "/checkLatestWetness/sockjs").setAllowedOrigins("*").withSockJS();
    }

    @Bean
    public WebSocketHandler temperatureWebSocketHandler() {
        return new TemperatureWebSocketHandler();
    }

    @Bean
    public WebSocketHandler wetnessWebSocketHandler() {
        return new WetnessWebSocketHandler();
    }

    @Bean
    public WebSocketHandler singleTemperatureHandler() {
        return new SingleTemperatureHandler();
    }
}