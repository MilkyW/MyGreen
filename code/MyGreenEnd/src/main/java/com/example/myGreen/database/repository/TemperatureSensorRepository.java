package com.example.myGreen.database.repository;

import com.example.myGreen.database.entity.TemperatureSensor;
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
    List<Long> findSensorIdByGardenId(@Param("gardenId") long gardenId);

    List<TemperatureSensor> findByGardenId(long gardenId);

    List<TemperatureSensor> findByName(String name);

    @Transactional
    @Modifying
    @Query(value = "update temperatureSensor set valid = :valid where id = :id", nativeQuery = true)
    void updateValidById(@Param("id") long id, @Param("valid") boolean valid);

    @Query("select id from TemperatureSensor t where t.gardenId=:gardenId")
    List<Long> findIdByGardenId(@Param("gardenId") long gardenId);

    @Query("select id from TemperatureSensor t")
    List<Long> findAllId();

    @Query("select new TemperatureSensor (id, gardenId) from TemperatureSensor t")
    List<TemperatureSensor> findSensorInfo();

    @Transactional
    @Modifying
    @Query(value = "update temperatureSensor set name=:name where id=:id", nativeQuery = true)
    void updateNameById(@Param("id") long id, @Param("name") String name);
}