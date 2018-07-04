package com.example.myGreen.repository;

import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import javax.persistence.Table;

import com.example.myGreen.entity.TemperatureSensorData;
import com.example.myGreen.key.SensorDataKey;

@Repository
@Table(name = "TEMPERATURESENSORDATA")
@Qualifier("TemperatureSensorDataRepository")
public interface TemperatureSensorDataRepository extends JpaRepository<TemperatureSensorData, SensorDataKey> {

    @Query(value = "select * from TemperatureSensorData where id=:id and time=(select max(time) from TemperatureSensorData having id=:id)",nativeQuery = true)
    public TemperatureSensorData findLatestDataById(@Param("id") long id);
}