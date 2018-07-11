package com.example.myGreen.controller;

import com.alibaba.fastjson.JSON;
import com.alibaba.fastjson.JSONArray;

import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.http.MediaType;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.MvcResult;
import org.springframework.test.web.servlet.request.MockMvcRequestBuilders;
import org.springframework.test.web.servlet.result.MockMvcResultHandlers;
import org.springframework.test.web.servlet.result.MockMvcResultMatchers;
import org.springframework.test.web.servlet.setup.MockMvcBuilders;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.web.context.WebApplicationContext;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.example.myGreen.Application;
import com.example.myGreen.entity.Garden;
import com.example.myGreen.entity.GardenController;
import com.example.myGreen.entity.TemperatureSensor;
import com.example.myGreen.entity.WetnessSensor;

@RunWith(SpringRunner.class)
@SpringBootTest(classes = Application.class)
public class MySQLControllerTest {

    @Autowired
    private WebApplicationContext context;
    private MockMvc mvc;

    private String route = "/";

    @Before
    public void setUp() throws Exception {
        mvc = MockMvcBuilders.webAppContextSetup(context).build();
    }

    @Test
    public void getUserByAccount() throws Exception {
        mvc.perform(MockMvcRequestBuilders.get(route + "getUserByAccount")
                .param("account", "dennis")
                .accept(MediaType.APPLICATION_JSON))
                .andExpect(MockMvcResultMatchers.status().isOk())
                .andDo(MockMvcResultHandlers.print())
                .andExpect(MockMvcResultMatchers.jsonPath("$.account").value("dennis"));
    }

    @Test
    public void isPhoneExist() throws Exception {
        mvc.perform(MockMvcRequestBuilders.get(route + "isPhoneExist")
                .param("phone", "13000000000")
                .accept(MediaType.APPLICATION_JSON))
                .andExpect(MockMvcResultMatchers.status().isOk())
                .andDo(MockMvcResultHandlers.print())
                .andExpect(MockMvcResultMatchers.content().string("true"));
    }

    @Test
    public void isEmailExist() throws Exception {
        mvc.perform(MockMvcRequestBuilders.get(route + "isEmailExist")
                .param("email", "123456@qq.com")
                .accept(MediaType.APPLICATION_JSON))
                .andExpect(MockMvcResultMatchers.status().isOk())
                .andDo(MockMvcResultHandlers.print())
                .andExpect(MockMvcResultMatchers.content().string("true"));
    }

    @Test
    public void isAccountExist() throws Exception {
        mvc.perform(MockMvcRequestBuilders.get(route + "isAccountExist")
                .param("account", "dennis")
                .accept(MediaType.APPLICATION_JSON))
                .andExpect(MockMvcResultMatchers.status().isOk())
                .andDo(MockMvcResultHandlers.print())
                .andExpect(MockMvcResultMatchers.content().string("true"));
    }

    @Test
    public void login() throws Exception {
        mvc.perform(MockMvcRequestBuilders.get(route + "login")
                .param("account", "dennis").param("password", "123456")
                .accept(MediaType.APPLICATION_JSON))
                .andExpect(MockMvcResultMatchers.status().isOk())
                .andDo(MockMvcResultHandlers.print())
                .andExpect(MockMvcResultMatchers.content().string("true"));

        mvc.perform(MockMvcRequestBuilders.get(route + "login")
                .param("account", "dennis").param("password", "12345")
                .accept(MediaType.APPLICATION_JSON))
                .andExpect(MockMvcResultMatchers.status().isOk())
                .andDo(MockMvcResultHandlers.print())
                .andExpect(MockMvcResultMatchers.content().string("false"));
    }

