package com.example.myGreen.httpReqPreHandle.filter;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;

import javax.servlet.*;
import javax.servlet.http.HttpServletRequest;
import java.io.BufferedReader;
import java.io.IOException;

/**
 * Filter
 * 对http请求进行包装，持久化body的数据
 */
public class HttpServletRequestReplacedFilter implements Filter {

    private static final Log log = LogFactory.getLog("requestLogInterceptor");

    @Override
    public void destroy() {
    }

    @Override
    public void doFilter(ServletRequest request, ServletResponse response, FilterChain chain)
            throws IOException, ServletException {
        ServletRequest requestWrapper = null;
        if (request instanceof HttpServletRequest) {
            requestWrapper = new BodyReaderHttpServletRequestWrapper((HttpServletRequest) request);
        }
        if (requestWrapper == null) {
            chain.doFilter(request, response);
        } else {
            BufferedReader br = requestWrapper.getReader();

            String str = null;
            String retStr = "";
            while ((str = br.readLine()) != null) {
                retStr += str;
            }

            chain.doFilter(requestWrapper, response);
        }
    }

    @Override
    public void init(FilterConfig arg0) throws ServletException {
    }
}