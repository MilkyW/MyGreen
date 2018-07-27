package com.example.myGreen.service;
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.OutputStream;

import com.example.myGreen.entity.TemperatureSensorData;
import com.example.myGreen.entity.WetnessSensorData;
import com.example.myGreen.key.SensorDataKey;
import com.example.myGreen.repository.TemperatureSensorDataRepository;
import com.example.myGreen.repository.WetnessSensorDataRepository;
import gnu.io.CommPortIdentifier;
import gnu.io.SerialPort;
import gnu.io.SerialPortEvent;
import gnu.io.SerialPortEventListener;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.sql.Timestamp;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Enumeration;

@Service
public class SerialTest implements SerialPortEventListener {

    @Autowired
    private TemperatureSensorDataRepository temperatureSensorDataRepository;

    @Autowired
    private WetnessSensorDataRepository wetnessSensorDataRepository;

    SerialPort serialPort;
    /** The port we're normally going to use. */
    private static final String PORT_NAMES[] = {
            "/dev/ttyUSB0"
    };
    /**
     * A BufferedReader which will be fed by a InputStreamReader
     * converting the bytes into characters
     * making the displayed results codepage independent
     */
    private BufferedReader input;
    /** The output stream to the port */
    private OutputStream output;
    /** Milliseconds to block while waiting for port open */
    private static final int TIME_OUT = 2000;
    /** Default bits per second for COM port. */
    private static final int DATA_RATE = 9600;

    static int r = 1;

    public void initialize() {

        CommPortIdentifier portId = null;
        Enumeration portEnum = CommPortIdentifier.getPortIdentifiers();

        //First, Find an instance of serial port as set in PORT_NAMES.
        while (portEnum.hasMoreElements()) {
            CommPortIdentifier currPortId = (CommPortIdentifier) portEnum.nextElement();
            for (String portName : PORT_NAMES) {
                if (currPortId.getName().equals(portName)) {
                    portId = currPortId;
                    break;
                }
            }
        }
        if (portId == null) {
            System.out.println("Could not find COM port.");
            return;
        }

        try {
            // open serial port, and use class name for the appName.
            serialPort = (SerialPort) portId.open(this.getClass().getName(),
                    TIME_OUT);

            // set port parameters
            serialPort.setSerialPortParams(DATA_RATE,
                    SerialPort.DATABITS_8,
                    SerialPort.STOPBITS_1,
                    SerialPort.PARITY_NONE);

            // open the streams
            input = new BufferedReader(new InputStreamReader(serialPort.getInputStream()));
            output = serialPort.getOutputStream();

            // add event listeners
            serialPort.addEventListener(this);
            serialPort.notifyOnDataAvailable(true);
        } catch (Exception e) {
            System.err.println(e.toString());
        }
    }

    /**
     * This should be called when you stop using the port.
     * This will prevent port locking on platforms like Linux.
     */
    public synchronized void close() {
        if (serialPort != null) {
            serialPort.removeEventListener();
            serialPort.close();
        }
    }

    /**
     * Handle an event on the serial port. Read the data and print it.
     */
    public synchronized void serialEvent(SerialPortEvent oEvent) {
        if (oEvent.getEventType() == SerialPortEvent.DATA_AVAILABLE) {
            try {
                String inputLine=input.readLine();
                System.out.println(inputLine);
                /* 存入数据库时nullpointer
                 */

                if(inputLine.contains("error")){
                    r = 0;
                }else if(inputLine.contains("Read")){
                    r = 1;
                }
                if(r == 1){
                    long id = 2;
                    SensorDataKey sensorDataKey = new SensorDataKey();
                    sensorDataKey.setId(id);
                    Date date = new Date();//获得系统时间.
                    String nowTime = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(date);//将时间格式转换成符合Timestamp要求的格式.
                    Timestamp goodsC_date =Timestamp.valueOf(nowTime);//把时间转换
                    sensorDataKey.setTime(goodsC_date);

                    if(inputLine.startsWith("Humidity")){
                        int i = inputLine.indexOf(":");
                        int j = inputLine.length();
                        if(i !=-1){
                            String str = inputLine.substring(i+2,j);
                            System.out.println(str);
                            Float a = Float.parseFloat(str);
                            WetnessSensorData wetnessSensorData = new WetnessSensorData();
                            wetnessSensorData.setId(sensorDataKey);
                            wetnessSensorData.setWetness(a);
                            try{
                            wetnessSensorDataRepository.save(wetnessSensorData);} catch (Exception e) {
                                System.err.println(e.toString());
                                System.err.println("sssssssssss");
                            }
                        }
                    }
                    else if(inputLine.startsWith("Temperature")){
                        int i = inputLine.indexOf(":");
                        int j = inputLine.length();
                        if(i != -1) {
                            String str = inputLine.substring(i+2,j);
                            System.out.println(str);
                            Float a = Float.parseFloat(str);
                            TemperatureSensorData temperatureSensorData = new TemperatureSensorData();
                            temperatureSensorData.setId(sensorDataKey);
                            temperatureSensorData.setTemperature(a);
                            try{
                            temperatureSensorDataRepository.save(temperatureSensorData);} catch (Exception e) {
                            System.err.println(e.toString());
                            System.err.println("sssssssssss");
                        }
                        }
                    }
                }
            } catch (Exception e) {
                System.err.println(e.toString());
            }
        }
        // Ignore all the other eventTypes, but you should consider the other ones.
    }

    public static void main(String[] args) throws Exception {
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
    }
}

