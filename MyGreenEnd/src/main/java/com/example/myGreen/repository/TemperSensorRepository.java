package com.example.myGreen.repository;

import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import javax.persistence.Table;

import com.example.myGreen.entity.TemperSensor;

@Repository
@Table(name = "TEMPERSENSOR")
@Qualifier("temperSensorRepository")
public interface TemperSensorRepository extends JpaRepository<TemperSensor, Long> {


}