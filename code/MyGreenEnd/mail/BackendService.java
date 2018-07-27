package com.example.myGreen.mail;

import com.example.myGreen.dto.NormalDto;
import com.example.myGreen.entity.Register;
import com.example.myGreen.entity.TemperatureSensorData;
import com.example.myGreen.entity.User;
import com.example.myGreen.entity.WetnessSensorData;
import com.example.myGreen.key.SensorDataKey;
import com.example.myGreen.mail.RegService;
import com.example.myGreen.repository.RegRepository;
import com.example.myGreen.repository.TemperatureSensorDataRepository;
import com.example.myGreen.repository.UserRepository;
import com.example.myGreen.repository.WetnessSensorDataRepository;
import com.example.myGreen.service.SerialTest;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.sql.Timestamp;
import java.text.SimpleDateFormat;
import java.util.Date;

@Service
public class BackendService {
    @Autowired
    private RegRepository regRepository;

    @Autowired
    private UserRepository userRepository;

    @Autowired
    private RegService regService;

    @Autowired
    private TemperatureSensorDataRepository temperatureSensorDataRepository;

    @Autowired
    private WetnessSensorDataRepository wetnessSensorDataRepository;

    public NormalDto validate(String token){
        NormalDto normalDto = new NormalDto();
        Register reg = regRepository.findByToken(token);

        if(reg == null){
            normalDto.setCode(1);
            normalDto.setResult("the url is invalid");
            return normalDto;
        }

        /*检测是否超时（unfinished）*/
        Long date = new Date().getTime();
        if((date - reg.getTime())> 86400000){
            normalDto.setCode(1);
            normalDto.setResult("the url has been overtime");
            return normalDto;
        }

        User user = userRepository.findByAccount(reg.getAccount());
        user.setValid(true);
        userRepository.save(user);

        normalDto.setCode(0);
        normalDto.setResult("success,please close the window");
        return normalDto;
    }

    public NormalDto testmail(){
        NormalDto normalDto = new NormalDto();

        User user = new User();
        user.setAccount("bian");
        user.setEmail("1276704961@qq.com");
        user.setPassword("12345678");
        user.setValid(false);
        userRepository.save(user);

        regService.sendValidateEmail(user);

        normalDto.setCode(0);
        normalDto.setResult("success,please close the window");
        return normalDto;
    }

    /*  data generation
     *  every 3 seconds insert a new line
     *  include WetnessSensorData, TemperatureSensorData
     */
    public NormalDto generation(){
        NormalDto normalDto = new NormalDto();

        // run in 3 seconds
        final long timeInterval = 3000;
        int i = 0;
        while (i < 1000) {
            // ------- code for task to run
            java.util.Random r=new java.util.Random();
            /* id in 0 - 99*/
            long id = r.nextLong() % 100;
            SensorDataKey sensorDataKey = new SensorDataKey();
            sensorDataKey.setId(id);

            Date date = new Date();//获得系统时间.
            String nowTime = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(date);//将时间格式转换成符合Timestamp要求的格式.
            Timestamp goodsC_date =Timestamp.valueOf(nowTime);//把时间转换
            sensorDataKey.setTime(goodsC_date);

            float x = r.nextFloat() - 0.5f;
            float y = r.nextFloat();

            TemperatureSensorData temperatureSensorData = new TemperatureSensorData();
            temperatureSensorData.setId(sensorDataKey);
            temperatureSensorData.setTemperature(22+x*10);
            temperatureSensorDataRepository.save(temperatureSensorData);

            WetnessSensorData wetnessSensorData = new WetnessSensorData();
            wetnessSensorData.setId(sensorDataKey);
            wetnessSensorData.setWetness(y*100);
            wetnessSensorDataRepository.save(wetnessSensorData);
            // ------- ends here
            i++;
            try {
                Thread.sleep(timeInterval);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }

        normalDto.setCode(0);
        normalDto.setResult("success,please close the window");
        return normalDto;
    }

    public NormalDto getdata(){
        NormalDto normalDto = new NormalDto();

        SerialTest main = new SerialTest();
        main.initialize();
        Thread t=new Thread() {
            public void run() {
                //the following line will keep this app alive for 1000 seconds,
                //waiting for events to occur and responding to them (printing incoming messages to console).
                try {Thread.sleep(1000000);} catch (InterruptedException ie) {}
            }
        };
        t.start();
        System.out.println("Started");

        normalDto.setCode(0);
        normalDto.setResult("success,please close the window");
        return normalDto;
    }

    public NormalDto wifi(String key, String temper, String humi, String check){
        NormalDto normalDto = new NormalDto();

        SensorDataKey sensorDataKey = new SensorDataKey();
        sensorDataKey.setId(Long.parseLong(key));

        Date date = new Date();//获得系统时间.
        String nowTime = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(date);//将时间格式转换成符合Timestamp要求的格式.
        Timestamp goodsC_date =Timestamp.valueOf(nowTime);//把时间转换
        sensorDataKey.setTime(goodsC_date);

        TemperatureSensorData temperatureSensorData = new TemperatureSensorData();
        temperatureSensorData.setId(sensorDataKey);
        temperatureSensorData.setTemperature(Float.parseFloat(temper));
        temperatureSensorDataRepository.save(temperatureSensorData);

        WetnessSensorData wetnessSensorData = new WetnessSensorData();
        wetnessSensorData.setId(sensorDataKey);
        wetnessSensorData.setWetness(Float.parseFloat(humi));
        wetnessSensorDataRepository.save(wetnessSensorData);

        normalDto.setCode(0);
        normalDto.setResult("success"+check);
        return normalDto;
    }
}
