package com.example.myGreen.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.autoconfigure.EnableAutoConfiguration;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import java.util.Iterator;
import java.util.List;
import java.util.ArrayList;
import net.sf.json.JSONArray;

import com.example.myGreen.repository.*;
import com.example.myGreen.entity.*;
import com.example.myGreen.key.*;

@Controller
@RequestMapping("/MySQL")
@EnableAutoConfiguration
public class MySQLController {

    @Autowired
    private UserRepository userRepository;

    @RequestMapping("getUserById")
    @ResponseBody
    public User getUserById(long id) {
        User user = userRepository.findById(id).get();
        return user;
    }
}
