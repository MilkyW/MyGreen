package com.example.myGreen.service;

import com.example.myGreen.entity.User;
import com.example.myGreen.repository.UserRepository;
import com.example.myGreen.tool.MD5;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class UserService {

    @Autowired
    private UserRepository userRepository;

    public User getUserByAccount(String account) {
        return userRepository.findByAccount(account);
    }

    public boolean isAccountExist(String account) {
        System.out.println(account);
        User user = userRepository.findByAccount(account);
        return user!=null;
    }

    public boolean isEmailExist(String email) {
        System.out.println(email);
        User user = userRepository.findByEmail(email);
        return user!=null;
    }

    public boolean isPhoneExist(String phone) {
        System.out.println(phone);
        User user = userRepository.findByPhone(phone);
        return user!=null;
    }

    public boolean login(String account, String password) {
        System.out.println(account);
        System.out.println(password);
        User user = userRepository.findByAccount(account);
        if (user == null) {
            System.out.println("user not found");
            return false;
        }

        try {
            System.out.println("password:"+password+",MD5:"+MD5.EncoderByMd5(password));
            return MD5.checkPassword(password, user.getPassword());
        }catch (Exception e) {
            e.printStackTrace();
        }

        return false;
    }

    public void saveUser(User user) {
        try {
            user.setPassword(MD5.EncoderByMd5(user.getPassword()));
        }catch (Exception e) {
            e.printStackTrace();
            return;
        }

        userRepository.save(user);

        System.out.println("id:"+user.getId());
    }
}
