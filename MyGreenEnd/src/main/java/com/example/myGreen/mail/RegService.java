package com.example.myGreen.mail;

import com.example.myGreen.entity.User;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.scheduling.annotation.Async;
import org.springframework.stereotype.Service;

@Service
public class RegService {

    @Autowired
    TokenManagement tokenManagement;

    @Autowired
    MailService mailService;

    public void sendValidateEmail(User user){
        String token = tokenManagement.getTokenOfSignUp(user);
        System.out.println("用户注册，准备发送邮件：User:" + user.getAccount() + ", Token: " + token);
        mailService.userValidate(user, token);
    }

}
