package com.example.myGreen.repository;

import com.example.myGreen.Application;
import com.example.myGreen.database.entity.User;
import com.example.myGreen.database.repository.UserRepository;
import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;

@RunWith(SpringRunner.class)
@SpringBootTest(classes = Application.class)
public class UserRepositoryTest {
    @Autowired
    private UserRepository userRepository;

    @Test
    public void findByUsername() throws Exception {
        User user = userRepository.findByUsername("dennis");
        Assert.assertEquals("dennis", user.getUsername());
    }

    @Test
    public void findByPhone() throws Exception {
        User user = userRepository.findByPhone("13000000000");
        Assert.assertEquals("13000000000", user.getPhone());
    }

    @Test
    public void findByEmail() throws Exception {
        User user = userRepository.findByEmail("123456@qq.com");
        Assert.assertEquals("123456@qq.com", user.getEmail());
    }

    @Test
    public void findEnabledByUsername() throws Exception {
        boolean valid = userRepository.findEnabledByUsername("dennis");
        Assert.assertEquals(true, valid);
    }
}