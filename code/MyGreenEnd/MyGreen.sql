/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     2018-07-03 09:31:11                          */
/*==============================================================*/

drop database if exists mygreen;

create database mygreen;

use mygreen;

/*==============================================================*/
/* Table: Controller                                            */
/*==============================================================*/
create table Controller
(
   id                   bigint not null auto_increment,
   garden_id            bigint,
   name                 varchar(20),
   x                    int,
   y                    int,
   valid                boolean,
   primary key (id)
)
auto_increment = 0;

/*==============================================================*/
/* Table: Garden                                                */
/*==============================================================*/
create table Garden
(
   id                   bigint not null auto_increment,
   user_id              bigint,
   width                int,
   length               int,
   name                 varchar(20),
   ideal_temperature    float,
   ideal_wetness        float,
   primary key (id)
)
auto_increment = 0;

/*==============================================================*/
/* Table: Register                                              */
/*==============================================================*/
create table Register
(
   id                   bigint not null,
   token                varchar(255),
   time                 bigint,
   primary key (id)
)
auto_increment = 0;

/*==============================================================*/
/* Table: TemperatureSensor                                     */
/*==============================================================*/
create table TemperatureSensor
(
   id                   bigint not null auto_increment,
   garden_id            bigint,
   name                 varchar(20),
   x                    int,
   y                    int,
   valid                boolean,
   primary key (id)
)
auto_increment = 0;

/*==============================================================*/
/* Table: User                                                  */
/*==============================================================*/
create table User
(
   id                   bigint not null auto_increment,
   username             varchar(20),
   password             varchar(32),
   nickname             varchar(20),
   gender               boolean,
   phone                varchar(12),
   email                varchar(40),
   enabled              boolean,
   firstname            varchar(20),
   lastname             varchar(20),
   role                 varchar(20),
   primary key (id)
)
auto_increment = 0;

/*==============================================================*/
/* Table: WetnessSensor                                         */
/*==============================================================*/
create table WetnessSensor
(
   id                   bigint not null auto_increment,
   garden_id            bigint,
   name                 varchar(20),
   x                    int,
   y                    int,
   valid                boolean,
   primary key (id)
)
auto_increment = 0;

/*==============================================================*/
/* Table: WetnessSensorData                                     */
/*==============================================================*/
create table WetnessSensorData
(
   time                 timestamp not null,
   wetness              float,
   id                   bigint not null,
   primary key (time, id)
);

/*==============================================================*/
/* Table: temperatureSensorData                                 */
/*==============================================================*/
create table temperatureSensorData
(
   time                 timestamp not null,
   temperature          float,
   id                   bigint not null,
   primary key (time, id)
);

alter table Controller add constraint FK_Reference_2 foreign key (garden_id)
      references Garden (id) on delete restrict on update restrict;

alter table Garden add constraint FK_Reference_1 foreign key (user_id)
      references User (id) on delete restrict on update restrict;

alter table Register add constraint FK_Reference_7 foreign key (id)
      references User (id) on delete restrict on update restrict;

alter table TemperatureSensor add constraint FK_Reference_3 foreign key (garden_id)
      references Garden (id) on delete restrict on update restrict;

alter table WetnessSensor add constraint FK_Reference_4 foreign key (garden_id)
      references Garden (id) on delete restrict on update restrict;

alter table WetnessSensorData add constraint FK_Reference_5 foreign key (id)
      references WetnessSensor (id) on delete restrict on update restrict;

alter table temperatureSensorData add constraint FK_Reference_6 foreign key (id)
      references TemperatureSensor (id) on delete restrict on update restrict;


DELIMITER $
create trigger deleteTemperatureData before delete
on temperatureSensor for each row
begin
    delete from temperatureSensorData where id=old.id;
end$
DELIMITER ;


DELIMITER $
create trigger deleteWetnessData before delete
on wetnessSensor for each row
begin
    delete from wetnessSensorData where id=old.id;
end$
DELIMITER ;


