package com.example.myGreen.controller;

import com.example.myGreen.dto.NormalDto;
import com.example.myGreen.mail.MailService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@CrossOrigin
@RestController
public class EmailController {
    @Autowired
    private MailService mailService;

    @RequestMapping(value = "/validate", method = RequestMethod.GET)
    public NormalDto validate(String token) {
        return mailService.validate(token);
    }
}
