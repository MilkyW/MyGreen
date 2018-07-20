package com.example.myGreen.database.repository;

import com.example.myGreen.database.entity.WetnessSensor;
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
@Table(name = "WETNESSSENSOR")
@Qualifier("wetnessSensorRepository")
public interface WetnessSensorRepository extends JpaRepository<WetnessSensor, Long> {

    List<WetnessSensor> findByGardenId(long gardenId);

    List<WetnessSensor> findByName(String name);

    @Transactional
    @Modifying
    @Query(value = "update wetnessSensor set valid = :valid where id = :id", nativeQuery = true)
    void updateValidById(@Param("id") long id, @Param("valid") boolean valid);

    @Query("select id from WetnessSensor t where t.gardenId=:gardenId")
    List<Long> findIdByGardenId(@Param("gardenId") long gardenId);

    @Query("select id from WetnessSensor t")
    List<Long> findAllId();

    @Query("select new WetnessSensor (id, gardenId) from WetnessSensor t")
    List<WetnessSensor> findSensorInfo();
}