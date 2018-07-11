package com.example.myGreen.controller;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;

@Controller
public class PageController {

    @GetMapping("/")
    public String Greeting() {
        return "/index.html";
    }

    @GetMapping("/webSocketTest")
    public String webSocket() {
        return "/webSocket.html";
    }
}