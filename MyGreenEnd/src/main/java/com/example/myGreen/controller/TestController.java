package com.example.myGreen.controller;


import com.example.myGreen.dto.NormalDto;
import com.example.myGreen.mail.BackendService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@CrossOrigin
@RestController
public class TestController {
    @Autowired
    private BackendService backendService;

    @RequestMapping(value = "/datageneration", method = RequestMethod.GET)
    public @ResponseBody
    NormalDto generation() {
        return backendService.generation();
    }
}
