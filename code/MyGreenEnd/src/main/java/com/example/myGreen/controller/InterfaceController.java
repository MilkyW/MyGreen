package com.example.myGreen.controller;

import com.example.myGreen.database.entity.*;
import com.example.myGreen.service.ControllerService;
import com.example.myGreen.service.GardenService;
import com.example.myGreen.service.SensorService;
import com.example.myGreen.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.autoconfigure.EnableAutoConfiguration;
import org.springframework.security.access.annotation.Secured;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * 提供接口给前端
 * 包含对用户、花园、传感器和控制器的操作
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

    @Secured("ROLE_ADMIN")
    @GetMapping("getAllUser")
    public List<User> getAllUser() {
        return userService.getAllUser();
    }

    @Secured("ROLE_ADMIN")
    @GetMapping("updateUserEnabledById")
    public boolean updateUserEnabledById(long id, boolean enabled) {
        return userService.updateUserEnabledById(id, enabled);
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

    @PostMapping("updateGarden")
    public boolean updateGardenNameById(@RequestBody Garden garden) {
        return gardenService.updateGarden(garden);
    }

    /* Temperature Sensor */
    @GetMapping("getTemperatureSensorByGardenId")
    public List<TemperatureSensor> getTemperatureSensorByGardenId(long gardenId) {
        return sensorService.getTemperatureSensorByGardenId(gardenId);
    }

    @PostMapping("saveTemperatureSensor")
    public long saveTemperatureSensor(@RequestBody TemperatureSensor sensor) {
        sensorService.saveTemperatureSensor(sensor);
        return sensor.getId();
    }

    @PostMapping("updateTemperatureSensorNameById")
    public boolean updateTemperatureSensorNameById(long id, String name) {
        return sensorService.updateTemperatureSensorNameById(id, name);
    }

    @GetMapping("deleteTemperatureSensorById")
    public boolean deleteTemperatureSensorById(long id) {
        return sensorService.deleteTemperatureSensorById(id);
    }

    /* Wetness Sensor */
    @GetMapping("getWetnessSensorByGardenId")
    public List<WetnessSensor> getWetnessSensorByGardenId(long gardenId) {
        return sensorService.getWetnessSensorByGardenId(gardenId);
    }

    @PostMapping("saveWetnessSensor")
    public long saveWetnessSensor(@RequestBody WetnessSensor sensor) {
        sensorService.saveWetnessSensor(sensor);
        return sensor.getId();
    }

    @PostMapping("updateWetnessSensorNameById")
    public boolean updateWetnessSensorNameById(long id, String name) {
        return sensorService.updateWetnessSensorNameById(id, name);
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

    @PostMapping("updateControllerNameById")
    public boolean updateControllerNameById(long id, String name) {
        return controllerService.updateControllerNameById(id, name);
    }

    @GetMapping("deleteControllerById")
    public boolean deleteControllerById(long id) {
        return controllerService.deleteControllerById(id);
    }

    /* Temperature Sensor Data */

    /**
     * @return {"id":long, "temperature":float }
     */
    @GetMapping("getLatestTemperatureByGardenId")
    public String getLatestTemperatureByGardenId(long gardenId) {
        return sensorService.getLatestTemperatureByGardenId(gardenId);
    }

    /**
     * @return {"temperature":float, "time":"YYYY-MM-DD HH:MM:SS.S" }
     */
    @GetMapping("getRecentTemperatureDataById")
    public String getRecentTemperatureDataById(long id, int num) {
        return sensorService.getRecentTemperatureDataById(id, num);
    }

    /* Wetness Sensor Data */
    /**
     * @return {"id":long, "wetness":float }
     */
    @GetMapping("getLatestWetnessByGardenId")
    public String getLatestWetnessByGardenId(long gardenId) {
        return sensorService.getLatestWetnessByGardenId(gardenId);
    }

    /**
     * @return {"wetness":float, "time":"YYYY-MM-DD HH:MM:SS.S" }
     */
    @GetMapping("getRecentWetnessDataById")
    public String getRecentWetnessDataById(long id, int num) {
        return sensorService.getRecentWetnessDataById(id, num);
    }
}
