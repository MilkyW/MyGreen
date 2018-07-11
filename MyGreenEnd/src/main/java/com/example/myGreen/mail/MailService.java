package com.example.myGreen.mail;

import com.example.myGreen.dto.NormalDto;
import com.example.myGreen.entity.Register;
import com.example.myGreen.entity.User;
import com.example.myGreen.repository.RegRepository;
import com.example.myGreen.repository.UserRepository;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.mail.SimpleMailMessage;
import org.springframework.mail.javamail.JavaMailSender;
import org.springframework.mail.javamail.MimeMessageHelper;
import org.springframework.stereotype.Component;
import org.springframework.stereotype.Service;
import org.springframework.web.bind.annotation.GetMapping;

import javax.mail.MessagingException;
import javax.mail.internet.MimeMessage;
import java.util.Date;

@Service
public class MailService {

    private final String uri = "http://localhost:8081";

    @Autowired
    private RegRepository regRepository;
    @Autowired
    private UserRepository userRepository;

    @Autowired
    private JavaMailSender mailSender;

    @Autowired
    TokenManagement tokenManagement;

    @Value("${spring.mail.username}")
    private String from;

    public NormalDto validate(String token) {
        NormalDto normalDto = new NormalDto();
        Register reg = regRepository.findByToken(token);

        if (reg == null) {
            normalDto.setCode(1);
            normalDto.setResult("the url is invalid");
            return normalDto;
        }

        /*检测是否超时（unfinished）*/
        Long date = new Date().getTime();
        if ((date - reg.getTime()) > 86400000) {
            normalDto.setCode(1);
            normalDto.setResult("the url has been overtime");
            return normalDto;
        }

        User user = userRepository.findByAccount(reg.getAccount());
        user.setValid(true);

        normalDto.setCode(0);
        normalDto.setResult("success,please close the window");
        return normalDto;
    }

    public void sendValidateEmail(User user){
        String token = tokenManagement.getTokenOfSignUp(user);
        System.out.println("用户注册，准备发送邮件：User:" + user.getAccount() + ", Token: " + token);
        userValidate(user, token);
    }

    /* 发送邮件需要传入2个参数 user 和 token，
     * user即为用户注册信息，token是一个随机的UUID
     */
    private void userValidate(User user, String token) {
        MimeMessage mailMessage = mailSender.createMimeMessage();
        try {
            String title = "[验证邮件]";
            String content = "[邮件内容]";

            MimeMessageHelper helper = new MimeMessageHelper(mailMessage, true, "GBK");
            helper.setFrom(from);
            helper.setTo(user.getEmail());
            helper.setSubject(title);
            String link = uri + "/validate?token=" + token;
            String message = content + link;
            helper.setText(message, true);
            mailSender.send(mailMessage);
        } catch (Exception e) {
            System.out.println("发送邮件失败：User:" + user.getAccount() + ", Token: " + token);
            e.printStackTrace();
        }
        System.out.println("发送成功");
    }
}
