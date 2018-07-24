package com.example.myGreen.database.repository;

import com.example.myGreen.database.entity.GardenController;
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
@Table(name = "CONTROLLER")
@Qualifier("controllerRepository")
public interface GardenControllerRepository extends JpaRepository<GardenController, Long> {

    List<GardenController> findByGardenId(long gardenId);

    List<GardenController> findByName(String name);

    @Transactional
    @Modifying
    @Query(value = "update controller set valid = :valid where id = :id", nativeQuery = true)
    void updateValidById(@Param("id") long id, @Param("valid") boolean valid);

    @Transactional
    @Modifying
    @Query(value = "update controller set name=:name where id=:id", nativeQuery = true)
    void updateNameById(@Param("id") long id, @Param("name") String name);
}