/*==============================================================*/
/* Insert Data                                                  */
/*==============================================================*/
insert into user values(null,"dennis",md5("123456"),"dennis",1,"13000000000","123456@qq.com",1,"dennis","yeh","ROLE_USER");
insert into user values(null,"dawu",md5("123456"),"dawu",0,"15000000000","654321@qq.com",1,"lianyi","wu","ROLE_ADMIN");
insert into garden values(null,1,200,200,"big garden",30,64);
insert into garden values(null,1,50,200,"little garden",10,30);
insert into garden values(null,2,1160,740,"Wu Xingrong Garden",25,55);
/* Garden 1 */
insert into temperatureSensor values(null,1,"t1",78,45,1);
insert into temperatureSensor values(null,1,"t2",32,45,1);
insert into temperaturesensor values(null,1,"t3",174,134,1);
insert into temperaturesensor values(null,1,"t4",171,127,1);
insert into temperaturesensor values(null,1,"t5",135,64,1);
insert into temperaturesensor values(null,1,"t6",38,56,1);
insert into temperaturesensor values(null,1,"t7",41,104,1);
insert into temperaturesensor values(null,1,"t8",132,37,1);
insert into temperaturesensor values(null,1,"t9",151,66,1);
insert into temperaturesensor values(null,1,"t10",44,20,1);
insert into temperaturesensor values(null,1,"t11",130,53,1);
insert into temperaturesensor values(null,1,"t12",119,40,1);
insert into temperaturesensor values(null,1,"t13",147,149,1);
insert into temperaturesensor values(null,1,"t14",116,16,1);
insert into temperaturesensor values(null,1,"t15",62,40,1);
insert into temperaturesensor values(null,1,"t16",86,95,1);
insert into temperaturesensor values(null,1,"t17",139,22,1);
insert into temperaturesensor values(null,1,"t18",71,129,1);
insert into temperaturesensor values(null,1,"t19",107,186,1);
insert into wetnessSensor values(null,1,"w1",1,1,1);
insert into wetnessSensor values(null,1,"w2",150,45,1);
insert into wetnesssensor values(null,1,"w3",83,114,1);
insert into wetnesssensor values(null,1,"w4",39,186,1);
insert into wetnesssensor values(null,1,"w5",72,126,1);
insert into wetnesssensor values(null,1,"w6",94,108,1);
insert into wetnesssensor values(null,1,"w7",67,117,1);
insert into wetnesssensor values(null,1,"w8",77,181,1);
insert into wetnesssensor values(null,1,"w9",87,156,1);
insert into wetnesssensor values(null,1,"w10",93,189,1);
insert into wetnesssensor values(null,1,"w11",107,136,1);
insert into wetnesssensor values(null,1,"w12",48,132,1);
insert into wetnesssensor values(null,1,"w13",27,167,1);
insert into wetnesssensor values(null,1,"w14",13,105,1);
insert into controller values(null,1,"c1",199,180,1);
insert into controller values(null,1,"c2",175,25,1);
insert into controller values(null,1,"c3",41,89,1);
insert into controller values(null,1,"c4",139,63,1);
insert into controller values(null,1,"c5",80,55,1);
insert into controller values(null,1,"c6",157,32,1);
insert into controller values(null,1,"c7",20,168,1);
insert into controller values(null,1,"c8",175,131,1);
insert into controller values(null,1,"c9",85,93,1);
/* Garden 3 */
insert into temperaturesensor values(null,3,"t1",831,727,1);
insert into temperaturesensor values(null,3,"t2",573,635,1);
insert into temperaturesensor values(null,3,"t3",552,250,1);
insert into temperaturesensor values(null,3,"t4",200,269,1);
insert into temperaturesensor values(null,3,"t5",488,521,1);
insert into temperaturesensor values(null,3,"t6",567,6,1);
insert into temperaturesensor values(null,3,"t7",660,413,1);
insert into temperaturesensor values(null,3,"t8",576,34,1);
insert into temperaturesensor values(null,3,"t9",498,611,1);
insert into wetnesssensor values(null,3,"w1",705,74,1);
insert into wetnesssensor values(null,3,"w2",690,22,1);
insert into wetnesssensor values(null,3,"w3",993,27,1);
insert into wetnesssensor values(null,3,"w4",548,31,1);
insert into wetnesssensor values(null,3,"w5",941,45,1);
insert into wetnesssensor values(null,3,"w6",734,14,1);
insert into wetnesssensor values(null,3,"w7",1106,148,1);
insert into wetnesssensor values(null,3,"w8",494,147,1);
insert into wetnesssensor values(null,3,"w9",808,312,1);
/* Sensor Data */
insert into temperatureSensorData values(null, 40, 1);
insert into wetnessSensorData values(null, 39, 1);
/* Large Test Data */
insert into user values(null,"user3","92877af70a45fd6a2ed7fe81e1236b78","user3",1,"15737081291","15737081291@qq.com",1,"first","last","ROLE_USER");
insert into garden values(4,3,763,899,"garden4",25.73,30.20);
insert into temperaturesensor values(null,4,"user3t0",726,876,1);
insert into temperaturesensor values(null,4,"user3t1",546,870,1);
insert into temperaturesensor values(null,4,"user3t2",526,138,1);
insert into temperaturesensor values(null,4,"user3t3",639,541,1);
insert into temperaturesensor values(null,4,"user3t4",136,561,1);
insert into temperaturesensor values(null,4,"user3t5",368,825,1);
insert into temperaturesensor values(null,4,"user3t6",484,699,1);
insert into temperaturesensor values(null,4,"user3t7",193,249,1);
insert into temperaturesensor values(null,4,"user3t8",153,421,1);
insert into temperaturesensor values(null,4,"user3t9",657,75,1);
insert into wetnesssensor values(null,4,"user3w0",176,868,1);
insert into wetnesssensor values(null,4,"user3w1",284,879,1);
insert into wetnesssensor values(null,4,"user3w2",451,883,1);
insert into wetnesssensor values(null,4,"user3w3",546,39,1);
insert into wetnesssensor values(null,4,"user3w4",249,681,1);
insert into wetnesssensor values(null,4,"user3w5",268,120,1);
insert into wetnesssensor values(null,4,"user3w6",714,256,1);
insert into wetnesssensor values(null,4,"user3w7",29,16,1);
insert into garden values(5,3,860,635,"garden5",18.73,58.43);
insert into temperaturesensor values(null,5,"user3t0",606,309,1);
insert into temperaturesensor values(null,5,"user3t1",149,70,1);
insert into temperaturesensor values(null,5,"user3t2",217,64,1);
insert into temperaturesensor values(null,5,"user3t3",475,147,1);
insert into temperaturesensor values(null,5,"user3t4",234,19,1);
insert into temperaturesensor values(null,5,"user3t5",828,8,1);
insert into wetnesssensor values(null,5,"user3w0",504,349,1);
insert into wetnesssensor values(null,5,"user3w1",254,541,1);
insert into wetnesssensor values(null,5,"user3w2",782,407,1);
insert into wetnesssensor values(null,5,"user3w3",18,386,1);
insert into wetnesssensor values(null,5,"user3w4",436,286,1);
insert into wetnesssensor values(null,5,"user3w5",60,398,1);
insert into wetnesssensor values(null,5,"user3w6",50,419,1);
insert into wetnesssensor values(null,5,"user3w7",537,547,1);
insert into wetnesssensor values(null,5,"user3w8",584,399,1);
insert into wetnesssensor values(null,5,"user3w9",461,533,1);
insert into user values(null,"user4","3f02ebe3d7929b091e3d8ccfde2f3bc6","user4",0,"13888009519","13888009519@qq.com",1,"first","last","ROLE_USER");
insert into garden values(6,4,737,981,"garden6",11.92,48.57);
insert into temperaturesensor values(null,6,"user4t0",88,767,1);
insert into temperaturesensor values(null,6,"user4t1",485,551,1);
insert into temperaturesensor values(null,6,"user4t2",223,437,1);
insert into temperaturesensor values(null,6,"user4t3",41,941,1);
insert into temperaturesensor values(null,6,"user4t4",481,421,1);
insert into temperaturesensor values(null,6,"user4t5",666,444,1);
insert into temperaturesensor values(null,6,"user4t6",2,528,1);
insert into wetnesssensor values(null,6,"user4w0",276,632,1);
insert into wetnesssensor values(null,6,"user4w1",711,49,1);
insert into wetnesssensor values(null,6,"user4w2",735,142,1);
insert into wetnesssensor values(null,6,"user4w3",647,218,1);
insert into wetnesssensor values(null,6,"user4w4",36,183,1);
insert into wetnesssensor values(null,6,"user4w5",292,821,1);
insert into wetnesssensor values(null,6,"user4w6",693,28,1);
insert into wetnesssensor values(null,6,"user4w7",219,564,1);
insert into garden values(7,4,956,711,"garden7",11.96,60.76);
insert into temperaturesensor values(null,7,"user4t0",1,481,1);
insert into temperaturesensor values(null,7,"user4t1",755,500,1);
insert into temperaturesensor values(null,7,"user4t2",933,299,1);
insert into temperaturesensor values(null,7,"user4t3",306,176,1);
insert into temperaturesensor values(null,7,"user4t4",940,147,1);
insert into temperaturesensor values(null,7,"user4t5",444,625,1);
insert into temperaturesensor values(null,7,"user4t6",219,694,1);
insert into temperaturesensor values(null,7,"user4t7",166,645,1);
insert into temperaturesensor values(null,7,"user4t8",688,188,1);
insert into temperaturesensor values(null,7,"user4t9",9,382,1);
insert into wetnesssensor values(null,7,"user4w0",755,103,1);
insert into wetnesssensor values(null,7,"user4w1",741,478,1);
insert into wetnesssensor values(null,7,"user4w2",343,328,1);
insert into wetnesssensor values(null,7,"user4w3",255,108,1);
insert into wetnesssensor values(null,7,"user4w4",916,520,1);
insert into wetnesssensor values(null,7,"user4w5",921,348,1);
insert into wetnesssensor values(null,7,"user4w6",106,592,1);
insert into wetnesssensor values(null,7,"user4w7",373,82,1);
insert into user values(null,"user5","0a791842f52a0acfbb3a783378c066b8","user5",0,"15095263176","15095263176@qq.com",1,"first","last","ROLE_USER");
insert into garden values(8,5,670,730,"garden8",10.44,39.89);
insert into temperaturesensor values(null,8,"user5t0",389,204,1);
insert into temperaturesensor values(null,8,"user5t1",501,199,1);
insert into temperaturesensor values(null,8,"user5t2",455,617,1);
insert into temperaturesensor values(null,8,"user5t3",413,260,1);
insert into temperaturesensor values(null,8,"user5t4",39,51,1);
insert into temperaturesensor values(null,8,"user5t5",19,327,1);
insert into temperaturesensor values(null,8,"user5t6",140,216,1);
insert into temperaturesensor values(null,8,"user5t7",582,455,1);
insert into temperaturesensor values(null,8,"user5t8",474,212,1);
insert into temperaturesensor values(null,8,"user5t9",313,92,1);
insert into wetnesssensor values(null,8,"user5w0",96,342,1);
insert into wetnesssensor values(null,8,"user5w1",640,207,1);
insert into wetnesssensor values(null,8,"user5w2",498,272,1);
insert into wetnesssensor values(null,8,"user5w3",269,508,1);
insert into wetnesssensor values(null,8,"user5w4",190,653,1);
insert into wetnesssensor values(null,8,"user5w5",114,249,1);
insert into wetnesssensor values(null,8,"user5w6",627,568,1);
insert into wetnesssensor values(null,8,"user5w7",349,293,1);
insert into user values(null,"user6","affec3b64cf90492377a8114c86fc093","user6",0,"13312477000","13312477000@qq.com",1,"first","last","ROLE_USER");
insert into garden values(9,6,870,709,"garden9",16.48,48.60);
insert into temperaturesensor values(null,9,"user6t0",576,403,1);
insert into temperaturesensor values(null,9,"user6t1",658,5,1);
insert into temperaturesensor values(null,9,"user6t2",231,685,1);
insert into temperaturesensor values(null,9,"user6t3",332,126,1);
insert into temperaturesensor values(null,9,"user6t4",646,200,1);
insert into temperaturesensor values(null,9,"user6t5",12,535,1);
insert into temperaturesensor values(null,9,"user6t6",703,640,1);
insert into temperaturesensor values(null,9,"user6t7",789,69,1);
insert into temperaturesensor values(null,9,"user6t8",611,374,1);
insert into temperaturesensor values(null,9,"user6t9",57,613,1);
insert into wetnesssensor values(null,9,"user6w0",695,66,1);
insert into wetnesssensor values(null,9,"user6w1",574,381,1);
insert into wetnesssensor values(null,9,"user6w2",657,269,1);
insert into wetnesssensor values(null,9,"user6w3",346,74,1);
insert into wetnesssensor values(null,9,"user6w4",591,530,1);
insert into wetnesssensor values(null,9,"user6w5",655,563,1);
insert into wetnesssensor values(null,9,"user6w6",220,576,1);
insert into wetnesssensor values(null,9,"user6w7",733,94,1);
insert into garden values(10,6,691,863,"garden10",29.89,53.20);
insert into temperaturesensor values(null,10,"user6t0",5,573,1);
insert into temperaturesensor values(null,10,"user6t1",562,811,1);
insert into temperaturesensor values(null,10,"user6t2",614,809,1);
insert into temperaturesensor values(null,10,"user6t3",223,190,1);
insert into temperaturesensor values(null,10,"user6t4",667,27,1);
insert into temperaturesensor values(null,10,"user6t5",432,513,1);
insert into temperaturesensor values(null,10,"user6t6",548,67,1);
insert into temperaturesensor values(null,10,"user6t7",206,105,1);
insert into temperaturesensor values(null,10,"user6t8",150,238,1);
insert into temperaturesensor values(null,10,"user6t9",107,468,1);
insert into wetnesssensor values(null,10,"user6w0",630,327,1);
insert into wetnesssensor values(null,10,"user6w1",257,696,1);
insert into wetnesssensor values(null,10,"user6w2",75,99,1);
insert into wetnesssensor values(null,10,"user6w3",344,425,1);
insert into wetnesssensor values(null,10,"user6w4",150,162,1);
insert into wetnesssensor values(null,10,"user6w5",660,594,1);
insert into wetnesssensor values(null,10,"user6w6",163,4,1);
insert into wetnesssensor values(null,10,"user6w7",190,512,1);
insert into wetnesssensor values(null,10,"user6w8",103,779,1);
insert into wetnesssensor values(null,10,"user6w9",274,363,1);
insert into user values(null,"user7","3e0469fb134991f8f75a2760e409c6ed","user7",0,"13280647788","13280647788@qq.com",1,"first","last","ROLE_USER");
insert into garden values(11,7,741,891,"garden11",17.96,56.01);
insert into temperaturesensor values(null,11,"user7t0",199,489,1);
insert into temperaturesensor values(null,11,"user7t1",397,13,1);
insert into temperaturesensor values(null,11,"user7t2",383,480,1);
insert into temperaturesensor values(null,11,"user7t3",476,330,1);
insert into temperaturesensor values(null,11,"user7t4",310,792,1);
insert into temperaturesensor values(null,11,"user7t5",557,340,1);
insert into temperaturesensor values(null,11,"user7t6",125,519,1);
insert into temperaturesensor values(null,11,"user7t7",128,816,1);
insert into temperaturesensor values(null,11,"user7t8",148,681,1);
insert into wetnesssensor values(null,11,"user7w0",552,679,1);
insert into wetnesssensor values(null,11,"user7w1",440,192,1);
insert into wetnesssensor values(null,11,"user7w2",710,847,1);
insert into wetnesssensor values(null,11,"user7w3",188,669,1);
insert into wetnesssensor values(null,11,"user7w4",597,636,1);
insert into wetnesssensor values(null,11,"user7w5",428,678,1);
insert into wetnesssensor values(null,11,"user7w6",394,521,1);
insert into wetnesssensor values(null,11,"user7w7",485,672,1);
insert into wetnesssensor values(null,11,"user7w8",579,526,1);
insert into wetnesssensor values(null,11,"user7w9",724,824,1);
insert into user values(null,"user8","7668f673d5669995175ef91b5d171945","user8",1,"15706049778","15706049778@qq.com",1,"first","last","ROLE_USER");
insert into garden values(12,8,894,951,"garden12",15.72,47.41);
insert into temperaturesensor values(null,12,"user8t0",402,341,1);
insert into temperaturesensor values(null,12,"user8t1",562,94,1);
insert into temperaturesensor values(null,12,"user8t2",235,709,1);
insert into temperaturesensor values(null,12,"user8t3",587,21,1);
insert into temperaturesensor values(null,12,"user8t4",759,282,1);
insert into temperaturesensor values(null,12,"user8t5",485,737,1);
insert into temperaturesensor values(null,12,"user8t6",428,948,1);
insert into temperaturesensor values(null,12,"user8t7",547,857,1);
insert into temperaturesensor values(null,12,"user8t8",293,224,1);
insert into wetnesssensor values(null,12,"user8w0",443,304,1);
insert into wetnesssensor values(null,12,"user8w1",625,602,1);
insert into wetnesssensor values(null,12,"user8w2",291,656,1);
insert into wetnesssensor values(null,12,"user8w3",749,626,1);
insert into wetnesssensor values(null,12,"user8w4",848,405,1);
insert into wetnesssensor values(null,12,"user8w5",462,273,1);
insert into wetnesssensor values(null,12,"user8w6",359,510,1);
insert into garden values(13,8,879,752,"garden13",27.73,47.17);
insert into temperaturesensor values(null,13,"user8t0",522,204,1);
insert into temperaturesensor values(null,13,"user8t1",776,339,1);
insert into temperaturesensor values(null,13,"user8t2",418,632,1);
insert into temperaturesensor values(null,13,"user8t3",232,81,1);
insert into temperaturesensor values(null,13,"user8t4",747,396,1);
insert into temperaturesensor values(null,13,"user8t5",308,515,1);
insert into temperaturesensor values(null,13,"user8t6",375,751,1);
insert into temperaturesensor values(null,13,"user8t7",615,264,1);
insert into wetnesssensor values(null,13,"user8w0",277,241,1);
insert into wetnesssensor values(null,13,"user8w1",41,681,1);
insert into wetnesssensor values(null,13,"user8w2",316,306,1);
insert into wetnesssensor values(null,13,"user8w3",675,191,1);
insert into wetnesssensor values(null,13,"user8w4",15,499,1);
insert into wetnesssensor values(null,13,"user8w5",95,361,1);
insert into wetnesssensor values(null,13,"user8w6",413,372,1);
insert into wetnesssensor values(null,13,"user8w7",420,578,1);
insert into garden values(14,8,921,719,"garden14",10.90,45.25);
insert into temperaturesensor values(null,14,"user8t0",553,89,1);
insert into temperaturesensor values(null,14,"user8t1",562,283,1);
insert into temperaturesensor values(null,14,"user8t2",460,566,1);
insert into temperaturesensor values(null,14,"user8t3",250,368,1);
insert into temperaturesensor values(null,14,"user8t4",709,307,1);
insert into temperaturesensor values(null,14,"user8t5",670,491,1);
insert into temperaturesensor values(null,14,"user8t6",566,100,1);
insert into temperaturesensor values(null,14,"user8t7",311,67,1);
insert into wetnesssensor values(null,14,"user8w0",561,584,1);
insert into wetnesssensor values(null,14,"user8w1",47,572,1);
insert into wetnesssensor values(null,14,"user8w2",622,354,1);
insert into wetnesssensor values(null,14,"user8w3",67,369,1);
insert into wetnesssensor values(null,14,"user8w4",770,552,1);
insert into wetnesssensor values(null,14,"user8w5",485,163,1);
insert into user values(null,"user9","8808a13b854c2563da1a5f6cb2130868","user9",0,"13295983739","13295983739@qq.com",1,"first","last","ROLE_USER");
insert into garden values(15,9,780,655,"garden15",25.68,59.98);
insert into temperaturesensor values(null,15,"user9t0",354,336,1);
insert into temperaturesensor values(null,15,"user9t1",451,328,1);
insert into temperaturesensor values(null,15,"user9t2",740,477,1);
insert into temperaturesensor values(null,15,"user9t3",558,191,1);
insert into temperaturesensor values(null,15,"user9t4",570,609,1);
insert into temperaturesensor values(null,15,"user9t5",510,607,1);
insert into temperaturesensor values(null,15,"user9t6",746,516,1);
insert into temperaturesensor values(null,15,"user9t7",530,467,1);
insert into temperaturesensor values(null,15,"user9t8",464,260,1);
insert into temperaturesensor values(null,15,"user9t9",495,351,1);
insert into wetnesssensor values(null,15,"user9w0",503,156,1);
insert into wetnesssensor values(null,15,"user9w1",148,233,1);
insert into wetnesssensor values(null,15,"user9w2",580,75,1);
insert into wetnesssensor values(null,15,"user9w3",451,351,1);
insert into wetnesssensor values(null,15,"user9w4",156,392,1);
insert into wetnesssensor values(null,15,"user9w5",15,433,1);
insert into wetnesssensor values(null,15,"user9w6",194,653,1);
insert into wetnesssensor values(null,15,"user9w7",706,527,1);
insert into wetnesssensor values(null,15,"user9w8",654,58,1);
insert into wetnesssensor values(null,15,"user9w9",768,408,1);
insert into garden values(16,9,641,885,"garden16",18.55,60.11);
insert into temperaturesensor values(null,16,"user9t0",629,446,1);
insert into temperaturesensor values(null,16,"user9t1",237,231,1);
insert into temperaturesensor values(null,16,"user9t2",155,883,1);
insert into temperaturesensor values(null,16,"user9t3",122,563,1);
insert into temperaturesensor values(null,16,"user9t4",297,118,1);
insert into temperaturesensor values(null,16,"user9t5",253,358,1);
insert into temperaturesensor values(null,16,"user9t6",346,3,1);
insert into temperaturesensor values(null,16,"user9t7",375,41,1);
insert into temperaturesensor values(null,16,"user9t8",326,0,1);
insert into temperaturesensor values(null,16,"user9t9",251,361,1);
insert into wetnesssensor values(null,16,"user9w0",437,67,1);
insert into wetnesssensor values(null,16,"user9w1",461,379,1);
insert into wetnesssensor values(null,16,"user9w2",458,558,1);
insert into wetnesssensor values(null,16,"user9w3",603,272,1);
insert into wetnesssensor values(null,16,"user9w4",383,163,1);
insert into wetnesssensor values(null,16,"user9w5",306,735,1);
insert into wetnesssensor values(null,16,"user9w6",133,397,1);
insert into garden values(17,9,877,765,"garden17",21.74,39.59);
insert into temperaturesensor values(null,17,"user9t0",19,420,1);
insert into temperaturesensor values(null,17,"user9t1",861,697,1);
insert into temperaturesensor values(null,17,"user9t2",314,408,1);
insert into temperaturesensor values(null,17,"user9t3",339,752,1);
insert into temperaturesensor values(null,17,"user9t4",244,407,1);
insert into temperaturesensor values(null,17,"user9t5",356,163,1);
insert into temperaturesensor values(null,17,"user9t6",555,472,1);
insert into temperaturesensor values(null,17,"user9t7",533,380,1);
insert into temperaturesensor values(null,17,"user9t8",202,725,1);
insert into temperaturesensor values(null,17,"user9t9",258,532,1);
insert into wetnesssensor values(null,17,"user9w0",169,164,1);
insert into wetnesssensor values(null,17,"user9w1",513,23,1);
insert into wetnesssensor values(null,17,"user9w2",588,650,1);
insert into wetnesssensor values(null,17,"user9w3",396,26,1);
insert into wetnesssensor values(null,17,"user9w4",434,759,1);
insert into wetnesssensor values(null,17,"user9w5",346,556,1);
insert into wetnesssensor values(null,17,"user9w6",799,423,1);
insert into wetnesssensor values(null,17,"user9w7",723,46,1);
insert into user values(null,"user10","990d67a9f94696b1abe2dccf06900322","user10",0,"15866498953","15866498953@qq.com",1,"first","last","ROLE_USER");
insert into garden values(18,10,959,985,"garden18",15.20,34.91);
insert into temperaturesensor values(null,18,"user10t0",750,255,1);
insert into temperaturesensor values(null,18,"user10t1",870,536,1);
insert into temperaturesensor values(null,18,"user10t2",486,830,1);
insert into temperaturesensor values(null,18,"user10t3",272,879,1);
insert into temperaturesensor values(null,18,"user10t4",378,97,1);
insert into temperaturesensor values(null,18,"user10t5",271,920,1);
insert into temperaturesensor values(null,18,"user10t6",264,622,1);
insert into temperaturesensor values(null,18,"user10t7",958,816,1);
insert into temperaturesensor values(null,18,"user10t8",333,699,1);
insert into temperaturesensor values(null,18,"user10t9",496,143,1);
insert into wetnesssensor values(null,18,"user10w0",880,858,1);
insert into wetnesssensor values(null,18,"user10w1",108,869,1);
insert into wetnesssensor values(null,18,"user10w2",339,864,1);
insert into wetnesssensor values(null,18,"user10w3",19,534,1);
insert into wetnesssensor values(null,18,"user10w4",717,832,1);
insert into wetnesssensor values(null,18,"user10w5",22,895,1);
insert into wetnesssensor values(null,18,"user10w6",146,423,1);
insert into wetnesssensor values(null,18,"user10w7",633,451,1);
insert into wetnesssensor values(null,18,"user10w8",841,390,1);