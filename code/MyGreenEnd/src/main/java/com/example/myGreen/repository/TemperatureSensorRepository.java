package com.example.myGreen.repository;

import com.example.myGreen.entity.TemperatureSensor;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import javax.persistence.Table;
import javax.transaction.Transactional;
import java.util.List;


@Repository
@Table(name = "TEMPERATURESENSOR")
@Qualifier("TemperatureSensorRepository")
public interface TemperatureSensorRepository extends JpaRepository<TemperatureSensor, Long> {
    @Query("select id from TemperatureSensor t where t.gardenId=:gardenId")
    public List<Long> findSensorIdByGardenId(@Param("gardenId") long gardenId);

    public List<TemperatureSensor> findByGardenId(long gardenId);

    public List<TemperatureSensor> findByName(String name);

    @Transactional
    @Modifying
    @Query(value = "update temperatureSensor set valid = :valid where id = :id", nativeQuery = true)
    public void updateValidById(@Param("id") long id, @Param("valid") boolean valid);
}