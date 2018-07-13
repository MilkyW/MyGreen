package com.example.myGreen.controller;


import com.example.myGreen.entity.*;
import com.example.myGreen.service.ControllerService;
import com.example.myGreen.service.GardenService;
import com.example.myGreen.service.SensorService;
import com.example.myGreen.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.autoconfigure.EnableAutoConfiguration;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@Controller
@RequestMapping("/")
@CrossOrigin
@EnableAutoConfiguration
public class InterfaceController {

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

    @PostMapping("saveUser")
    @ResponseBody
    public long saveUser(@RequestBody User user) {
        userService.saveUser(user);
        return user.getId();
    }

    @PostMapping("updateUser")
    @ResponseBody
    public void updateUser(@RequestBody User user) {
        userService.updateUser(user);
    }

    /* Garden */
    @RequestMapping("getGardenByUserId")
    @ResponseBody
    public List<Garden> getGardenByUserId(long userId) {
        return gardenService.getGardenByUserId(userId);
    }

    @PostMapping("saveGarden")
    @ResponseBody
    public long saveGarden(@RequestBody Garden garden) {
        gardenService.saveGarden(garden);
        return garden.getId();
    }

    /* TemperatureSensor */
    @RequestMapping("getTemperatureSensorByGardenId")
    @ResponseBody
    public List<TemperatureSensor> getTemperatureSensorByGardenId(long gardenId) {
        return sensorService.getTemperatureSensorByGardenId(gardenId);
    }

    @PostMapping("saveTemperatureSensor")
    @ResponseBody
    public long saveTemperatureSensor(@RequestBody TemperatureSensor sensor) {
        sensorService.saveTemperatureSensor(sensor);
        return sensor.getId();
    }

    /* WetnessSensor */
    @RequestMapping("getWetnessSensorByGardenId")
    @ResponseBody
    public List<WetnessSensor> getWetnessSensorByGardenId(long gardenId) {
        return sensorService.getWetnessSensorByGardenId(gardenId);
    }

    @PostMapping("saveWetnessSensor")
    @ResponseBody
    public long saveWetnessSensor(@RequestBody WetnessSensor sensor) {
        sensorService.saveWetnessSensor(sensor);
        return sensor.getId();
    }

    /* Controller */
    @RequestMapping("getControllerByGardenId")
    @ResponseBody
    public List<GardenController> getControllerByGardenId(long gardenId) {
        return controllerService.getControllerByGardenId(gardenId);
    }

    @RequestMapping("updateControllerValidById")
    @ResponseBody
    public void updateControllerValidById(long id, boolean valid) {
        controllerService.updateControllerValidById(id, valid);
    }

    @PostMapping("saveController")
    @ResponseBody
    public long saveController(@RequestBody GardenController controller) {
        controllerService.saveController(controller);
        return controller.getId();
    }

    /* TemperatureSensorData */

    /* @Name: getLatestTemperatureByGardenId
     * @Return: {"id":long, "temperature":float }
     */
    @RequestMapping("getLatestTemperatureByGardenId")
    @ResponseBody
    public String getLatestTemperatureByGardenId(long gardenId) {
        return sensorService.getLatestTemperatureByGardenId(gardenId);
    }

    /* @Name: getLatestTemperatureByGardenId
     * @Return: {"temperature":float, "time":"YYYY-MM-DD HH:MM:SS.S" }
     */
    @RequestMapping("getRecentTemperatureDataById")
    @ResponseBody
    public String getRecentTemperatureDataById(long id, int num) {
        return sensorService.getRecentTemperatureDataById(id, num);
    }

    /* WetnessSensorData */
}
