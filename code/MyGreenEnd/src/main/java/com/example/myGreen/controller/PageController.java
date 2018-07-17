package com.example.myGreen.controller;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

@CrossOrigin
@Controller
public class PageController {

    @RequestMapping("/loginPage")
    @ResponseBody
    public String loginPage() {
        return "please login";
    }
}