    @Test
    @Transactional
    public void saveUser() throws Exception {
        Map<String, Object> map = new HashMap<>();
        map.put("id", "");
        map.put("account", "wly");
        map.put("password", "654321");
        map.put("nickname", "dawu");
        map.put("gender", "false");
        map.put("email", "1276704961@qq.com");
        map.put("phone", "15000000000");
        map.put("valid", "false");
        map.put("firstname", "lianyi");
        map.put("lastname", "wu");

        mvc.perform(MockMvcRequestBuilders.post(route + "saveUser")
                .contentType(MediaType.APPLICATION_JSON_UTF8)
                .content(JSON.toJSONString(map)))
                .andDo(MockMvcResultHandlers.print())
                .andExpect(MockMvcResultMatchers.status().isOk());
    }

    @Test
    @Transactional
    public void updateUser() throws Exception {
        Map<String, Object> map = new HashMap<>();
        map.put("id", "1");
        map.put("account", "dennis");
        map.put("password", "asdf");
        map.put("nickname", "DENNIS");
        map.put("gender", "false");
        map.put("email", "654321@qq.com");
        map.put("phone", "15000000000");
        map.put("valid", "true");
        map.put("firstname", "dongxian");
        map.put("lastname", "ye");

        mvc.perform(MockMvcRequestBuilders.post(route + "updateUser")
                .contentType(MediaType.APPLICATION_JSON_UTF8)
                .content(JSON.toJSONString(map)))
                .andDo(MockMvcResultHandlers.print())
                .andExpect(MockMvcResultMatchers.status().isOk());

        mvc.perform(MockMvcRequestBuilders.get(route + "getUserByAccount")
                .param("account","dennis")
                .accept(MediaType.APPLICATION_JSON))
                .andDo(MockMvcResultHandlers.print())
                .andExpect(MockMvcResultMatchers.status().isOk())
                .andExpect(MockMvcResultMatchers.jsonPath("$.nickname").value("DENNIS"));
    }

    @Test
    public void getGardenByUserId() throws Exception{
        MvcResult result = mvc.perform(MockMvcRequestBuilders.get(route + "getGardenByUserId")
                .param("userId","1")
                .accept(MediaType.APPLICATION_JSON))
                .andDo(MockMvcResultHandlers.print())
                .andExpect(MockMvcResultMatchers.status().isOk())
                .andReturn();

        ArrayList<Garden> gardenList=new ArrayList<>();
        JSONArray jsonList = JSON.parseArray(result.getResponse().getContentAsString());
        for (Object jsonObject : jsonList) {
            Garden garden = JSON.parseObject(jsonObject.toString(), Garden.class);
            Assert.assertEquals(garden.getUserId(), 1);
        }
    }

    @Test
    @Transactional
    public void saveGarden() throws Exception {
        Garden garden = new Garden();
        garden.setUserId(1);
        garden.setLength(250);
        garden.setWidth(251);
        garden.setName("dennis's garden");
        garden.setIdealTemperature(24);
        garden.setIdealWetness(50);

        mvc.perform(MockMvcRequestBuilders.post(route + "saveGarden")
                .contentType(MediaType.APPLICATION_JSON_UTF8)
                .content(JSON.toJSONString(garden))
                .accept(MediaType.APPLICATION_JSON))
                .andDo(MockMvcResultHandlers.print())
                .andExpect(MockMvcResultMatchers.status().isOk());
    }

    @Test
    public void getTemperatureSensorByGardenId() throws Exception{
        long gardenId = 1;

        MvcResult result = mvc.perform(MockMvcRequestBuilders.get(route + "getTemperatureSensorByGardenId")
                .param("gardenId", Long.toString(gardenId))
                .accept(MediaType.APPLICATION_JSON))
                .andDo(MockMvcResultHandlers.print())
                .andExpect(MockMvcResultMatchers.status().isOk())
                .andReturn();

        List<TemperatureSensor> list = JSON.parseArray(result.getResponse().getContentAsString(), TemperatureSensor.class);
        for (TemperatureSensor sensor:list) {
            Assert.assertEquals(sensor.getGardenId(), gardenId);
        }
    }

