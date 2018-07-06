package com.example.myGreen.repository;

import com.example.myGreen.Application;
import com.example.myGreen.entity.User;
import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.context.web.WebAppConfiguration;

@RunWith(SpringRunner.class)
@SpringBootTest(classes = Application.class)
public class UserRepositoryTest {
    @Autowired
    private UserRepository userRepository;

    @Test
    public void findByAccount() throws Exception {
        User user = userRepository.findByAccount("dennis");
        Assert.assertEquals("dennis", user.getAccount());
    }

    @Test
    public void findByPhone() throws Exception{
        User user = userRepository.findByPhone("13000000000");
        Assert.assertEquals("13000000000", user.getPhone());
    }

    @Test
    public void findByEmail() throws Exception{
        User user = userRepository.findByEmail("123456@qq.com");
        Assert.assertEquals("123456@qq.com", user.getEmail());
    }

    @Test
    public void findValidByAccount() throws Exception{
        boolean valid = userRepository.findValidByAccount("dennis");
        Assert.assertEquals(true, valid);
    }
}