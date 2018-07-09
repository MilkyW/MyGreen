package com.example.myGreen.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.autoconfigure.EnableAutoConfiguration;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import java.util.List;
import java.util.Optional;

import com.example.myGreen.repository.*;
import com.example.myGreen.entity.*;

@Controller
@RequestMapping("/")
//@RequestMapping("/MySQL")
@EnableAutoConfiguration
public class MySQLController {

    @Autowired
    private UserRepository userRepository;
    @Autowired
    private GardenControllerRepository gardenControllerRepository;
    @Autowired
    private GardenRepository gardenRepository;
    @Autowired
    private TemperatureSensorRepository temperatureSensorRepository;
    @Autowired
    private WetnessSensorRepository wetnessSensorRepository;
    @Autowired
    private TemperatureSensorDataRepository temperatureSensorDataRepository;
    @Autowired
    private WetnessSensorDataRepository wetnessSensorDataRepository;

    /* User */
    @RequestMapping("getUserById")
    @ResponseBody
    public User getUserById(long id) {
        Optional<User> userOptional = userRepository.findById(id);
        if (userOptional.isPresent()) {
            return userOptional.get();
        }
        //else
        return null;
    }

    @RequestMapping("getUserByAccount")
    @ResponseBody
    public User getUserByAccount(String account) {
        return userRepository.findByAccount(account);
    }

    @RequestMapping("getUserValidByAccount")
    @ResponseBody
    public boolean getUserValidByAccount(String account) {
        return userRepository.findValidByAccount(account);
    }

    @PostMapping("saveUser")
    @ResponseBody
    public void saveUser(@RequestBody User user) {
        userRepository.save(user);
        System.out.println("id:"+user.getId());
    }

    @RequestMapping("isPhoneExist")
    @ResponseBody
    public boolean isPhoneExist(String phone) {
        System.out.println(phone);
        User user = userRepository.findByPhone(phone);
        return user!=null;
    }

    @RequestMapping("isEmailExist")
    @ResponseBody
    public boolean isEmailExist(String email) {
        System.out.println(email);
        User user = userRepository.findByEmail(email);
        return user!=null;
    }

    @RequestMapping("isAccountExist")
    @ResponseBody
    public boolean isAccountExist(String account) {
        System.out.println(account);
        User user = userRepository.findByAccount(account);
        return user!=null;
    }

    @RequestMapping("login")
    @ResponseBody
    public boolean login(String account, String password) {
        System.out.println(account);
        System.out.println(password);
        User user = userRepository.findByAccount(account);
        if (user == null) {
            System.out.println("user not found");
            return false;
        }
        if (!user.getPassword().equals(password)) {
            return false;
        }

        return true;
    }

    /* Garden */
    @RequestMapping("getGardenByUserId")
    @ResponseBody
    public List<Garden> getGardenByUserId(long userId) {
        return gardenRepository.findByUserId(userId);
    }

    @RequestMapping("getAllGarden")
    @ResponseBody
    public List<Garden> getAllGarden() {
        return gardenRepository.findAll();
    }

    @PostMapping("saveGarden")
    @ResponseBody
    public void saveGarden(@RequestBody Garden garden) {
        gardenRepository.save(garden);
    }

    /* TemperatureSensor */
    @RequestMapping("getTemperatureSensorByGardenId")
    @ResponseBody
    public List<TemperatureSensor> getTemperatureSensorByGardenId(long gardenId) {
        return temperatureSensorRepository.findByGardenId(gardenId);
    }

    @RequestMapping("getLatestWetnessByGardenId")
    @ResponseBody
    public String getLatestWetnessByGardenId(long gardenId) {
        return "";
    }

    @RequestMapping("getLatestWetnessById")
    @ResponseBody
    public float getLatestWetnessById(long id) {
        return 0;
    }

    /* WetnessSensor */
    @RequestMapping("getWetnessSensorByGardenId")
    @ResponseBody
    public List<WetnessSensor> getWetnessSensorByGardenId(long gardenId) {
        return wetnessSensorRepository.findByGardenId(gardenId);
    }

    @RequestMapping("getLatestTemperatureByGardenId")
    @ResponseBody
    public String getLatestTemperatureByGardenId(long gardenId) {
        return "";
    }

    @RequestMapping("getLatestTemperatureById")
    @ResponseBody
    public float getLatestTemperatureById(long id) {
        return 0;
    }

    /* Controller */
    @RequestMapping("getControllerByGardenId")
    @ResponseBody
    public List<GardenController> getControllerByGardenId(long gardenId) {
        return gardenControllerRepository.findByGardenId(gardenId);
    }

    @RequestMapping("updateControllerValidByControllerId")
    @ResponseBody
    public void updateControllerValidByControllerId(long id,boolean valid) {
        gardenControllerRepository.updateValidById(id, valid);
    }

    @PostMapping("saveController")
    @ResponseBody
    public void saveController(@RequestBody GardenController controller) {
        gardenControllerRepository.save(controller);
    }

    /* TemperatureSensorData */

    /* WetnessSensorData */
}
