package com.example.myGreen.repository;

import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import javax.persistence.Table;
import javax.transaction.Transactional;

import com.example.myGreen.entity.WetnessSensor;

import java.util.List;

@Repository
@Table(name = "WETNESSSENSOR")
@Qualifier("wetnessSensorRepository")
public interface WetnessSensorRepository extends JpaRepository<WetnessSensor, Long> {

    public List<WetnessSensor> findByGardenId(long gardenId);

    public List<WetnessSensor> findByName(String name);

    @Transactional
    @Modifying
    @Query(value = "update wetnessSensor set valid = :valid where id = :id",nativeQuery = true)
    public void updateValidById(@Param("id") long id, @Param("valid") boolean valid);
}