    @Test
    @Transactional
    public void saveTemperatureSensor() throws Exception{
        TemperatureSensor sensor = new TemperatureSensor();
        sensor.setGardenId(1);
        sensor.setName("test temperature");
        sensor.setValid(true);
        sensor.setX(31);
        sensor.setY(65);

        mvc.perform(MockMvcRequestBuilders.post(route + "saveTemperatureSensor")
                .contentType(MediaType.APPLICATION_JSON_UTF8)
                .content(JSON.toJSONString(sensor))
                .accept(MediaType.APPLICATION_JSON))
                .andDo(MockMvcResultHandlers.print())
                .andExpect(MockMvcResultMatchers.status().isOk());
    }

    @Test
    public void getWetnessSensorByGardenId() throws Exception {
        long gardenId = 1;

        MvcResult result = mvc.perform(MockMvcRequestBuilders.get(route + "getWetnessSensorByGardenId")
                .param("gardenId", Long.toString(gardenId))
                .accept(MediaType.APPLICATION_JSON))
                .andDo(MockMvcResultHandlers.print())
                .andExpect(MockMvcResultMatchers.status().isOk())
                .andReturn();

        List<WetnessSensor> list = JSON.parseArray(result.getResponse().getContentAsString(), WetnessSensor.class);
        for (WetnessSensor sensor:list) {
            Assert.assertEquals(sensor.getGardenId(), gardenId);
        }
    }

    @Test
    @Transactional
    public void saveWetnessSensor() throws Exception{
        WetnessSensor sensor = new WetnessSensor();
        sensor.setGardenId(1);
        sensor.setName("test wetness");
        sensor.setValid(true);
        sensor.setX(17);
        sensor.setY(99);

        mvc.perform(MockMvcRequestBuilders.post(route + "saveWetnessSensor")
                .contentType(MediaType.APPLICATION_JSON_UTF8)
                .content(JSON.toJSONString(sensor))
                .accept(MediaType.APPLICATION_JSON))
                .andDo(MockMvcResultHandlers.print())
                .andExpect(MockMvcResultMatchers.status().isOk());
    }

    @Test
    public void getControllerByGardenId() throws Exception{
        long gardenId = 1;

        MvcResult result = mvc.perform(MockMvcRequestBuilders.get(route + "getControllerByGardenId")
                .param("gardenId", Long.toString(gardenId))
                .accept(MediaType.APPLICATION_JSON))
                .andDo(MockMvcResultHandlers.print())
                .andExpect(MockMvcResultMatchers.status().isOk())
                .andReturn();

        List<GardenController> list = JSON.parseArray(result.getResponse().getContentAsString(), GardenController.class);
        for (GardenController controller:list) {
            Assert.assertEquals(controller.getGardenId(), gardenId);
        }
    }

    @Test
    @Transactional
    public void updateControllerValidById() throws Exception{
        long id = 1;

        mvc.perform(MockMvcRequestBuilders.get(route + "updateControllerValidById")
                .param("id", Long.toString(id)).param("valid", "false")
                .accept(MediaType.APPLICATION_JSON))
                .andDo(MockMvcResultHandlers.print())
                .andExpect(MockMvcResultMatchers.status().isOk())
                .andReturn();
    }

    @Test
    @Transactional
    public void saveController() throws Exception{
        GardenController controller = new GardenController();
        controller.setGardenId(1);
        controller.setName("test controller");
        controller.setValid(true);
        controller.setX(17);
        controller.setY(99);

        mvc.perform(MockMvcRequestBuilders.post(route + "saveController")
                .contentType(MediaType.APPLICATION_JSON_UTF8)
                .content(JSON.toJSONString(controller))
                .accept(MediaType.APPLICATION_JSON))
                .andDo(MockMvcResultHandlers.print())
                .andExpect(MockMvcResultMatchers.status().isOk());
    }
}