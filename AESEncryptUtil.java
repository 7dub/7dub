package com.dub.common.utils;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import javax.crypto.Cipher;
import javax.crypto.spec.SecretKeySpec;
import java.util.Arrays;

/**
 * Created by 7dub on 2017/4/19.
 */
public final class AESEncryptUtil {
    private static final Logger LOGGER = LoggerFactory.getLogger(AESEncryptUtil.class);
    private static final String KEY = "7dub@utilAESDEnc";

    private AESEncryptUtil() {
        // 不可实例化
    }

    /**
     * 加密
     *
     * @param data 待加密字符串
     * @return 加密字符串
     */
    public static String aesEncrypt(String data) {
        if (null == data) {
            return null;
        }
        try {
            byte[] keyBytes = Arrays.copyOf(KEY.getBytes("ASCII"), 16);
            SecretKeySpec skey = new SecretKeySpec(keyBytes, "AES");
            Cipher cipher = Cipher.getInstance("AES/ECB/PKCS5Padding");
            cipher.init(Cipher.ENCRYPT_MODE, skey);
            return bytes2Hex(cipher.doFinal(data.getBytes("utf-8")));
        } catch (Exception e) {
            LOGGER.warn("aesEncrypt fail", e);
            return null;
        }
    }

    public static String bytes2Hex(byte[] data) {
        StringBuilder buffer = new StringBuilder();
        for (int i = 0; i < data.length; i++) {
            String hexData = Integer.toHexString(data[i] & 0XFF);
            if (hexData.length() < 2) {
                buffer.append("0").append(hexData);
            } else {
                buffer.append(hexData);
            }
        }

        return buffer.toString();
    }

    public static byte[] hex2Bytes(String hexStr) {
        if (hexStr.length() < 1)
            return new byte[0];
        byte[] result = new byte[hexStr.length() / 2];
        for (int i = 0; i < hexStr.length() / 2; i++) {
            int high = Integer.parseInt(hexStr.substring(i * 2, i * 2 + 1), 16);
            int low = Integer.parseInt(hexStr.substring(i * 2 + 1, i * 2 + 2),
                    16);
            result[i] = (byte) (high * 16 + low);
        }
        return result;
    }

    /**
     * 解密
     *
     * @param data 加密字符串
     * @return 原始内容
     */
    public static String aesDecrypt(String data) {
        if (null == data) {
            return null;
        }
        try {
            byte[] content = hex2Bytes(data);
            byte[] keyBytes = Arrays.copyOf(KEY.getBytes("ASCII"), 16);
            SecretKeySpec skey = new SecretKeySpec(keyBytes, "AES");
            Cipher cipher = Cipher.getInstance("AES/ECB/PKCS5Padding");
            cipher.init(Cipher.DECRYPT_MODE, skey);
            byte[] result = cipher.doFinal(content);
            return new String(result, "utf-8");
        } catch (Exception e) {
            LOGGER.warn("aesDecrypt fail", e);
            return null;
        }
    }

    public static void main(String[] args) {
        String s = "123456";
        String sc = aesEncrypt(s);
        System.out.println("加密：" + sc);
        System.out.println("解密：" + aesDecrypt(sc));
    }
}
