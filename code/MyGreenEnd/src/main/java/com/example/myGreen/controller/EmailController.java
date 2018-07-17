package com.example.myGreen.controller;

import com.example.myGreen.dto.NormalDto;
import com.example.myGreen.service.mail.MailService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

@CrossOrigin
@RestController
public class EmailController {

    @Autowired
    private MailService mailService;

    @GetMapping("/validate")
    public NormalDto validate(String token) {
        return mailService.validate(token);
    }

    @GetMapping("/resendEmail")
    public void resend(long id) {
        mailService.sendValidateEmail(id);
    }
}
