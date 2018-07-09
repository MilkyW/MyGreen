package com.example.myGreen.repository;

import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import javax.persistence.Table;

import com.example.myGreen.entity.WetnessSensorData;

@Repository
@Table(name = "WETNESSSENSORDATA")
@Qualifier("WetnessSensorDataRepository")
public interface WetnessSensorDataRepository extends JpaRepository<WetnessSensorData, Long> {


}