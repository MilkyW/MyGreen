package com.example.myGreen.controller;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

@CrossOrigin
@Controller
public class PageController {

    /**
     * 未授权的请求自动导向此处
     */
    @RequestMapping("/loginPage")
    @ResponseBody
    public String loginPage() {
        return "please login";
    }

    @RequestMapping("/")
    public String index() {
        return "index.html";
    }
}