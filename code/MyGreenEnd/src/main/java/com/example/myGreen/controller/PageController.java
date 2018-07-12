package com.example.myGreen.controller;

import com.example.myGreen.service.SensorDataGeneratorService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

@CrossOrigin
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

    @RequestMapping("/loginPage")
    @ResponseBody
    public String loginPage() {
        return "please login";
    }
}