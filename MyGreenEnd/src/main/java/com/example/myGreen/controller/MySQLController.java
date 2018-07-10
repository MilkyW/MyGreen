package com.example.myGreen.controller;


import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.autoconfigure.EnableAutoConfiguration;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.List;

import com.example.myGreen.entity.*;
import com.example.myGreen.service.*;

@Controller
@RequestMapping("/")
//@RequestMapping("/MySQL")
@EnableAutoConfiguration
public class MySQLController {

    @Autowired
    private UserService userService;
    @Autowired
    private GardenService gardenService;
    @Autowired
    private ControllerService controllerService;
    @Autowired
    private SensorService sensorService;

    /* User */
    @RequestMapping("getUserByAccount")
    @ResponseBody
    public User getUserByAccount(String account) {
        return userService.getUserByAccount(account);
    }

    @PostMapping("saveUser")
    @ResponseBody
    public void saveUser(@RequestBody User user) {
        userService.saveUser(user);
    }

    @RequestMapping("isPhoneExist")
    @ResponseBody
    public boolean isPhoneExist(String phone) {
        return userService.isPhoneExist(phone);
    }

    @RequestMapping("isEmailExist")
    @ResponseBody
    public boolean isEmailExist(String email) {
        return userService.isEmailExist(email);
    }

    @RequestMapping("isAccountExist")
    @ResponseBody
    public boolean isAccountExist(String account) {
        return userService.isAccountExist(account);
    }

    @RequestMapping("login")
    @ResponseBody
    public boolean login(String account, String password) {
        return userService.login(account, password);
    }

    /* Garden */
    @RequestMapping("getGardenByUserId")
    @ResponseBody
    public List<Garden> getGardenByUserId(long userId) {
        return gardenService.getGardenByUserId(userId);
    }

    @PostMapping("saveGarden")
    @ResponseBody
    public void saveGarden(@RequestBody Garden garden) {
        gardenService.saveGarden(garden);
    }

    /* TemperatureSensor */
    @RequestMapping("getTemperatureSensorByGardenId")
    @ResponseBody
    public List<TemperatureSensor> getTemperatureSensorByGardenId(long gardenId) {
        return sensorService.getTemperatureSensorByGardenId(gardenId);
    }

    @PostMapping("saveTemperatureSensor")
    @ResponseBody
    public void saveTemperatureSensor(TemperatureSensor sensor) {
        sensorService.saveTemperatureSensor(sensor);
    }

    /* WetnessSensor */
    @RequestMapping("getWetnessSensorByGardenId")
    @ResponseBody
    public List<WetnessSensor> getWetnessSensorByGardenId(long gardenId) {
        return sensorService.getWetnessSensorByGardenId(gardenId);
    }

    @PostMapping("saveWetnessSensor")
    @ResponseBody
    public void saveWetnessSensor(WetnessSensor sensor) {
        sensorService.saveWetnessSensor(sensor);
    }

    /* Controller */
    @RequestMapping("getControllerByGardenId")
    @ResponseBody
    public List<GardenController> getControllerByGardenId(long gardenId) {
        return controllerService.getControllerByGardenId(gardenId);
    }

    @RequestMapping("updateControllerValidByControllerId")
    @ResponseBody
    public void updateControllerValidByControllerId(long id,boolean valid) {
        controllerService.updateControllerValidByControllerId(id, valid);
    }

    @PostMapping("saveController")
    @ResponseBody
    public void saveController(@RequestBody GardenController controller) {
        controllerService.saveController(controller);
    }

    /* TemperatureSensorData */

    /* WetnessSensorData */
}
