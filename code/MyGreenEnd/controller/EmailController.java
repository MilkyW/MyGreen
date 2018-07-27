package com.example.myGreen.controller;

import com.example.myGreen.dto.NormalDto;
import com.example.myGreen.mail.BackendService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@CrossOrigin
@RestController
public class EmailController {
    @Autowired
    private BackendService backendService;

    @RequestMapping(value = "/validate", method = RequestMethod.GET)
    public @ResponseBody
    NormalDto validate(String token) {
        return backendService.validate(token);
    }

    /* test mapping function(unfinished) */
    @RequestMapping(value = "/testmail", method = RequestMethod.GET)
    public @ResponseBody NormalDto testmail(){
        return backendService.testmail();
    }

    @RequestMapping(value = "/wifi", method = RequestMethod.GET)
    public @ResponseBody
    NormalDto wifi(String key, String temper, String humi, String check) {
        return backendService.wifi(key, temper, humi, check);
    }

    @RequestMapping(value = "/getdata", method = RequestMethod.GET)
    public @ResponseBody
    NormalDto getdata() {
        return backendService.getdata();
    }


}
