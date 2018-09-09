package com.example.myGreen.database.repository;

import com.example.myGreen.database.entity.WetnessSensorData;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import javax.persistence.Table;
import java.util.List;

@Repository
@Table(name = "WETNESSSENSORDATA")
@Qualifier("WetnessSensorDataRepository")
public interface WetnessSensorDataRepository extends JpaRepository<WetnessSensorData, Long> {

    @Query(value = "select * from WetnessSensorData where id=:id", nativeQuery = true)
    List<WetnessSensorData> findBySensorId(@Param("id") long id);

    @Query(value = "select wetness from WetnessSensorData where id=:id and time=(select max(time) from WetnessSensorData where id=:id)",
            nativeQuery = true)
    Float findLatestWetnessById(@Param("id") long id);

    @Query(value = "select * from WetnessSensorData where id=:id order by time desc limit 0,:num", nativeQuery = true)
    List<WetnessSensorData> findRecentDataById(@Param("id") long id, @Param("num") int num);
}