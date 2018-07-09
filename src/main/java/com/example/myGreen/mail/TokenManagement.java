package com.example.myGreen.mail;

import com.example.myGreen.entity.Reg;
import com.example.myGreen.entity.User;
import com.example.myGreen.repository.RegRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import java.util.Date;
import java.util.UUID;

@Component
public class TokenManagement {

    @Autowired
    private RegRepository regRepository;

    public String getTokenOfSignUp(User user){

        String token = UUID.randomUUID().toString();
        String value = user.getAccount();
        Long date = new Date().getTime();

        Reg reg = new Reg();
        reg.setAccount(value);
        reg.setToken(token);
        reg.setTime(date);
        regRepository.save(reg);

        return token;

    }


}
