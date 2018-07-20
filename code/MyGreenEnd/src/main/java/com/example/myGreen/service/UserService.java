package com.example.myGreen.service;

import com.example.myGreen.database.entity.User;
import com.example.myGreen.database.repository.UserRepository;
import com.example.myGreen.service.mail.MailService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class UserService {

    @Autowired
    private UserRepository userRepository;

    @Autowired
    private MailService mailService;

    public User getUserByAccount(String account) {
        return userRepository.findByUsername(account);
    }

    public boolean isAccountExist(String account) {
        User user = userRepository.findByUsername(account);
        return user != null;
    }

    public boolean isEmailExist(String email) {
        User user = userRepository.findByEmail(email);
        return user != null;
    }

    public boolean isPhoneExist(String phone) {
        User user = userRepository.findByPhone(phone);
        return user != null;
    }

    public void saveUser(User user) {
        /* Check username, phone and email */
//        if (isAccountExist(user.getUsername())) {
//            user.setId(0);
//            return;
//        }
//        if (!user.getEmail().matches("^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\\.[a-zA-Z0-9_-]+)+$")) {
//            user.setId(0);
//            return;
//        }
//        if (isEmailExist(user.getEmail())) {
//            user.setId(0);
//            return;
//        }
//        if (!user.getPhone().matches("^((13[0-9])|(14[5|7])|(15([0-3]|[5-9]))|(18[0,5-9]))[0-9]{8}$")) {
//            user.setId(0);
//            return;
//        }
//        if (isPhoneExist(user.getPhone())) {
//            user.setId(0);
//            return;
//        }
//
//        if (user.getPassword().length() != 32) {
//            user.setId(0);
//            return;
//        }

        /* Set unvalid */
        user.setEnabled(false);

        userRepository.save(user);

        /* Send email */
        mailService.sendValidateEmail(user.getId());
    }

    public void updateUser(User newUser) {
        User user = userRepository.findByUsername(newUser.getUsername());
        if (user == null) {
            return;
        }

        user.setPassword(newUser.getPassword());
        user.setEmail(newUser.getEmail());
        user.setPhone(newUser.getPhone());
        user.setFirstname(newUser.getFirstname());
        user.setLastname(newUser.getLastname());
        user.setGender(newUser.getGender());
        user.setNickname(newUser.getNickname());

        userRepository.save(user);
    }
}
