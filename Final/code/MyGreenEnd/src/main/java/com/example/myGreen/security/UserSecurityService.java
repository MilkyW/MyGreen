package com.example.myGreen.security;

import com.example.myGreen.database.entity.User;
import com.example.myGreen.database.repository.RegisterRepository;
import com.example.myGreen.database.repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Service;

@Service
public class UserSecurityService implements UserDetailsService {

    @Autowired
    UserRepository userRepository;
    @Autowired
    RegisterRepository registerRepository;

    @Override
    public UserDetails loadUserByUsername(String username) throws UsernameNotFoundException {
        User user = userRepository.findByUsername(username);
        if (user == null) {
            /* 避免返回null，这里返回一个不含有任何值的User对象，在后期的密码比对过程中一样会验证失败 */
            return new User();
        }
        return user;
    }

    public boolean isBanned(String username) {
        User user = userRepository.findByUsername(username);
        /* 没有注册信息返回true */
        return !registerRepository.findById(user.getId()).isPresent();
    }

    public boolean isEnabled(String username) {
        return userRepository.findByUsername(username).isEnabled();
    }
}
