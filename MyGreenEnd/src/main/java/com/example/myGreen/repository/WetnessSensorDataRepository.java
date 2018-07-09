package com.example.myGreen.repository;

import com.example.myGreen.entity.WetnessSensorData;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import javax.persistence.Table;

@Repository
@Table(name = "WETNESSSENSORDATA")
@Qualifier("WetnessSensorDataRepository")
public interface WetnessSensorDataRepository extends JpaRepository<WetnessSensorData, Long> {

    @Query(value = "select * from WetnessSensorData where id=:id and time=(select max(time) from WetnessSensorData having id=:id)", nativeQuery = true)
    public WetnessSensorData findLatestDataById(@Param("id") long id);
}