package com.example.myGreen.service.mail;

import com.example.myGreen.database.entity.Register;
import com.example.myGreen.database.repository.RegisterRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import java.util.Date;
import java.util.UUID;

@Component
public class TokenManagement {

    @Autowired
    private RegisterRepository registerRepository;

    /**
     * 生成用户验证所需的token
     *
     * @param id 用户ID
     * @return token
     */
    public String getTokenOfSignUp(long id) {
        String token = UUID.randomUUID().toString();
        Long time = new Date().getTime();

        Register reg = new Register(id, token, time);
        registerRepository.save(reg);

        return token;
    }
}
