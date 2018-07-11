package com.example.myGreen.repository;

import com.example.myGreen.entity.GardenController;
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

    public List<GardenController> findByGardenId(long gardenId);

    public List<GardenController> findByName(String name);

    @Transactional
    @Modifying
    @Query(value = "update controller set valid = :valid where id = :id", nativeQuery = true)
    public void updateValidById(@Param("id") long id, @Param("valid") boolean valid);
}