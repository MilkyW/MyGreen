package com.example.myGreen.repository;

import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import javax.persistence.Table;

import com.example.myGreen.entity.Garden;

import java.util.List;

@Repository
@Table(name = "GARDEN")
@Qualifier("gardenRepository")
public interface GardenRepository extends JpaRepository<Garden, Long> {

    public List<Garden> findByUserId(long userId);
}