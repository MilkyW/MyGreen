package com.example.myGreen.httpReqPreHandle.filter;

import org.springframework.boot.web.servlet.FilterRegistrationBean;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.servlet.config.annotation.WebMvcConfigurerAdapter;

@Configuration
public class FiilterConfig extends WebMvcConfigurerAdapter {

    @Bean
    public FilterRegistrationBean myFilter() {
        FilterRegistrationBean myFilter = new FilterRegistrationBean();
        myFilter.addUrlPatterns("/*");
        myFilter.setFilter(new HttpServletRequestReplacedFilter());
        return myFilter;
    }
}