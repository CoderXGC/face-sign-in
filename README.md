# 本仓库不再维护，请移步https://github.com/CoderXGC/bsfs
基于C#WIINFORM的人脸识别的签到系统，可以使用，使用虹软的SDK开发包，实现了基本的人脸签到后台查看等功能，后续功能不断完善中，敬请期待。欢迎STAR~

## 1.简介
### 主要界面：启动页：
![Image text](https://github.com/xgc1210/face-sign-in/blob/master/img/qidong.png)
### 主页：
![Image text](https://github.com/xgc1210/face-sign-in/blob/master/img/zhuye.png)
### 签到：
![签到](https://github.com/xgc1210/face-sign-in/blob/master/img/signin.png)
![Image text](https://github.com/xgc1210/face-sign-in/blob/master/img/sign2.png)
### 后台录入员工信息：
![Image text](https://github.com/xgc1210/face-sign-in/blob/master/img/insert.png)
### 更新员工信息：
![Image text](https://github.com/xgc1210/face-sign-in/blob/master/img/insert.png)
### 查看签到：
![Image text](https://github.com/xgc1210/face-sign-in/blob/master/img/query.png)
### 关于：
![Image text](https://github.com/xgc1210/face-sign-in/blob/master/img/about.png)
## 2.使用方法：
首先下载源码，然后第一步申请SDK APPID
请到官网https://ai.arcsoft.com.cn/index.html
![Image text](https://github.com/xgc1210/face-sign-in/blob/master/img/used1.png)
申请后创建应用C++应用，一定创建V2.0的，因为本软件就是基于2.0写的
会找到后台会有一个免费的复制到项目的App.config中，然后就可以执行下一步了
![Image text](https://github.com/xgc1210/face-sign-in/blob/master/img/used2.png)
进行数据库导入
在下载文件中找到facesign.sql导入Mysql数据库本软件基于Mysql数据库写的。
然后打开项目
找到DataMysql.cs修改数据库信息，然后找到Form_Admin_query.cs修改数据库信息。
![Image text](https://github.com/xgc1210/face-sign-in/blob/master/img/used3.png)
![Image text](https://github.com/xgc1210/face-sign-in/blob/master/img/used4.png)
最后启动项目就可以了。

## 3.结语
初级码农，很多地方写的不是很好，还请大家见谅。
您感觉用的还不错可以给一个STAR~谢谢~
欢迎留言反馈BUG
或者加入我们QQ群：57376015
个人技术博客：https://Www.ylesb.com
