- 系统: linux(ubuntu 16.04)
- 在使用Arduino Uno R3的板子的过程中rxtx串口的配置十分繁琐，而且网上许多的包都太老旧无法正常使用。在多个早上的探索之后，意识到Arduino的IDE用的也是java，应当存在相应的包，于是下载了arduino-1.0.5找到了三个文件：
  - RXTXcomm.jar
  - librxtxSerial.so
  - librxtxSerial64.so
- 具体步骤如下：
  1. 将RXTXcomm.jar文件复制到$JAVA_HOME/jre/lib/ext目录下
  2. 将librxtxSerial.so和librxtxSerial64.so复制到$JAVA_HOME/jre/lib/amd64/目录下（如果是32位系统应该是i386或者i686，而不是amd64）
- 如此可以避免 A fatal error has been detected by the Java Runtime Environment、 could not found COM port等问题。
