package com.example.myGreen.mail;

import com.example.myGreen.entity.User;
import net.sf.json.JSONObject;
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

@Service
public class MailService {

    @Autowired
    private JavaMailSender mailSender;

    @Value("${spring.mail.username}")
    private String from;


    /* 发送邮件需要传入2个参数 user 和 token，user即为用户注册信息，token是一个随机的UUID */

    public void userValidate(User user, String token) {
        MimeMessage mailMessage = mailSender.createMimeMessage();
        try {
            String title = "[验证邮件]";
            String content = "[邮件内容]";

            MimeMessageHelper helper = new MimeMessageHelper(mailMessage, true, "GBK");
            helper.setFrom(from);
            helper.setTo(user.getEmail());
            helper.setSubject(title);
            String link = "http://localhost:8080/validate/?token=" + token;
            String message = content + link;
            helper.setText(message, true);
            mailSender.send(mailMessage);
        } catch (MessagingException e) {
            System.out.println("发送邮件失败：User:" + user.getAccount() + ", Token: " + token);
        }
    }
}
