package com.example.myGreen.tool;

import org.springframework.util.DigestUtils;

/**
 * 以MD5方式对原文进行加密与比对
 */
public class MD5 {

    public static String EncoderByMd5(String str) {
        return DigestUtils.md5DigestAsHex(str.getBytes());
    }

    public static boolean checkPassword(String password, String md5) {
        return EncoderByMd5(password).equals(md5);
    }
}