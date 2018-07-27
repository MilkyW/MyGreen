package com.example.myGreen.database.repository;

import com.example.myGreen.database.entity.TemperatureSensorData;
import com.example.myGreen.database.entity.key.SensorDataKey;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import javax.persistence.Table;
import java.util.List;

@Repository
@Table(name = "TEMPERATURESENSORDATA")
@Qualifier("TemperatureSensorDataRepository")
public interface TemperatureSensorDataRepository extends JpaRepository<TemperatureSensorData, SensorDataKey> {

    @Query(value = "select * from TemperatureSensorData where id=:id", nativeQuery = true)
    List<TemperatureSensorData> findBySensorId(@Param("id") long id);

    @Query(value = "select * from TemperatureSensorData where id=:id and time=(select max(time) from TemperatureSensorData where id=:id)",
            nativeQuery = true)
    TemperatureSensorData findLatestDataById(@Param("id") long id);

    @Query(value = "select temperature from TemperatureSensorData where id=:id and time=(select max(time) from TemperatureSensorData where id=:id)",
            nativeQuery = true)
    Float findLatestTemperatureById(@Param("id") long id);

    @Query(value = "select * from TemperatureSensorData where id=:id order by time desc limit 0,:num", nativeQuery = true)
    List<TemperatureSensorData> findRecentDataById(@Param("id") long id, @Param("num") int num);
}