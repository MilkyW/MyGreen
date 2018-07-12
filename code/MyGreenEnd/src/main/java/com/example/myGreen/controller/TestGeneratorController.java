package com.example.myGreen.controller;


import com.example.myGreen.dto.NormalDto;
import com.example.myGreen.service.GeneratorService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@CrossOrigin
@RestController
public class TestGeneratorController {
    @Autowired
    private GeneratorService generatorService;

    @RequestMapping(value = "/dataGeneration", method = RequestMethod.GET)
    @ResponseBody
    public NormalDto generation() {
        return generatorService.generation();
    }
}
