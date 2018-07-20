package com.example.myGreen.database.repository;

import com.example.myGreen.database.entity.Garden;
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
@Table(name = "GARDEN")
@Qualifier("gardenRepository")
public interface GardenRepository extends JpaRepository<Garden, Long> {

    List<Garden> findByUserId(long userId);

    @Transactional
    @Modifying
    @Query(value = "update garden set name=:name where id=:id", nativeQuery = true)
    void updateNameById(@Param("id") long id, @Param("name") String name);
}