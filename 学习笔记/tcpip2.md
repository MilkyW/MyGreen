# TCP/IP的具体含义
从字面意义上讲，有人可能会认为TCP/IP是指TCP与IP两种协议。实际生活当中有时也确实就是指这两种协议。</br>
然而在很多情况下，它只是 ___利用IP进行通信时所必须用到的协议群的统称___ 。</br>
具体来说，IP 或ICMP、TCP或UDP、TELNET或FTP、以及HTTP等都属于TCP/IP的协议。它们与TCP或IP的关系紧密，是互联网必不可少的组成部分。TCP/IP一词泛指这些协议，因此，有时也称TCP/IP为网际协议族。
![2.2](https://github.com/MilkyW/MyGreen/blob/bian2/学习笔记/pic/tcpip.png)

### TCP/IP vs OSI
![2.4](https://github.com/MilkyW/MyGreen/blob/bian2/学习笔记/pic/tcpip2.png)

### 分层中包的结构
![2.5](https://github.com/MilkyW/MyGreen/blob/bian2/学习笔记/pic/包.png)
<br>每个分层的包首部还包含一个识别位,它是用来标识上一层协议的种类信息。
