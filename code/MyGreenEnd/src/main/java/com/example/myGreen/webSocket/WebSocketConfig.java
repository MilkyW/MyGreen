package com.example.myGreen.webSocket;

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
        registry.addHandler(tHeatMapHandler(), "/getAllLatestTemperature");
        registry.addHandler(tLineChartHandler(), "/getSingleLatestTemperature");
        registry.addHandler(wHeatMapHandler(), "/getAllLatestWetness");
        registry.addHandler(wLineChartHandler(), "/getSingleLatestWetness");
//        registry.addHandler(wetnessWebSocketHandler(), "/checkLatestWetness");
//        registry.addHandler(temperatureWebSocketHandler(), "/checkLatestTemperature/sockjs").setAllowedOrigins("*").withSockJS();
//        registry.addHandler(temperatureWebSocketHandler(), "/checkLatestWetness/sockjs").setAllowedOrigins("*").withSockJS();
    }

    @Bean
    public WebSocketHandler tHeatMapHandler() {
        return new THeatMapHandler();
    }

    @Bean
    public WebSocketHandler wHeatMapHandler() {
        return new WHeatMapHandler();
    }

    @Bean
    public WebSocketHandler tLineChartHandler() {
        return new TLineChartHandler();
    }

    @Bean
    public WebSocketHandler wLineChartHandler() {
        return new WLineChartHandler();
    }
}