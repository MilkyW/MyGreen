package com.example.myGreen.service.mail;

import com.example.myGreen.database.entity.Register;
import com.example.myGreen.database.entity.User;
import com.example.myGreen.database.repository.RegisterRepository;
import com.example.myGreen.database.repository.UserRepository;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.mail.javamail.JavaMailSender;
import org.springframework.mail.javamail.MimeMessageHelper;
import org.springframework.stereotype.Service;

import javax.mail.internet.MimeMessage;
import java.util.Date;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

@Service
public class MailService {

    private final String uri = "http://localhost:8080";

    private static Logger log = LoggerFactory.getLogger(MailService.class);

    private static ExecutorService executor = Executors.newCachedThreadPool();

    @Autowired
    private RegisterRepository registerRepository;
    @Autowired
    private UserRepository userRepository;

    @Autowired
    private JavaMailSender mailSender;

    @Autowired
    TokenManagement tokenManagement;

    @Value("${spring.mail.username}")
    private String from;

    /**
     * 验证用户，若超时则删除记录
     *
     * @param token 用户提交的UUID
     */
    public NormalDto validate(String token) {
        NormalDto normalDto = new NormalDto();
        Register reg = registerRepository.findByToken(token);

        if (reg == null) {
            normalDto.setCode(1);
            normalDto.setResult("the url is invalid");
            return normalDto;
        }

        /*检测是否超时（unfinished）*/
        Long now = new Date().getTime();
        if (isTimeout(now, reg.getTime(), 86400000)) {
            /* 删除用户和token信息 */
            registerRepository.deleteById(reg.getId());
            userRepository.deleteById(reg.getId());

            normalDto.setCode(1);
            normalDto.setResult("the url has been overtime");
            return normalDto;
        }

        User user = userRepository.findById(reg.getId()).get();
        user.setEnabled(true);
        /* 删除验证信息 */
        registerRepository.deleteById(user.getId());

        normalDto.setCode(0);
        normalDto.setResult("success,please close the window");
        return normalDto;
    }

    public void sendValidateEmail(long id) {
        if (!userRepository.existsById(id)) {
            return;
        }
        User user = userRepository.findById(id).get();

        if (!registerRepository.existsById(id)) {
            /* 刚注册的用户，直接发送验证邮件 */
            String token = tokenManagement.getTokenOfSignUp(user.getId());
            log.info("用户注册，开始发送邮件 User:{} Email:{} Token:{}", user.getUsername(), user.getEmail(), token);
            executor.execute(new EmailThread(user, token));
        } else {
            /* 已注册用户，重新发送验证邮件 */
            Register reg = registerRepository.findById(id).get();
            /* 超时检测 */
            long now = new Date().getTime();
            if (isTimeout(now, reg.getTime(), 86400000)) {
                /* 删除用户和token信息 */
                log.info("用户验证超时，已删除 User:{}", user.getUsername());
                registerRepository.deleteById(reg.getId());
                userRepository.deleteById(reg.getId());
            } else {
                executor.execute(new EmailThread(user, reg.getToken()));
            }
        }
    }

    private boolean isTimeout(long now, long past, long gap) { //单位:ms
        return (now - past) > gap;
    }

    private class EmailThread implements Runnable {

        private User user;
        private String token;

        /**
         * 发送邮件的线程
         *
         * @param user  用户注册信息
         * @param token 随机的UUID
         */
        public EmailThread(User user, String token) {
            this.user = user;
            this.token = token;
        }

        @Override
        public void run() {
            MimeMessage mailMessage = mailSender.createMimeMessage();
            try {
                String title = "[MyGreen验证邮件]";
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
                log.info("发送邮件失败：username:{} token:{}", user.getUsername(), token);
                e.printStackTrace();
                return;
            }
            log.info("发送成功");
        }
    }
}
