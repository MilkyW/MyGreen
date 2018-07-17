package com.example.myGreen.controller;

import com.example.myGreen.entity.*;
import com.example.myGreen.service.ControllerService;
import com.example.myGreen.service.GardenService;
import com.example.myGreen.service.SensorService;
import com.example.myGreen.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.autoconfigure.EnableAutoConfiguration;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/* @Name: InterfaceController
 * @Desc: 提供接口给前端，对用户、花园、传感器和控制器进行操作
 */
@RestController
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
    @GetMapping("getUserByAccount")
    public User getUserByAccount(String account) {
        return userService.getUserByAccount(account);
    }

    @GetMapping("isPhoneExist")
    public boolean isPhoneExist(String phone) {
        return userService.isPhoneExist(phone);
    }

    @GetMapping("isEmailExist")
    public boolean isEmailExist(String email) {
        return userService.isEmailExist(email);
    }

    @GetMapping("isAccountExist")
    public boolean isAccountExist(String account) {
        return userService.isAccountExist(account);
    }

    @PostMapping("saveUser")
    public long saveUser(@RequestBody User user) {
        userService.saveUser(user);
        return user.getId();
    }

    @PostMapping("updateUser")
    public void updateUser(@RequestBody User user) {
        userService.updateUser(user);
    }

    /* Garden */
    @GetMapping("getGardenByUserId")
    public List<Garden> getGardenByUserId(long userId) {
        return gardenService.getGardenByUserId(userId);
    }

    @PostMapping("saveGarden")
    public long saveGarden(@RequestBody Garden garden) {
        gardenService.saveGarden(garden);
        return garden.getId();
    }

    @GetMapping("deleteGardenById")
    public boolean deleteGardenById(long id) {
        return gardenService.deleteGardenById(id);
    }

    /* TemperatureSensor */
    @GetMapping("getTemperatureSensorByGardenId")
    public List<TemperatureSensor> getTemperatureSensorByGardenId(long gardenId) {
        return sensorService.getTemperatureSensorByGardenId(gardenId);
    }

    @PostMapping("saveTemperatureSensor")
    public long saveTemperatureSensor(@RequestBody TemperatureSensor sensor) {
        sensorService.saveTemperatureSensor(sensor);
        return sensor.getId();
    }

    @GetMapping("deleteTemperatureSensorById")
    public boolean deleteTemperatureSensorById(long id) {
        return sensorService.deleteTemperatureSensorById(id);
    }

    /* WetnessSensor */
    @GetMapping("getWetnessSensorByGardenId")
    public List<WetnessSensor> getWetnessSensorByGardenId(long gardenId) {
        return sensorService.getWetnessSensorByGardenId(gardenId);
    }

    @PostMapping("saveWetnessSensor")
    public long saveWetnessSensor(@RequestBody WetnessSensor sensor) {
        sensorService.saveWetnessSensor(sensor);
        return sensor.getId();
    }

    @GetMapping("deleteWetnessSensorById")
    public boolean deleteWetnessSensorById(long id) {
        return sensorService.deleteWetnessSensorById(id);
    }

    /* Controller */
    @GetMapping("getControllerByGardenId")
    public List<GardenController> getControllerByGardenId(long gardenId) {
        return controllerService.getControllerByGardenId(gardenId);
    }

    @GetMapping("updateControllerValidById")
    public void updateControllerValidById(long id, boolean valid) {
        controllerService.updateControllerValidById(id, valid);
    }

    @PostMapping("saveController")
    public long saveController(@RequestBody GardenController controller) {
        controllerService.saveController(controller);
        return controller.getId();
    }

    @GetMapping("deleteControllerById")
    public boolean deleteControllerById(long id) {
        return controllerService.deleteControllerById(id);
    }

    /* TemperatureSensorData */

    /* @Name: getLatestTemperatureByGardenId
     * @Return: {"id":long, "temperature":float }
     */
    @GetMapping("getLatestTemperatureByGardenId")
    public String getLatestTemperatureByGardenId(long gardenId) {
        return sensorService.getLatestTemperatureByGardenId(gardenId);
    }

    /* @Name: getLatestTemperatureByGardenId
     * @Return: {"temperature":float, "time":"YYYY-MM-DD HH:MM:SS.S" }
     */
    @GetMapping("getRecentTemperatureDataById")
    public String getRecentTemperatureDataById(long id, int num) {
        return sensorService.getRecentTemperatureDataById(id, num);
    }

    /* WetnessSensorData */
}
