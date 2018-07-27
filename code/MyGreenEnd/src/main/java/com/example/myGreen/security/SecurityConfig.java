package com.example.myGreen.security;

import com.example.myGreen.tool.IP;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.ComponentScan;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.config.annotation.authentication.builders.AuthenticationManagerBuilder;
import org.springframework.security.config.annotation.method.configuration.EnableGlobalMethodSecurity;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.builders.WebSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.annotation.web.configuration.WebSecurityConfigurerAdapter;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.AuthenticationException;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.security.web.authentication.AuthenticationFailureHandler;
import org.springframework.security.web.authentication.AuthenticationSuccessHandler;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;

@Configuration
@EnableWebSecurity
@ComponentScan(basePackages = {"com.example.myGreen"})
@EnableGlobalMethodSecurity(securedEnabled = true)// 控制权限注解
public class SecurityConfig extends WebSecurityConfigurerAdapter {

    private static Logger log = LoggerFactory.getLogger(SecurityConfig.class);

    @Autowired
    private UserSecurityService userSecurityService;

    @Autowired
    protected void configure(AuthenticationManagerBuilder auth) throws Exception {
        auth.userDetailsService(userSecurityService).passwordEncoder(new PasswordEncoder() {
            /* 从客户端接收MD5加密的密码，后端无需处理 */
            @Override
            public String encode(CharSequence charSequence) {
                return charSequence.toString();
            }

            @Override
            public boolean matches(CharSequence charSequence, String s) {
                return s.equals(charSequence.toString());
            }
        });
    }

    /**
     * Spring Security具体配置
     *
     * @param http
     * @throws Exception
     */
    @Override
    protected void configure(HttpSecurity http) throws Exception {
        http.authorizeRequests()
                /* 任何人均可访问 */
                .antMatchers("/isAccountExist").permitAll()
                .antMatchers("/isPhoneExist").permitAll()
                .antMatchers("/isEmailExist").permitAll()
                .antMatchers("/saveUser").permitAll()
                .antMatchers("/resendEmail").permitAll()
                .antMatchers("/validate").permitAll()
                .anyRequest().authenticated()//其他的路径都是登录后即可访问
                .and().formLogin().loginPage("/loginPage").successHandler(new AuthenticationSuccessHandler() {
            @Override
            public void onAuthenticationSuccess(HttpServletRequest httpServletRequest, HttpServletResponse httpServletResponse, Authentication authentication)
                    throws IOException, ServletException {
                String ip = IP.getIPAddress(httpServletRequest);
                String username = httpServletRequest.getParameter("account");
                log.info("{} {} 登陆成功", ip, username);

                httpServletResponse.setContentType("application/json;charset=utf-8");
                PrintWriter out = httpServletResponse.getWriter();
                out.write("true");
                out.flush();
                out.close();
            }
        })
                .failureHandler(new AuthenticationFailureHandler() {
                    @Override
                    public void onAuthenticationFailure(HttpServletRequest httpServletRequest, HttpServletResponse httpServletResponse, AuthenticationException e)
                            throws IOException, ServletException {
                        String ip = IP.getIPAddress(httpServletRequest);
                        String username = httpServletRequest.getParameter("account");
                        log.info("{} {} 登陆失败", ip, username);

                        httpServletResponse.setContentType("application/json;charset=utf-8");
                        PrintWriter out = httpServletResponse.getWriter();

                        if (userSecurityService.isBanned(username)) {
                            /* 账户被ban */
                            out.write("banned");
                        } else if (!userSecurityService.isEnabled(username)) {
                            /* 账户未激活 */
                            out.write("notEnabled");
                        } else {
                            /* 密码错误or账号未注册or */
                            out.write("false");
                        }

                        out.flush();
                        out.close();
                    }
                }).loginProcessingUrl("/login")
                .usernameParameter("account").passwordParameter("password").permitAll()
                .and().logout().logoutUrl("/logout").permitAll()
                .and().csrf().disable();
        /* 只允许一个用户登录,如果同一个账户两次登录,那么第一个账户将被踢下线,跳转到登录页面 */
        //http.sessionManagement().maximumSessions(1).expiredUrl("/login");
        /* 调试用，可开放所有链接 */
        //http.authorizeRequests().antMatchers("/*").permitAll();
    }

    @Override
    public void configure(WebSecurity web) throws Exception {
        web.ignoring().antMatchers("/reg");
    }
}
