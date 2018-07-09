package com.example.myGreen.controller;

import com.example.myGreen.dto.NormalDto;
import com.example.myGreen.entity.Reg;
import com.example.myGreen.entity.User;
import com.example.myGreen.mail.RegService;
import com.example.myGreen.repository.RegRepository;
import com.example.myGreen.repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.Date;

@Service
public class BackendService {
    @Autowired
    private RegRepository regRepository;

    @Autowired
    private UserRepository userRepository;

    @Autowired
    private RegService regService;

    public NormalDto validate(String token){
        NormalDto normalDto = new NormalDto();
        Reg reg = regRepository.findByToken(token);

        if(reg == null){
            normalDto.setCode(1);
            normalDto.setResult("the url is invalid");
            return normalDto;
        }

        /*检测是否超时（unfinished）*/
        Long date = new Date().getTime();
        if((date - reg.getTime())> 86400000){
            normalDto.setCode(1);
            normalDto.setResult("the url has been overtime");
            return normalDto;
        }

        User user = userRepository.findByAccount(reg.getAccount());
        user.setValid(true);
        userRepository.save(user);

        normalDto.setCode(0);
        normalDto.setResult("success,please close the window");
        return normalDto;

    }

    public NormalDto testmail(){
        NormalDto normalDto = new NormalDto();

        User user = new User();
        user.setAccount("bian");
        user.setEmail("315889187@qq.com");
        user.setPassword("12345678");
        user.setValid(false);
        userRepository.save(user);

        regService.sendValidateEmail(user);

        normalDto.setCode(0);
        normalDto.setResult("success,please close the window");
        return normalDto;
    }
}
